
using DependencyInjection.Extensions;
using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using DependencyInjection.Services;

namespace DependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Without Extension method

            // Register Bank Services
            builder.Services.AddTransient<IBank, HDFCBankService>(); // Using ICICI as the default implementation
            //builder.Services.AddTransient<IBank, ICICIBankService>(); // Using ICICI as the default implementation

            // Register Services
            builder.Services.AddScoped<ICompany, Company>(); // Scoped
            builder.Services.AddTransient<IEmployee, Employee>(); // Transient
            builder.Services.AddSingleton<ILoanService, LoanService>(); // Singleton
          
            #endregion

            #region With Extension method
            // Use Extension Methods for Registration
            //builder.Services.AddBankServices();
            //builder.Services.AddApplicationServices();
            #endregion


            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();



            app.Run();

        }
    }
}