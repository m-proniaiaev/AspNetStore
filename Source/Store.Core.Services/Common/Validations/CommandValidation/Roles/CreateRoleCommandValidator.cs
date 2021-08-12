using FluentValidation;
using Store.Core.Authorization.Services.Roles.Queries.CreateRole;
using Store.Core.Common.CustomValidators;

namespace Store.Core.Common.Validations.CommandValidation.Roles
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