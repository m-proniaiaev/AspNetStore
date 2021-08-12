using FluentValidation;
using Store.Core.Services.Common.CustomValidators;
using Store.Core.Services.Internal.Sellers.Queries.CreateSeller;

namespace Store.Core.Services.Common.Validations.CommandValidation.Sellers
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