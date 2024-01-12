using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll(string? includeProperties = null);
		Task<T> Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
		Task<IEnumerable<T>> GetRange(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
		Task Add(T entity);

	}
}
