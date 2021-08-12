using FluentValidation;
using Store.Core.Services.Common.CustomValidators;
using Store.Core.Services.Internal.Sellers.Queries.UpdateSellerAsync;

namespace Store.Core.Services.Common.Validations.CommandValidation.Sellers
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