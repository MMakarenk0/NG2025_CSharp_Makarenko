using AutoMapper;
using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Dtos.Update;
using Crowdfunding.BLL.Services.Interfaces;
using Crowdfunding.BLL.Shared;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.UoF;

namespace Crowdfunding.BLL.Services.Classes;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> CreateAsync(CreateCategoryDto categoryDto)
    {
        var categoryRepository = _unitOfWork.CategoryRepository;

        var category = _mapper.Map<Category>(categoryDto);
        var createdCategory = await categoryRepository.CreateAsync(category);

        await _unitOfWork.SaveChangesAsync();
        return createdCategory.Id;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var categoryRepository = _unitOfWork.CategoryRepository;

        await categoryRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<CategoryDto>> GetByIdAsync(Guid id)
    {
        var categoryRepository = _unitOfWork.CategoryRepository;

        var category = await categoryRepository.FindAsync(id);

        if (category == null)
            return Result.Failure<CategoryDto>(new Error(
                "Category.NotFound",
                $"The category with Id {id} was not found"));

        return _mapper.Map<CategoryDto?>(category);
    }

    public async Task<Result<IEnumerable<CategoryDto>>> GetAllAsync()
    {
        var categoryRepository = _unitOfWork.CategoryRepository;

        var categories = await categoryRepository.GetAllAsync();
        var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

        return Result.Success(categoryDtos);
    }

    public async Task<Result<Guid>> UpdateAsync(UpdateCategoryDto categoryDto)
    {
        var categoryRepository = _unitOfWork.CategoryRepository;

        var category = await categoryRepository.FindAsync(categoryDto.Id);

        if (category == null)
            return Result.Failure<Guid>(new Error(
                "Category.NotFound",
                $"The category with Id {categoryDto.Id} was not found"));

        _mapper.Map(categoryDto, category);

        await categoryRepository.UpdateAsync(category);

        await _unitOfWork.SaveChangesAsync();

        return category.Id;
    }
}

