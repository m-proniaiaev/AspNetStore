using FluentValidation;
using Store.Core.Services.Records.Queries.CreateRecord;

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
        }
    }
}