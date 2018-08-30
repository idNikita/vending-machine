using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VendingMachine.Models;

namespace VendingMachine {

    public class Startup {

        private string _contentRootPath = "";

        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            string connectionString = Configuration["Data:VendingMachineDb:ConnectionString"];
            if (connectionString.Contains("%CONTENTROOTPATH%")) {
                connectionString = connectionString.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            //services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<ICoinRepository, EFCoinRepository>();
            services.AddScoped<Balance>(ub => SessionBalance.GetBalance(ub));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //SeedData.EnsurePopulated(app);
        }
    }
}
