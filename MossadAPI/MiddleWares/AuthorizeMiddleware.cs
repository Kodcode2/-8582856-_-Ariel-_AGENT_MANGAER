using Microsoft.AspNetCore.Http;
using MossadAPI.Models;
using MossadAPI.Data;

namespace MossadAPI.MiddleWares
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MossadAPIContext _context;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, MossadAPIContext context2)
        {
            
        }
    }
}
