using Amazon.Core.Entities;
using System.Linq.Expressions;

namespace Amazone.Infrastructure.Specification
{
	public interface ISpecification<T> where T : BaseEntity 
	{
		public Expression<Func<T, bool>> Critera { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; }

		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Take { get; set; } 
		public int Skip { get; set; }
		public bool IsPaginationEnabled { get; set; }
	}
}
