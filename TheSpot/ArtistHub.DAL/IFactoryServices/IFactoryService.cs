using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistHub.DAL.IFactoryServices
{
    public interface IFactoryService
    {
        public IDbConnection CreateConnection();

    }
}
