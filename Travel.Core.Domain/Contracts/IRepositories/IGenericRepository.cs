using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Travel.Core.Domain.Base;

namespace Travel.Core.Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity, IAggregateRoot
    {
        T Find(object id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        void AddRange(List<T> entities);
        void DeleteRange(List<T> entities);

        IEnumerable<T> GetAll();
        
        //para uso de linq y habiltar consultar flexibles
        T FindFirstOrDefault(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindBy(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, 
                IOrderedQueryable<T>> orderBy = null,
            string includeProperties = ""
        );
    }
}