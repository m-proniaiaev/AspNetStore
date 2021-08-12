using FluentValidation;
using Store.Core.Authorization.Services.Roles.Queries.UpdateRole;
using Store.Core.Common.CustomValidators;

namespace Store.Core.Common.Validations.CommandValidation.Roles
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.RoleType).IsInEnum().NotEmpty();

            RuleFor(x => x.Name).ValidateName();
        }
    }
}