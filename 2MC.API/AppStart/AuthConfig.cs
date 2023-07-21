using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace _2MC.API.AppStart
{
    public static class AuthConfig
    {
        public static void ConfigureAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection("Token");

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    //auto gen
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    //sign token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                });
            
            services.AddAuthorization(config =>
            {
                config.AddPolicy("2MCAuthorization", policyBuilder =>
                    policyBuilder.RequireClaim("Role")
                );
            });
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
    
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RolesAuthorizationRequirement requirement)
        {
            if (context.User.Identity != null && !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if (requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var userRoles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                var roles = requirement.AllowedRoles;

                validRole = roles.Any(r => userRoles.Contains(r));
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}