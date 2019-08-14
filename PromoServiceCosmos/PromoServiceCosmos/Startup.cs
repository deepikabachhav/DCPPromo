using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PromoServiceCosmos.DataAccess;
using PromoServiceCosmos.DataAccess.Utility;
using Swashbuckle.AspNetCore.Swagger;

namespace PromoServiceCosmos
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
           // services.AddMvcCore(options => { options.Conventions.Add(.GetInstance<WebApiOverloadingApplicationModelConvention>()); });
            
           /* services.AddMvcCore(options =>
            {
                options.Conventions.Add(container.GetInstance<WebApiOverloadingApplicationModelConvention>());
            });*/
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<ICosmosConnection, CosmosConnection>();
            services.AddTransient<ICosmosDataAdapter, CosmosDataAdapter>();
            //services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Promo API" });
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Promo API");
            }
            );
        }
    }
}
