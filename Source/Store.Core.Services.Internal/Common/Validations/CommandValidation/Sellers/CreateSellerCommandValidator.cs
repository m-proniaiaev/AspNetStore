using FluentValidation;
using Store.Core.Common.Validations.CustomValidators;
using Store.Core.Contracts.Enums;
using Store.Core.Services.Sellers.Queries.CreateSeller;

namespace Store.Core.Common.Validations.CommandValidation.Sellers
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