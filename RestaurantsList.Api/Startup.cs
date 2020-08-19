using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantsList.Api.Filters;
using RestaurantsList.DataAccess.MSSQL.Extensions;
using RestaurantsList.DataAccess.MSSQL.Mapper;
using RestaurantsList.Extensions;
using RestaurantsList.Mapper;
using RestaurantsList.Validators;
using Serilog;

namespace RestaurantsList.Api
{
    public class Startup
    {
        private const string ConnectionString = "RestaurantsListConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDataAccessMsSql(Configuration.GetConnectionString(ConnectionString));
            services.AddDomain();

            services.AddAutoMapper(typeof(DomainMapperProfile), typeof(MsSqlMapperProfile));
            services
                .AddMvc(o => o.Filters.Add(new ErrorHandlingFilter()))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCityRqValidator>());

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantsList.Api V1");
            });

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
