using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
    services.AddControllers();
    services.AddSwaggerGen();

    // Register the ProductAccess class
    services.AddScoped<IDataAccess<Product>, ProductAccess>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greggs Products API V1"); });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}