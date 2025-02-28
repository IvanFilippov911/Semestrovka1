using Justwatch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justwatch.Services
{
    public class AuthHelper
    {
        public bool IsAuthorized(HttpRequestContext context)
        {
            // Проверка наличия Cookie с session-token
            if (context.Request.Cookies.Any(c => c.Name == "session-token"))
            {
                var cookie = context.Request.Cookies["session-token"];
                return SessionStorage.ValidateToken(cookie.Value);
            }

            return false;
        }
    }
}
