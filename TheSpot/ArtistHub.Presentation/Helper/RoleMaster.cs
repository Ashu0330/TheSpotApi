using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Presentation.Helper
{
    public static class RoleMaster
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Artist = "Artist";
        public const string Lounge = "Lounge";
        public const string UserOrLounge = User + "," + Lounge;
    }
    public enum RoleEnum 
    {
        Admin = 1,
        User = 3,
        Artist = 2,
        Lounge = 4
    }
}
