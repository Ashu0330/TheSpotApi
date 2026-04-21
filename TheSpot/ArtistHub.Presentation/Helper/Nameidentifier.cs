using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ArtistHub.Presentation.Helper
{
    public class Nameidentifier
    {

        public static long GetUserId(HttpContext? httpContext)
        {
            var userId = httpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (long.TryParse(userId, out long id))
            return id;

            return 0;
        }
        public static string? GetRole(HttpContext? httpContext)
        {
            return httpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        }

    }
}
