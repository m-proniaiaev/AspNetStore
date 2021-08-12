using FluentValidation;
using Store.Core.Common.CustomValidators;
using Store.Core.Internal.Records.Queries.CreateRecord;

namespace Store.Core.Common.Validations.CommandValidation.Records
{
    public class CreateRecordCommandValidator : AbstractValidator<CreateRecordCommand>
    {
        public CreateRecordCommandValidator()
        {
            RuleFor(x => x.Price)
                .ValidateRecordPrice();

            RuleFor(x => x.Name)
                .ValidateName();

            RuleFor(x => x.Seller)
                .ValidateSeller();

            RuleFor(x => x.RecordType).ValidateType();
        }
    }
}