using ArtistHub.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistHub.DAL.UOW
{
    public interface IUOW
    {
        IGenricRepository<T> GenricRepository<T>() where T : class;
        void Save();
        Task SaveAsync();
    }
}
