using Amazon.Core.Entities;
using Amazone.Infrastructure.Specification;

namespace Amazone.Infrastructure.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int? id);
		Task<IReadOnlyList<T>> GetAllAsync();

		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
		Task<T?> GetWithSpecAsync(ISpecification<T> spec);
		Task<int> GetCountAsync(ISpecification<T> spec);

		Task<int> Add(T entity);
		Task<T> Update(T entity);
		Task Delete(T entity);
	}
}
