using FluentValidation;
using Store.Core.Services.CustomValidators;

namespace Store.Core.Services.Authorization.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).ValidateName();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.Password).ValidatePassword();
        }
    }
}