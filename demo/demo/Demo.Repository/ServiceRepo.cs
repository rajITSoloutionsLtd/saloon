using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using demo.Models;
using demo.Demo.Entity;
using System.Data.Entity;

namespace demo.Demo.Repository
{
	public class ServiceRepo : IRepository<Service_Table>,IDisposable
	{
		private SMSEntities _context=null;
		public ServiceRepo()
		{
			_context = new SMSEntities();
		}

		public void Add(Service_Table entity)
		{
			_context.Service_Table.Add(entity);			
		}

		public void Attach(Service_Table entity)
		{
			_context.Service_Table.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			entity.update_at = DateTime.Now;
		}

		public void Delete(Service_Table entity)
		{
			_context.Service_Table.Remove(entity);
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public IEnumerable<Service_Table> Find(Expression<Func<Service_Table, bool>> where)
		{
			return _context.Service_Table.Where(where);
		}

		public Service_Table First(Expression<Func<Service_Table, bool>> where)
		{

			throw new NotImplementedException();
		}

		public Service_Table FirstOrDefault(Expression<Func<Service_Table, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Service_Table> GetAll()
		{
			return _context.Service_Table.ToList();
		}

		public IQueryable<Service_Table> GetQuery()
		{
			throw new NotImplementedException();
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public Service_Table Single(Expression<Func<Service_Table, bool>> where)
		{
			return	_context.Service_Table.Where(where).Single();
		}

		public IQueryable<Service_Table> Where(Expression<Func<Service_Table, bool>> expression)
		{
			throw new NotImplementedException();
		}
	}
}