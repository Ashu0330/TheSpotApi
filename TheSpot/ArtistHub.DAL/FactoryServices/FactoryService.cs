using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ArtistHub.DAL.IFactoryServices;

namespace ArtistHub.DAL.FactoryServices
{
    public class FactoryService : IFactoryService
    {
        private readonly IConnectionService _connectionService;
        public FactoryService(IConnectionService _connectionService)
        {
            this._connectionService = _connectionService;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(this._connectionService.GetConnectionString());
        }
    }
}
