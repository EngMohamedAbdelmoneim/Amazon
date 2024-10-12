using Amazon.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Amazone.Infrastructure.Specification
{
	internal class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
		{
			var query = inputQuery; 

			if (spec.Critera is not null) 
				query = query.Where(spec.Critera);
			
			if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);

			else if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);

			if (spec.IsPaginationEnabled)
				query = query.Skip(spec.Skip).Take(spec.Take);

			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
			
			return query;
		}
	}
}
