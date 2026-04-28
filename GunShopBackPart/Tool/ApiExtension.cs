using GunShopBackPart.Tool.JVT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace GunShopBackPart.Tool
{
    public static class ApiExtension
    {
        public static void AddAuthenticationHeader(this IServiceCollection services
            , IOptions<JvtOptions> options)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                        ValidateLifetime = true
                    };
                });
            services.AddAuthorization();
        }
    }
}
