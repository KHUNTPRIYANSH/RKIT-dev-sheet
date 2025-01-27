using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using DependencyInjection.Services;

namespace DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds bank-related services to the dependency injection container.
        /// By default, registers ICICIBankService as the implementation for IBank.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <returns>The IServiceCollection instance.</returns>
        public static IServiceCollection AddBankServices(this IServiceCollection services)
        {
            // Register Bank Services
            services.AddTransient<IBank, ICICIBankService>(); // Default bank service (can be switched to HDFC)

            return services;
        }

        /// <summary>
        /// Adds application-specific services to the dependency injection container.
        /// Registers Company and Employee as scoped services.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <returns>The IServiceCollection instance.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Company and Employee
            services.AddScoped<ICompany, Company>();
            services.AddScoped<IEmployee, Employee>();

            // Register Loan Service
            services.AddSingleton<ILoanService, LoanService>();

            return services;
        }
    }
}
