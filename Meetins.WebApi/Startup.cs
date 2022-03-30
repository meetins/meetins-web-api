using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Communication.Hubs;
using Meetins.Core.Data;
using Meetins.Core.Options;
using Meetins.Services.Ftp;
using Meetins.Services.MainPage;
using Meetins.Services.Dialogs;
using Meetins.Services.Profile;
using Meetins.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using Meetins.Services.People;
using System.Threading.Tasks;
using Meetins.Services.Events;
using Meetins.Services.Common;
using Meetins.Services.KudaGo;
using Microsoft.EntityFrameworkCore;
using Meetins.Communication.Abstractions;
using Meetins.Communication.Services;

namespace Meetins.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AccessTokenOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AccessTokenOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AccessTokenOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = tokenValidationParams;
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                // если запрос направлен хабу
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    (path.StartsWithSegments("/messenger")))
                                {
                                    // получаем токен из строки запроса
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });

            
            services.AddDbContext<PostgreDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("NpgTestSqlConnection")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meetins.WebApi", Version = "v1" });
            });

            services.AddSingleton<MessengerManager>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IAboutRepository, AboutRepository>();
            services.AddTransient<IDialogsRepository, DialogsRepository>();
            services.AddTransient<IPeopleRepository, PeopleRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<IKudaGoRepository, KudaGoRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IAboutService, AboutService>();
            services.AddTransient<IFtpService, FtpService>();
            services.AddTransient<IDialogsService, DialogsService>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IKudaGoService, KudaGoService>();
            services.AddTransient<IMailingService, MailingService>();
            services.AddCors();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:5003", "http://localhost:3000",
                        "https://meetins-s.vercel.app",
                        "https://meetins.ru")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetins.WebApi v1"));
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessengerHub>("/messenger");
            });
        }
    }
}
