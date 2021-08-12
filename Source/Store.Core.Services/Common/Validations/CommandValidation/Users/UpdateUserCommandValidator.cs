using FluentValidation;
using Store.Core.Services.Authorization.Users.Commands.Update;
using Store.Core.Services.Common.CustomValidators;

namespace Store.Core.Services.Common.Validations.CommandValidation.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name).ValidateName();
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}