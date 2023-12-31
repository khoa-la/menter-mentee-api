﻿using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace _2MC.API.Filters
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
         
            if (token != null) {
                   var handler = new JwtSecurityTokenHandler();
            var tokenRead = handler.ReadJwtToken(token);
            var userId = tokenRead.Claims.SingleOrDefault(c => c.Type == "nameid");
                context.Items["UserId"] = userId?.Value;
            };
            await _next(context);
        }
    }
}
