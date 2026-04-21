using ArtistHub.DAL.IFactoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistHub.DAL.FactoryServices
{
    public class ConnectionService:IConnectionService
    {
        public string ConStr;
        public ConnectionService(string ConStr)
        {
            this.ConStr = ConStr;
        }
        public string GetConnectionString()
        {
            return ConStr;
        }
    }
}
