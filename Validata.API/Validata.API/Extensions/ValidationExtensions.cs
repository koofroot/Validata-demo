using FluentValidation;
using Validata.API.Validators;

namespace Validata.API.Extensions
{
    public static class ValidationExtensions
    {
        public static void AddModelValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCustomerCommandValidator>();
        }
    }
}
