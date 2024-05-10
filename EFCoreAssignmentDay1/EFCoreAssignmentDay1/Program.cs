using EFCoreAssignmentDay1.Configs;
using EFCoreAssignmentDay1.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAssignmentDay1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var databaseConnection = new DatabaseConnections();
            builder.Configuration.GetSection("ConnectionStrings").Bind(databaseConnection);
            // Add services to the container.// Register DatabaseSettings as a service
            builder.Services.AddSingleton<DatabaseConnections>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MyContext>(options =>
            {
                // Configure options such as the connection string here
                options.UseSqlServer(databaseConnection.ConnectionString);
                // Other options
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}