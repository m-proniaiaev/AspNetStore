using FluentValidation;
using Store.Core.Handlers.UpdateRecord;

namespace Store.Core.Common.CommandValidation
{
    public class UpdateRecordQueryValidator : AbstractValidator<UpdateRecordQuery>
    {
        public UpdateRecordQueryValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("The price can only be positive, non-negative value!");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name can not be empty!");
        }
    }
}