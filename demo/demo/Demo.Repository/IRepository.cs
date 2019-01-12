using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
 
namespace demo
{
   public interface IRepository<T> where T : class
    {
		
        IQueryable<T> GetQuery();

        /// <summary>
        /// Gets the entity for a given criteria.       
        /// </summary>
        /// <see cref="FirstOrDefault in LINQ/EF"/>
        /// <param name="expression"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Provides the functionality to write where clauses.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>IQueryable for an entity.</returns>
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets the entity for a given criteria.
        /// Note: if there are more than one records found for a given criteria then 
        /// it throws an exception.
        /// </summary>
        /// <see cref="Single in LINQ/EF"/>
        /// <param name="expression"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets the entity for a given criteria.       
        /// </summary>
        /// <see cref="First in LINQ/EF"/>
        /// <param name="expression"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> where);

        /// <summary>
        /// Removes the item from the object set and removes from the database whenever the Unit of work is committed.
        /// Note: It simply marks the entity state to deleted.
        /// </summary>        
        /// <param name="entity">Entity to delete.</param>
        void Delete(T entity);



        /// <summary>
        /// Adds the Entity to the current context scope. this simply marks the entity state to added.
        /// NOTE: this will not saves the entity to database until the UnitWork.Commit() is invoked. 
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        void Attach(T entity);

        /// <summary>
        /// Applies the changes to entity and attaches the entity to context if it is detached.
        /// </summary>
        /// <param name="entity"></param>
        void SaveChanges();


    }
}
