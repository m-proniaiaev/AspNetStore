using FluentValidation;
using Store.Core.Services.Authorization.Users.Commands;
using Store.Core.Services.Common.CustomValidators;

namespace Store.Core.Services.Common.Validations.CommandValidation.Users
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