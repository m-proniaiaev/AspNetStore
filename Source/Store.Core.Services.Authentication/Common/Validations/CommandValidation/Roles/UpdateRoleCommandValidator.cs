using FluentValidation;
using Store.Core.Common.Validations.CustomValidators;
using Store.Core.Services.AuthHost.Services.Roles.Queries.UpdateRole;

namespace Store.Core.Services.AuthHost.Common.Validations.CommandValidation.Roles
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