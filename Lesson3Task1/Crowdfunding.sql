USE NGdb

DECLARE @ErrorMessage NVARCHAR(MAX) = '';

BEGIN TRANSACTION;
BEGIN TRY

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'User')
CREATE TABLE [User] (
	UserId INT PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	SecondName NVARCHAR(100) NOT NULL
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Category')
CREATE TABLE Category (
	CategoryId INT PRIMARY KEY,
	Description NVARCHAR(255) NOT NULL
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Project')
CREATE TABLE Project (
	ProjectId INT PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	Description NVARCHAR(MAX) NOT NULL,
	CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
	CreatorId INT NOT NULL,
	CategoryId INT NOT NULL,
	FOREIGN KEY (CreatorId) REFERENCES [User](UserId),
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comment')
CREATE TABLE Comment (
	CommentId INT PRIMARY KEY,
	Text NVARCHAR(MAX) NOT NULL,
	Date DATETIME NOT NULL DEFAULT GETDATE(),
	UserId INT NOT NULL,
	ProjectId INT NOT NULL,
	FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vote')
CREATE TABLE Vote (
    VoteId INT PRIMARY KEY,
    UserId INT NOT NULL,
    ProjectId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    UNIQUE (UserId, ProjectId) -- Ensure a user can vote only once per project
);

COMMIT TRANSACTION
END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION;
	SET @ErrorMessage = ERROR_MESSAGE();
	PRINT 'Migration Failed: ' + @ErrorMessage;

END CATCH

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE CreateProject
	@ProjectId INT,
	@Name NVARCHAR(100),
	@Description NVARCHAR(MAX),
	@CreatorId INT,
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION;

	BEGIN TRY
		
		IF ((SELECT COUNT(*) FROM Project WHERE ProjectId = @ProjectId) > 0)
		BEGIN
			COMMIT TRANSACTION;

			RETURN;	
		END;

		INSERT INTO Project (Name, Description, CreatorId, CategoryId)
		VALUES (@Name, @Description, @CreatorId, @CategoryId);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;

	END CATCH;

END
GO

CREATE PROCEDURE CreateComment
	@CommentId INT,
	@Text NVARCHAR(MAX),
	@Date DATETIME = NULL,
    @UserId INT,
    @ProjectId INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION;

	BEGIN TRY
		
		IF ((SELECT COUNT(*) FROM Comment WHERE CommentId = @CommentId) > 0)
		BEGIN
			COMMIT TRANSACTION;

			RETURN;	
		END;

		INSERT INTO Comment (CommentId, Text, Date, UserId, ProjectId)
		VALUES (@CommentId, @Text, @Date, @UserId, @ProjectId);

	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;

	END CATCH;

END
GO

CREATE PROCEDURE GetProjectInfo
	@ProjectId INT
AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

			-- Get Project Info with Category and Owner
			SELECT 
				p.ProjectId,
				p.Name AS ProjectName,
				p.Description AS ProjectDescription,
				p.CreationDate,
				c.Description AS CategoryDescription,
				u.Name AS CreatorName,
				u.SecondName AS CreatorSecondName,
				(SELECT COUNT(*) FROM Vote WHERE ProjectId = p.ProjectId) AS VotesAmount
			FROM Project p
			INNER JOIN Category c ON p.CategoryId = c.CategoryId
			INNER JOIN [User] u ON p.CreatorId = u.UserId
			WHERE p.ProjectId = @ProjectId;

			-- Get Comments Ordered by Date
			SELECT 
				cm.Text,
				cm.Date,
				u.Name AS CommenterName,
				u.SecondName AS CommenterSecondName
			FROM Comment cm
			INNER JOIN [User] u ON cm.UserId = u.UserId
			WHERE cm.ProjectId = @ProjectId
			ORDER BY cm.Date DESC;
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;

	END CATCH;

END
GO

CREATE PROCEDURE GetPaginatedProjects
	@PageNumber INT,
    @PageSize INT,
    @NameFilter NVARCHAR(100) = NULL,
    @CategoryFilter NVARCHAR(255) = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON;
	DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

	BEGIN TRY
		BEGIN TRANSACTION;
		
			SELECT 
				p.ProjectId,
				p.Name AS ProjectName,
				p.Description AS ProjectDescription,
				p.CreationDate,
				c.Description AS CategoryDescription,
				(SELECT COUNT(*) FROM Vote WHERE ProjectId = p.ProjectId) AS VotesAmount
			FROM Project p
			INNER JOIN Category c ON p.CategoryId = c.CategoryId
			WHERE 
				(@NameFilter IS NULL OR p.Name LIKE '%' + @NameFilter + '%')
				AND (@CategoryFilter IS NULL OR c.Description LIKE '%' + @CategoryFilter + '%')
				AND (@StartDate IS NULL OR p.CreationDate >= @StartDate)
				AND (@EndDate IS NULL OR p.CreationDate <= @EndDate)
			ORDER BY p.CreationDate DESC
			OFFSET @Offset ROWS
			FETCH NEXT @PageSize ROWS ONLY;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;

	END CATCH;

END
GO

CREATE PROCEDURE AddVote
	@VoteId INT,
	@UserId INT,
    @ProjectId INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION;

	BEGIN TRY
		
		IF ((SELECT COUNT(*) FROM Vote WHERE VoteId = @VoteId) > 0)
		BEGIN
			COMMIT TRANSACTION;

			RETURN;	
		END;

		INSERT INTO Vote (UserId, ProjectId)
		VALUES (@UserId, @ProjectId);

	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;

	END CATCH;

END
GO