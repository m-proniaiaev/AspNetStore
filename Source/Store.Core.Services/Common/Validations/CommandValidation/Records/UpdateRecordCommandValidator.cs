using FluentValidation;
using Store.Core.Common.CustomValidators;
using Store.Core.Internal.Records.Queries.UpdateRecord;

namespace Store.Core.Common.Validations.CommandValidation.Records
{
    public class UpdateRecordCommandValidator : AbstractValidator<UpdateRecordCommand>
    {
        public UpdateRecordCommandValidator()
        {
            RuleFor(x => x.Price)
                .ValidateRecordPrice();

            RuleFor(x => x.Name)
                .ValidateName();
        }
    }
}