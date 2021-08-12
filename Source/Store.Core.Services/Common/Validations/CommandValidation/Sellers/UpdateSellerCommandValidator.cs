using FluentValidation;
using Store.Core.Common.CustomValidators;
using Store.Core.Contracts.Enums;
using Store.Core.Internal.Sellers.Queries.UpdateSellerAsync;

namespace Store.Core.Common.Validations.CommandValidation.Sellers
{
    public class UpdateSellerCommandValidator : AbstractValidator<UpdateSellerCommand>
    {
        public UpdateSellerCommandValidator()
        {
            RuleFor(x => x.Name).ValidateName();
            RuleForEach(x => x.RecordType).ValidateType();
        }
    }
}