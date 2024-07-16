using FluentValidation;
using Validata.Infrastructure.Handlers.CustomerHandlers;

namespace Validata.API.Validators
{
    public class UpdateCustomerCommandValidator: AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
