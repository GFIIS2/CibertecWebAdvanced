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

        public T GetById(int id)
        {
            throw new NotImplementedException();
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
        
    }
}
