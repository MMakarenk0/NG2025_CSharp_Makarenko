using Crowdfunding.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace Crowdfunding.DataAccessLayer.Repositories.Interface;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
    Task<ICollection<TEntity>> GetAllAsync();
    IQueryable<TEntity> GetAll();
    Task<TEntity> CreateAsync(TEntity entity);
    Task DeleteAsync(Guid Id);
    Task<TEntity?> FindAsync(Guid id);
    Task<TEntity> UpdateAsync(TEntity entity);
}