using Microsoft.OpenApi.Models;
using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI;
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddDbContext<CommandContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection")));
        services.AddControllers();
        services.AddCors();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Command", Version = "v1" });        
        });
    }

    public void Configure(IApplicationBuilder app , IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {endpoints.MapControllers();});
    }
    
}