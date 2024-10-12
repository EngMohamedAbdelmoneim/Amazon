using Amazon.Core.Entities;
using System.Linq.Expressions;

namespace Amazone.Infrastructure.Specification
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Critera { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public List<Expression<Func<T, object>>> ThenIncludes { get; set ; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Take { get; set; } = 0;
		public int Skip { get; set; } = 0;
		public bool IsPaginationEnabled { get; set; }

		public BaseSpecification()
		{
		}

		public BaseSpecification(Expression<Func<T, bool>> criteraExpression)
		{
			Critera = criteraExpression;
		}


		public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}

		public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
		{
			OrderByDesc = orderByDescExpression;
		}


		public void ApplyPagination(int skip, int take)
		{
			IsPaginationEnabled = true;
			Skip = skip;
			Take = take;
		}

	}
}
