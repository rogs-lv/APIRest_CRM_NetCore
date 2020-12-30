using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.DAO.DAO;
using CRM.Helpers;
using CRM.Middleware;
using CRM.Service;
using CRM.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CRM
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
            services.AddControllers(config => {
                // Filter all controllers and secure that have authentication
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            // services.AddApplicationInsightsTelemetry();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            /* Adds a default implementation for the IHttpContextAccessor service
             Provides access to the current HttpContext, if one is available. */
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o => {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddScoped<LoginDAO>();
            services.AddScoped<MasterDataDAO>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IMasterDataService, MasterDataService>();
            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme   = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme      = JwtBearerDefaults.AuthenticationScheme;
            })// Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"])),
                    ValidateLifetime    = true,
                    ValidIssuer         = "",
                    ValidAudience       = "",
                    ValidateAudience    = false,
                    ValidateIssuer      = false,
                    ValidateIssuerSigningKey = true
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 
            app.UseWelcomePage();
        }
    }
}
