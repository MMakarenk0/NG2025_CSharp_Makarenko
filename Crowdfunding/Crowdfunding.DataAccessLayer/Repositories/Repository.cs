using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Crowdfunding.DataAccessLayer.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly CrowfundingDbContext _dbContext;

    public Repository(CrowfundingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
    }
    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }
    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var createdEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
        return createdEntity.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = _dbContext.Set<TEntity>().Find(id);
        if (entity != null)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }

    public async Task<TEntity?> FindAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _dbContext.Set<TEntity>().Update(entity);
        return updatedEntity.Entity;
    }
}

