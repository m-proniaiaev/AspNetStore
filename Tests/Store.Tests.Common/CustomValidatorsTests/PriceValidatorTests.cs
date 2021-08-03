using FluentValidation;
using FluentValidation.TestHelper;
using Store.Core.Common.Validations;
using Store.Core.Common.Validations.CustomValidators;
using Xunit;

namespace Store.Tests.Common.CustomValidatorsTests
{
    public class PriceModel
    {
        public decimal Price { get; set; }
    }

    public class PriceValidator : AbstractValidator<PriceModel>
    {
        public PriceValidator()
        {
            RuleFor(x => x.Price).ValidateRecordPrice();
        }
    }

    public class PriceValidatorTests
    {
        [Fact]
        public void Price_CanNotBe_Negative()
        {
            var model = new PriceModel() { Price = -1 };
            var validator = new PriceValidator();
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }
        
        [Fact]
        public void Price_CanNotBe_Zero()
        {
            var model = new PriceModel() { Price = 0 };
            var validator = new PriceValidator();
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }
        
        [Fact]
        public void Price_CanBe_NonNegative()
        {
            var model = new PriceModel() { Price = 300 };
            var validator = new PriceValidator();
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}