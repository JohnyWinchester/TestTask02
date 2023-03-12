using Microsoft.EntityFrameworkCore;
using TestTask02Matveew.DAL.Context;
using TestTask02Matveew.Interfaces;
using TestTask02Matveew.Services;

namespace TestTask02Matveew
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            // Add services to the container.

            builder.Services.AddDbContext<TestTask02MatveewDB>(opt
                => opt.UseSqlServer(config["ConnectionStrings:SqlServer"]));

            builder.Services.AddScoped<ITicTac, SqlTicTac>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}