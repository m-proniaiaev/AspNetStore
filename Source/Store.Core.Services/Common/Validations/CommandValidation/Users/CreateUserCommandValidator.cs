using FluentValidation;
using Store.Core.Authorization.Services.Users.Query;
using Store.Core.Common.CustomValidators;

namespace Store.Core.Common.Validations.CommandValidation.Users
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