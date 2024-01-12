﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ITReviewer.Libraries.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ITReviewer.Libraries.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDBContext _db;
		internal DbSet<T> DbSet;
		public Repository(ApplicationDBContext db)
		{
			_db = db;
			this.DbSet= _db.Set<T>();
			
		}

		public void Add(T entity)
		{
			DbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = DbSet;
			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = DbSet;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach(var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			return query.ToList();
		}
		public IEnumerable<T> GetRange(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = DbSet;
			query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
			return query.ToList();
        }
		

		public void Remove(T entity)
		{
			DbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			DbSet.RemoveRange(entity);
		}
	}
}
