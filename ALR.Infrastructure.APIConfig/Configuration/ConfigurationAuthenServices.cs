
using ALR.Services.Authentication.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ALR.Infrastructure.Configuration
{
    public static class ConfigurationAuthenServices
    {
        public static void RegisterTokenBearer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["TokenBearer:Issuer"],
                        ValidateIssuer = true,
                        ValidAudience = configuration["TokenBearer:Audience"],
                        ValidateAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenBearer:SignatureKey"])),
                        ValidateLifetime = true,
                    };

                    //options.Events = new JwtBearerEvents()
                    //{
                    //    OnTokenValidated = async context =>
                    //    {
                    //        var tokenHandler = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
                    //        await tokenHandler.ValidateToken(context);
                    //    },
                    //    OnAuthenticationFailed = context =>
                    //    {
                    //        return Task.CompletedTask;
                    //    },
                    //    OnMessageReceived = context =>
                    //    {
                    //        return Task.CompletedTask;
                    //    },
                    //    OnChallenge = context =>
                    //    {
                    //        return Task.CompletedTask;
                    //    }


                    //};

                });
        }
    }
}
