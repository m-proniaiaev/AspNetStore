using FluentValidation;
using Store.Core.Services.Common.CustomValidators;
using Store.Core.Services.Internal.Records.Queries.CreateRecord;

namespace Store.Core.Services.Common.Validations.CommandValidation.Records
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