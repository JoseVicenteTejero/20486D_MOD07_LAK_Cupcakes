using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Cupcakes.Data;
using Microsoft.EntityFrameworkCore;
using Cupcakes.Repositories;  // Inyección de dependencia de los repositorios.

namespace Cupcakes
{
    public class Startup
    {
        private IConfiguration _configuration;   //Inyección dependencia Iconfiguration.

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();


            //services.AddDbContext<CupcakeContext>(options =>
            //options.UseSqlite("Data Source=cupcake.db"));

            services.AddDbContext<CupcakeContext>(options =>
                 options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ICupcakeRepository, CupcakeRepository>();   // Inyección dependencia repositorios

            //string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BakeriesDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            //services.AddDbContext<CupcakeContext>(options =>
            //          options.UseSqlServer(connectionString));
          
        }
        //public void Configure(IApplicationBuilder app, CupcakeContext cupcakeContext)
        public void Configure(IApplicationBuilder app)
        {
            //cupcakeContext.Database.EnsureDeleted();
            //cupcakeContext.Database.EnsureCreated();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "CupcakeRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Cupcake", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
