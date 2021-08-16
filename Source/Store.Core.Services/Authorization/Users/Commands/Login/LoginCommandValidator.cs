using FluentValidation;
using Store.Core.Services.CustomValidators;

namespace Store.Core.Services.Authorization.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).ValidateName();
            RuleFor(x => x.Password).ValidatePassword();
        }
    }
}