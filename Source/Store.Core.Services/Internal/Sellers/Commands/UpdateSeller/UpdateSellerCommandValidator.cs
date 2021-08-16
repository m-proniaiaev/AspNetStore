using FluentValidation;
using Store.Core.Services.CustomValidators;

namespace Store.Core.Services.Internal.Sellers.Commands.UpdateSeller
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