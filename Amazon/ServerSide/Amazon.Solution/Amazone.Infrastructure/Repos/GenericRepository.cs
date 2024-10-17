using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazone.Infrastructure.Interfaces;
using Amazone.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace Amazone.Infrastructure.Repos
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{

		private readonly AmazonDbContext _context;
		public GenericRepository(AmazonDbContext context)
		{
			_context = context;
		}

		public async Task<int> Add(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			var result = await _context.SaveChangesAsync();
			return result;
		}
		public async Task<T> Update(T entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
						   => await _context.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int? id) 
			=> await _context.Set<T>().FindAsync(id);


		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
			=> await ApplySpecification(spec).ToListAsync();

		public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
			=> await ApplySpecification(spec).FirstOrDefaultAsync();

		private IQueryable<T> ApplySpecification(ISpecification<T> spec)
			=> SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);

		public async Task<int> GetCountAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).CountAsync();
		}
	}
}
