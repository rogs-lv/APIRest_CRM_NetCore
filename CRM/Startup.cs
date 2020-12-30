using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Microsoft.OpenApi.Models;

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
            //Documentation API
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API with JWT Login and some endpoints to DB SAP B1 (pagination)",
                    Description = "A simple example ASP.NET Core Web API using pagination. The pagination was read in article of Mukesh Murugan https://codewithmukesh.com/blog/pagination-in-aspnet-core-webapi/",
                    Contact = new OpenApiContact
                    {
                        Name = "Oliver Rosas González",
                        Email = "oliverrosasg@gmail.com",
                        Url = new Uri("https://twitter.com/spboyer"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //Custom CORS
            services.AddCors(c => {
                c.AddPolicy("CORS", builder =>
                {
                    builder.AllowAnyHeader()
                    .WithOrigins(Configuration["AppSettings:Issuer"])
                    .WithMethods("GET", "POST").Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c => {
                c.SerializeAsV2 = true;
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API with JWT with some endpoints to DB SAP B1 and pagination");
            });
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseCors();

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
