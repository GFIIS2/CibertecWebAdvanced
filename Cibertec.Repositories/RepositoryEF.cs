using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Cibertec.Repositories
{
    public class RepositoryEF<T> : IRepository<T> where T : class
    {
        private const int SUCCESS_TRANSACTION = 1;

        protected readonly DbContext _dbContext;

        public RepositoryEF(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(T entity)
        {
            _dbContext.Remove(entity);

            return _dbContext.SaveChanges() == SUCCESS_TRANSACTION;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int Insert(T entity)
        {
            _dbContext.Add(entity);

            return _dbContext.SaveChanges();
        }

        public bool Update(T entity)
        {
            _dbContext.Update(entity);

            return _dbContext.SaveChanges() == SUCCESS_TRANSACTION;
        }
        
    }
}
