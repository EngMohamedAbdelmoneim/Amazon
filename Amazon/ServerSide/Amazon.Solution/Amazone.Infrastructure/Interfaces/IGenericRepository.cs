using Amazon.Core.Entities;

namespace Amazone.Infrastructure.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int? id);
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> Add(T entity);
		Task<T> Update(T entity);
		Task Delete(T entity);
	}
}
