using FluentValidation;
using Store.Core.Services.Common.CustomValidators;
using Store.Core.Services.Internal.Records.Queries.UpdateRecord;

namespace Store.Core.Services.Common.Validations.CommandValidation.Records
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