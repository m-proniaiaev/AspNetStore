using FluentValidation;
using Store.Core.Handlers.UpdateRecord;

namespace Store.Core.Common.CommandValidation
{
    public class UpdateRecordQueryValidator : AbstractValidator<UpdateRecordQuery>
    {
        public UpdateRecordQueryValidator()
        {
            RuleFor(x => x.Price)
                .ValidateRecordPrice();

            RuleFor(x => x.Name)
                .ValidateName();
        }
    }
}