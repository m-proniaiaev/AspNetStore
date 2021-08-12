using FluentValidation;
using Store.Core.Services.Authorization.Users.Commands;
using Store.Core.Services.Common.CustomValidators;

namespace Store.Core.Services.Common.Validations.CommandValidation.Users
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