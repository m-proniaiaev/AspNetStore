using FluentValidation;
using Store.Core.Services.Sellers.Queries.CreateSeller;

namespace Store.Core.Common.Validations.CommandValidation.Sellers
{
    public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerCommandValidator()
        {
            RuleFor(x => x.Name).ValidateName();
            RuleFor(x => x.RecordType).IsInEnum();
        }
    }
}