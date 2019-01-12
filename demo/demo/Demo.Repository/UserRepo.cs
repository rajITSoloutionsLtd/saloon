using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using demo.Demo.Entity;
namespace demo
{
	public class UserRepo : IRepository<user>
	{
		public void Add(user entity)
		{
			
			throw new NotImplementedException();
		}

		public void Attach(user entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(user entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<user> Find(Expression<Func<user, bool>> where)
		{
			throw new NotImplementedException();
		}

		public user First(Expression<Func<user, bool>> where)
		{
			throw new NotImplementedException();
		}

		public user FirstOrDefault(Expression<Func<user, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<user> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<user> GetQuery()
		{
			throw new NotImplementedException();
		}

		public void SaveChanges()
		{
			throw new NotImplementedException();
		}

		public user Single(Expression<Func<user, bool>> where)
		{
			throw new NotImplementedException();
		}

		public IQueryable<user> Where(Expression<Func<user, bool>> expression)
		{
			throw new NotImplementedException();
		}
	}
}