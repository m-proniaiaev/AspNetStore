using FluentValidation;
using Store.Core.Common.Validations.CustomValidators;
using Store.Core.Services.AuthHost.Services.Roles.Queries.CreateRole;

namespace Store.Core.Services.AuthHost.Common.Validations.CommandValidation.Roles
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleType).IsInEnum().NotEmpty();

            RuleFor(x => x.Name).ValidateName();
        }
    }
}