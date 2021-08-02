using FluentValidation;
using Store.Core.Contracts.Enums;
using Store.Core.Services.Sellers.Queries.UpdateSellerAsync;

namespace Store.Core.Common.Validations.CommandValidation.Sellers
{
    public class UpdateSellerCommandValidator : AbstractValidator<UpdateSellerCommand>
    {
        public UpdateSellerCommandValidator()
        {
            RuleFor(x => x.Name).ValidateName();
            RuleForEach(x => x.RecordType).NotEmpty()
                .WithMessage("Seller should have at least one RecordType!");
            
            RuleForEach(x => x.RecordType).IsInEnum();
            RuleForEach(x => x.RecordType).NotEqual(RecordType.Undefined)
                .WithMessage("Can't have undefined recordType!");
        }
    }
}