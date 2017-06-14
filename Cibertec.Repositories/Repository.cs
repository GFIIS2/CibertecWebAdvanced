using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Cibertec.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private const int SUCCESS_TRANSACTION = 1;

        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
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
        /*
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            SqlMapperExtensions.TableNameMapper = (type) =>
            {
                return type.Name;
            };

            _connectionString = connectionString;
        }


        public bool Delete(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Delete(entity);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<T>();
            }
        }

        public int Insert(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return (int)connection.Insert(entity);
            }
        }

        public bool Update(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Update(entity);
            }
        }
        */
    }
}
