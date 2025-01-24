
using FilterDemo.Filters.ActionFilters;
using FilterDemo.Filters.AuthorizationFilters;
using FilterDemo.Filters.ExceptionFilters;
using FilterDemo.Filters.ResourceFilters;
using FilterDemo.Filters.ResultFilters;

namespace FilterDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(
                opt => {
                    opt.Filters.Add(new MyGlobalFilter());
                    opt.Filters.Add(new MyResourceFilter());
                    opt.Filters.Add(new MyExceptionFilter());
                    opt.Filters.Add(new MyResultFilter());
                    opt.Filters.Add(new MyAlwaysRunResourceFilter());
                    opt.Filters.Add(new MyAuthorizationFilter());
                    }
                ); // Registers controllers

            // Add filter into builder
            // Add services to the container.
            builder.Services.AddAuthorization();

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
            app.MapControllers(); // Maps controller routes
            app.Run();
        }
    }
}