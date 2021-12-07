using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        T get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T,bool>> filter =null,
            Func<IQueryable<T>,IOrderedQueryable<T>> OrderBy = null,
            string includeProperties = null
            );
        T GetFirstorDefault(
            Expression<Func<T, bool>> filter = null,            
            string includeProperties = null
            );
        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
