using FluentValidation;
using Store.Core.Services.Authorization.Roles.Queries.CreateRole;
using Store.Core.Services.Common.CustomValidators;

namespace Store.Core.Services.Common.Validations.CommandValidation.Roles
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