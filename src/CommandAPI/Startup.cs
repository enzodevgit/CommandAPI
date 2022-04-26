using Microsoft.OpenApi.Models;
using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
namespace CommandAPI;
public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        
        // Build Connection String
        NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
        builder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
        builder.Username = Configuration["UserID"];
        builder.Password = Configuration["Password"];

        // Add services to the container.
        services.AddDbContext<CommandContext>(options => options.UseNpgsql(builder.ConnectionString));
        services.AddControllers();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<ICommandRepository, CommandRepository>();
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