using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestServis.Models;

namespace RestServis
{
    public class Startup
    {
        internal static HotelList HotelList;
        //internal static List<Osoba> PopisOsoba;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //PopisOsoba = new List<Osoba>();
            //Osoba a = new Osoba("Pero", "Peric");
            //Osoba b = new Osoba("Lero", "Leric");
            //PopisOsoba.Add(a);
            //PopisOsoba.Add(b);

            List<Hotels> hotels = new List<Hotels>();
            HotelList = new HotelList(hotels);

            //HotelList = new List<Hotels>();
            //Hotels h1 = new Hotels(00000, "New York Royal", "Main street 1", "NYK", "10000", 8.8);
            //Hotels h2 = new Hotels(00001, "New York President", "Main street 2", "NYK", "10000", 8.6);
            //HotelList.Add(h1);
            //HotelList.Add(h2);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlDataContractSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
