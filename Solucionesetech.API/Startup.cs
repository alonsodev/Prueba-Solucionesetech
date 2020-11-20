using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Solucionesetech.API.Extensions;
using Solucionesetech.API.Handlers;
using Solucionesetech.Command.AvailableTravel.Application.Interfaces;
using Solucionesetech.Command.AvailableTravel.Application.Services;
using Solucionesetech.Command.Destination.Application.Interfaces;
using Solucionesetech.Command.Destination.Application.Services;
using Solucionesetech.Command.Origin.Application.Interfaces;
using Solucionesetech.Command.Origin.Application.Services;
using Solucionesetech.Command.Travel.Application.Interfaces;
using Solucionesetech.Command.Travel.Application.Services;
using Solucionesetech.Command.Traveler.Application.Interfaces;
using Solucionesetech.Command.Traveler.Application.Services;

namespace Solucionesetech.API
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
            var domain = "";
            services.AddCors();

            string stringConnection = Configuration["SolucionesetechConnectionString"];
           
            services.AddTransient<IAuthorizationHandler, HasScopeHandler>();
            /*services.AddControllers()
              .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddClientRequestValidator>());*/

            services.AddTransient<IDestinationApplicationService>(s => new DestinationApplicationService(stringConnection));
            services.AddTransient<IOriginApplicationService>(s => new OriginApplicationService(stringConnection));
            services.AddTransient<IAvailableTravelApplicationService>(s => new AvailableTravelApplicationService(stringConnection));
            services.AddTransient<ITravelerApplicationService>(s => new TravelerApplicationService(stringConnection));
            services.AddTransient<ITravelApplicationService>(s => new TravelApplicationService(stringConnection));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Solucionesetech API",
                    Version = "v1",
                    Description = "RESTful API for Solucionesetech",
                    Contact = new OpenApiContact()
                    {
                        Name = "AlonsoDev",
                        Email = "alonso.palaciso.c@gmail.com"
                    }
                });
                c.TagActionsBy(api => new[] { api.GroupName });
                c.DocInclusionPredicate((name, api) => true);
            });
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.UseMemberCasing();
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
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.ConfigureCustomExceptionMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GA Injury Advocates API V1");
                });
            }
        }
    }
}
