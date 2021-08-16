using FluentValidation;
using Store.Core.Services.CustomValidators;

namespace Store.Core.Services.Internal.Records.Commands.CreateRecord
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