using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Ticketomat.Api.Framework;
using Ticketomat.Core.Repositories;
using Ticketomat.Infrastructure.Mappers;
using Ticketomat.Infrastructure.Repositories;
using Ticketomat.Infrastructure.Repositories.InMemory;
using Ticketomat.Infrastructure.Services;
using Ticketomat.Infrastructure.Settings;

namespace Ticketomat.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ()
                .AddJsonOptions (y => y.SerializerSettings.Formatting = Formatting.Indented);
            services.AddAuthorization(x => x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
            services.AddScoped<IEventRepository, DatabaseEventRepository> ();
            services.AddScoped<IUserRepository, DatabaseUserRepository> ();
            services.AddScoped<IEventService, EventService> ();
            services.AddScoped<IUserServices, UserServices> ();
            services.AddScoped<ITicketService, TicketService> ();
            services.AddSingleton<IJwtHandler, JwtHandler> ();
            services.AddSingleton (AutoMapperConfig.Initialize ());
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Configuration["Jwt:Key"]))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            app.UseAuthentication();
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseErrorHandler();
            app.UseMvc();
        }
    }
}