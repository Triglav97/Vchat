using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Vchat.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vchat.Models;
using Vchat.ViewModels;

namespace Vchat{
    public class Startup{
        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){
            services.AddTransient<IPasswordValidator<User>,CustomPasswordValidator>(
                                    serv => new CustomPasswordValidator(5));
            services.AddTransient<IUserValidator<User>, CustomUserValidator>();
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>();
            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlite(
            //         Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<User, IdentityRole>(
                opts=> {
                    opts.Password.RequiredLength = 5;               // минимальная длина
                    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                    opts.Password.RequireLowercase = false;         // требуются ли символы в нижнем регистре
                    opts.Password.RequireUppercase = false;         // требуются ли символы в верхнем регистре
                    opts.Password.RequireDigit = false;             // требуются ли цифры
                    opts.User.RequireUniqueEmail = true;    // уникальный email
                    opts.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz"; // допустимые символы
                } 
                /*options => options.SignIn.RequireConfirmedAccount = true*/
            )
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else{
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
