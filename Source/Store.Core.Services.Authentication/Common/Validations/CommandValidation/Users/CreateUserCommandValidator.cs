using FluentValidation;
using Store.Core.Common.Validations.CustomValidators;
using Store.Core.Services.AuthHost.Services.Users.Query;

namespace Store.Core.Services.AuthHost.Common.Validations.CommandValidation.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).ValidateName();
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}