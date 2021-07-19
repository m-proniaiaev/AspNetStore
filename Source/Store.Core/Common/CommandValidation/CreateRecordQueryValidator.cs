using FluentValidation;
using Store.Core.Handlers.CreateRecord;

namespace Store.Core.Common.CommandValidation
{
    public class CreateRecordQueryValidator : AbstractValidator<CreateRecordQuery>
    {
        public CreateRecordQueryValidator()
        {
            RuleFor(x => x.Price)
                .ValidateRecordPrice();

            RuleFor(x => x.Name)
                .ValidateName();
        }
    }
}