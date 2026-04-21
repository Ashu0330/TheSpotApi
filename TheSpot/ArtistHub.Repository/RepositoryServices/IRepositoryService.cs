using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistHub.Repository.RepositoryServices
{
    public interface IRepositoryService<T> where T : class
    {
        public ValueTask<IEnumerable<T>> GetAllAsync(string query , object? parameters = null);
        public ValueTask<T?> GetAsync(string query, object? parameters = null);

    }
}
