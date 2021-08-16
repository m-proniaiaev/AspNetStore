using FluentValidation;
using Store.Core.Services.CustomValidators;

namespace Store.Core.Services.Authorization.Users.Commands.Update
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