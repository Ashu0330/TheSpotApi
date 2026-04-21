using ArtistHub.DAL.Repository;
using ArtistHub.Domain.Context;

namespace ArtistHub.DAL.UOW
{
    public class UOW : IUOW
    {
        private readonly AppDbContext _context;

        public UOW(AppDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenricRepository<T> GenricRepository<T>() where T : class
        {
            return new GenricRepository<T>(_context);
        }
    }
}
