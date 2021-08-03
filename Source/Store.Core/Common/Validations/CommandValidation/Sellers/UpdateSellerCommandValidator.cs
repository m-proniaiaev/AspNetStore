using FluentValidation;
using Store.Core.Common.Validations.CustomValidators;
using Store.Core.Contracts.Enums;
using Store.Core.Services.Sellers.Queries.UpdateSellerAsync;

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