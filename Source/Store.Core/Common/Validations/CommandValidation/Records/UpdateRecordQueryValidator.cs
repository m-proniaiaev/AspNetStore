using FluentValidation;
using Store.Core.Services.Records.Queries.UpdateRecord;

namespace Store.Core.Common.Validations.CommandValidation.Records
{
    public class UpdateRecordQueryValidator : AbstractValidator<UpdateRecordCommand>
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