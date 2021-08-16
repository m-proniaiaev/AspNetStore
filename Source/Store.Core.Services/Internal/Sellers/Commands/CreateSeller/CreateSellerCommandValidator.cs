using FluentValidation;
using Store.Core.Services.CustomValidators;

namespace Store.Core.Services.Internal.Sellers.Commands.CreateSeller
{
    public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerCommandValidator()
        {
            RuleFor(x => x.Name).ValidateName();

            RuleForEach(x => x.RecordType).ValidateType();
        }
    }
}