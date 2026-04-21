using ArtistHub.Domain.Context;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;


namespace ArtistHub.DAL.Repository
{
    public class GenricRepository<T> : IGenricRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenricRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ✨ Modern LINQ entry point
        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        // ----------------------------------------
        //            GET ALL (Sync)
        // ----------------------------------------
        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int PageSize = 0)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            // Old-style string includes preserved for compatibility
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp.Trim());
            }

            if (orderBy != null)
                query = orderBy(query);

            if (PageSize > 0)
                query = query.Take(PageSize);

            return query.ToList();
        }

        // ----------------------------------------
        //            GET ALL (Async)
        // ----------------------------------------
        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int pageNo = 1,
            int pageSize = 10,
            bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp.Trim());
            }

            if (orderBy != null)
                query = orderBy(query);

            // Modern paging logic
            query = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        // ----------------------------------------
        //             COUNT ASYNC
        // ----------------------------------------
        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter != null)
                return await _dbSet.CountAsync(filter);

            return await _dbSet.CountAsync();
        }

        // ----------------------------------------
        //             GET BY ID
        // ----------------------------------------
        public T? GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        // ----------------------------------------
        //             ADD
        // ----------------------------------------
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public async Task<bool> AddListAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return true;
        }

        // ----------------------------------------
        //             DELETE
        // ----------------------------------------
        public void DeleteByID(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                Delete(entity);
        }

        public void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        // ----------------------------------------
        //             UPDATE
        // ----------------------------------------
        public void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void UpdateList(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        // ----------------------------------------
        //      STORED PROCEDURES (Modern Way)
        // ----------------------------------------
        public int InsertProcedureData(string storeProcedure, string query, params object[] parameters)
        {
            // This is preserved only because your interface requires it.
            // It simply executes SP + query (legacy style)
            return _context.Database.ExecuteSqlRaw($"{storeProcedure} {query}", parameters);
        }

        public List<T> InsertProcedureData(string storeProcedure, params object[] parameters)
        {
            // Execute stored procedures returning list
            var sql = $"{storeProcedure}";
            return _dbSet.FromSqlRaw(sql, parameters).ToList();
        }
    }
}
