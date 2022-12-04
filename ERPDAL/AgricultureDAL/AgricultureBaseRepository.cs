using ERPDAL.ControlPanelDAL;

using ERPDAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERPDAL.AgricultureDAL
{
    public class AgricultureBaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        internal DbSet<T> dbSet = null;
        public AgricultureBaseRepository(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            if (AgricultureUnitOfWork == null) throw new ArgumentNullException("DbContext is not assigned");
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            dbSet = this._AgricultureUnitOfWork.Db.Set<T>();
        }

        public T SingleOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).AsEnumerable();
        }

        public T GetById(object Id)
        {
            return dbSet.Find(Id);
        }

        public T GetOneByOrg(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.FirstOrDefault(whereCondition);
        }

        public bool Save()
        {
            //throw new NotImplementedException();
            return _AgricultureUnitOfWork.Db.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _AgricultureUnitOfWork.Db.SaveChangesAsync() > 0;
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void InsertAll(IList<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Update(T entity)
        {
            _AgricultureUnitOfWork.Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void UpdateAll(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                _AgricultureUnitOfWork.Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(object Id)
        {
            T entity = this.GetById(Id);
            dbSet.Remove(entity);
        }

        public void DeleteAll(Expression<Func<T, bool>> whereCondition)
        {
            IEnumerable<T> entities = this.GetAll(whereCondition);
            dbSet.RemoveRange(entities);
        }

        public void DeleteOneByOrg(Expression<Func<T, bool>> whereCondition)
        {
            T entity = this.GetOneByOrg(whereCondition);
            dbSet.Remove(entity);
        }

        public bool Exists(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Any(whereCondition);
        }

        public int Count(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).Count();
        }

        public IEnumerable<T> GetPagedRecords(Expression<Func<T, bool>> whereCondition, Expression<Func<T, string>> orderBy, int pageNo, int pageSize)
        {
            return (dbSet.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters);
        }

        public IEnumerable<dynamic> SqlQuery(string Sql, Dictionary<string, object> Parameters)
        {
            using (var cmd = _AgricultureUnitOfWork.Db.Database.Connection.CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                foreach (KeyValuePair<string, object> param in Parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;
                    }
                }
            }
        }
        public IEnumerable<dynamic> SqlQuery(string Sql)
        {
            using (var cmd = _AgricultureUnitOfWork.Db.Database.Connection.CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;
                    }
                }
            }
        }
        private static dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
            return dataRow;
        }

        public IEnumerable<T> GetAll(string childtableName)
        {
            return dbSet.Include(childtableName).AsEnumerable();
        }
        public IEnumerable<T> GetAll(string childtableName, Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Include(childtableName).Where(whereCondition).AsEnumerable();
        }
        public T GetOneByOrg(string childtableName, Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Include(childtableName).FirstOrDefault(whereCondition);
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await dbSet.Where(whereCondition).ToListAsync();
        }

    }
}
