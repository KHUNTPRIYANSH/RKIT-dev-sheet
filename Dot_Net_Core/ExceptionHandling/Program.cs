
using ExceptionHandling.Middlewares;

namespace ExceptionHandling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
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
                //app.UseDeveloperExceptionPage();

                // inorder to see how exception look like in production use following code

                app.UseMiddleware<ExceptionHandlerMiddleware>();
            }
            else
            {
                // Use a custom error handling middleware in production
                app.UseMiddleware<ExceptionHandlerMiddleware>();
            }

            app.UseAuthorization();



            app.MapControllers();
            app.Run();
        }
    }
}