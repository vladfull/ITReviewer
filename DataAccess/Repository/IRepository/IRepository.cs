﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll(string? includeProperties = null);
		T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
		IEnumerable<T> GetRange(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
		void Add(T entity);

	}
}
