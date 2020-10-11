using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestApp.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
    
//using Microsoft.AspNetCore.Identity.IdentityRole;

namespace TestApp
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
            var strConnection = Configuration.GetConnectionString("TestAppDatabase");
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(strConnection));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
               {
                   options.User.RequireUniqueEmail = false;
               })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            //ادد کردم برای role authentication
            .AddRoles<ApplicationRole>();

            // Register the Swagger generator, defining 1 or more Swagger documents
                services.AddSwaggerGen();
            
            

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            //بخش آپشن رو ادد کردم
            services.AddAuthorization( options =>
              {
                 options.AddPolicy("RequireUserRole", 
                   Policy => Policy.RequireRole("User"));
              });
            services.AddMvc();

            //adding memory cache by self
            services.AddMemoryCache();
            services.AddControllers();



            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


            }
            else
            {
                   app.UseExceptionHandler("/Error");
                    app.UseHsts();

            }

        

            app.UseExceptionsHandlerController();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
             app.UseSwagger();
             
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
           // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
