using FluentValidation;
using Store.Core.Services.CustomValidators;

namespace Store.Core.Services.Internal.Records.Commands.UpdateRecord
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