using FluentValidation;
using Store.Core.Services.Authorization.Roles.Queries.UpdateRole;
using Store.Core.Services.Common.CustomValidators;

namespace Store.Core.Services.Common.Validations.CommandValidation.Roles
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