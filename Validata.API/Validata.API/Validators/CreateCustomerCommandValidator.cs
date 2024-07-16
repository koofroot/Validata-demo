using FluentValidation;
using Validata.Infrastructure.Handlers.CustomerHandlers;

namespace Validata.API.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.PostalCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
