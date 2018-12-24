using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoutingService.Services;
using RoutingService.Services.Interfaces;
using System;

namespace RoutingService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var routeFinderConfig = new RouteFinderConfig();
            Configuration.GetSection("RouteFinder").Bind(routeFinderConfig);
            services.AddSingleton(routeFinderConfig);

            services.AddHttpClient<IFlightsApiClient, FlightsApiClient>(client => {
                client.BaseAddress = new Uri(Configuration["FlightUrlBase"]);
            });

            services.AddScoped<IAirlinesCache, AirlinesCache>();
            services.AddScoped<IFlightsService, FlightsService>();
            services.AddScoped<IRouteFinder, RouteFinder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
