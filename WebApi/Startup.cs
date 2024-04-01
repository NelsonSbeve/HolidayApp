using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain.DataBaseContext;
using Domain.Service; // Import your DbContext namespace

namespace WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register DbContext with an in-memory database
            services.AddDbContext<ColaboratorContext>(options =>
                options.UseInMemoryDatabase("ColaboratorContext"));
            
            services.AddDbContext<HolidayContext>(options =>
                options.UseInMemoryDatabase("HolidayContext"));
            
            // Add controllers
            services.AddScoped<ColaboratorService>(); 
            services.AddControllers();
        }

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
