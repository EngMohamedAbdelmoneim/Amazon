using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazone.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Repos
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{

		private readonly AmazonDbContext _context;
		public GenericRepository(AmazonDbContext context)
		{
			_context = context;
		}

		public async Task<T> Add(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
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

	}
}
