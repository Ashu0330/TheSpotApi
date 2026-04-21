using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArtistHub.DAL.Repository
{
    public interface IGenricRepository<T> where T : class
    {
        // Modern LINQ entry point (new but required)
        IQueryable<T> Query();

        // SAME METHOD NAMES — now modernized
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int PageSize = 0);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int pageNo = 1,
            int pageSize = 10,
            bool asNoTracking = false);

        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

        T? GetByID(object id);
        Task<T?> GetByIdAsync(object id);

        void Add(T entity);
        Task<bool> AddAsync(T entity);
        Task<bool> AddListAsync(IEnumerable<T> entityList);

        void DeleteByID(object id);
        void Delete(T entity);

        void Update(T entity);
        void UpdateList(IEnumerable<T> entities);

        // Modern stored procedure support but SAME NAME
        int InsertProcedureData(string storeProcedure, string query, params object[] parameters);

        List<T> InsertProcedureData(string storeProcedure, params object[] parameters);
    }
}
