using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TariffComparer.Business.Interface;
using TariffComparer.Business.Service;

namespace TariffComparerWebAPI
{
    public class Startup
    {
        public IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITariffAnnualCostsCalculator, TariffAnnualCostsCalculator>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TerrifComparerWebAPI", Version = "v1" });
            });

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TerrifComparerWebAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHsts();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
