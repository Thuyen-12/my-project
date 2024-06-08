using ALR.Data.Dto;
using ALR.Infrastructure.Configuration;
using ALR.Services.Common;

using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddSignalR(o =>
        {
            o.EnableDetailedErrors = true;
        });
        builder.Services.RegisterTokenBearer(builder.Configuration);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers()
   .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
        builder.Services.RegisterContextDB(builder.Configuration);
        builder.Services.RegiserDI();

        builder.Services.RegisterCors();
        builder.Services.RegiserMapper();
        builder.Services.AddHttpContextAccessor();
        var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        builder.Services.AddSingleton(emailConfig);


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseAuthorization();
        app.UseCors("AllowReactApp");


        app.MapControllers();
        app.MapHub<ChatHub>("/chat");


        app.Run();
    }
}