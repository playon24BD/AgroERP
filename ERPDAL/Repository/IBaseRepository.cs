using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERPDAL.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        T SingleOrDefault(Expression<Func<T, bool>> whereCondition);
        IEnumerable<T> GetAll(string childtableName);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string childtableName,Expression<Func<T, bool>> whereCondition);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition);
        T GetById(object Id);
        T GetOneByOrg(string childtableName,Expression<Func<T, bool>> whereCondition);
        T GetOneByOrg(Expression<Func<T, bool>> whereCondition);
        void Insert(T entity);
        void InsertAll(IList<T> entities);
        void Update(T entity);
        void UpdateAll(IList<T> entities);
        void Delete(object Id);
        void DeleteOneByOrg(Expression<Func<T, bool>> whereCondition);
        void DeleteAll(Expression<Func<T, bool>> whereCondition);
        bool Exists(Expression<Func<T, bool>> whereCondition);
        bool Save();
        Task<bool> SaveAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition);
    }
}
