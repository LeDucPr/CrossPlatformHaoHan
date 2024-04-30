using ApiTruyenLau.Services;
using ApiTruyenLau.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Configuration;
using System.Text;

namespace ApiTruyenLau
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder);
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            Configure(builder.Services, app, builder.Environment);
            // run 
            app.MapControllers();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)

        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0.1", new OpenApiInfo { Title = "ApiTruyenLau", Version = "v1.0.1" });
            });
            // Configure MongoDB
            var mongoDBSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDBSettings>();
            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongoDBSettings?.ConnectionString));

            services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDBSettings?.DatabaseName));
            services.AddSingleton<IConfiguration>(builder.Configuration);
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IClientServices, ClientServices>();
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<ISecurityServices, SecurityServices>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });
        }

        private static void Configure(IServiceCollection services, IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.0.1/swagger.json", "ApiTruyenLau v1.0.1"));
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseAuthorization();
        }
    }
}
