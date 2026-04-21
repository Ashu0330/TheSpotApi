
using ArtistHub.DAL.IFactoryServices;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistHub.Repository.RepositoryServices
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly IFactoryService service;
        public RepositoryService(IFactoryService service)
        {
            this.service = service;
        }
        public async ValueTask<IEnumerable<T>> GetAllAsync(string query, object? parameters = null)
        {
            using var connection = this.service.CreateConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            return parameters != null ? (await connection.QueryAsync<T>(query, parameters)).ToList() :
                (await connection.QueryAsync<T>(query)).ToList();
        }

        public async ValueTask<T?> GetAsync(string query, object? parameters = null)
        {
            using var connection = this.service.CreateConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            return parameters != null ? (await connection.QueryAsync<T>(query, parameters)).LastOrDefault() :
                 (await connection.QueryAsync<T>(query)).LastOrDefault();
        }
    }
}
