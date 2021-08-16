using FluentValidation;
using FluentValidation.TestHelper;
using Store.Core.Services.Common.CustomValidators;
using Xunit;

namespace Store.Tests.Common.CustomValidatorsTests
{
    public class NameModel
    {
        public string Name { get; set; }
    }

    public class NameValidator : AbstractValidator<NameModel>
    {
        public NameValidator()
        {
            RuleFor(x => x.Name).ValidateName();
        }
    }
    
    public class NameValidatorTests
    {
        [Fact]
        public void Name_CanNotBeNull()
        {
            var model = new NameModel {Name = null};
            var validator = new NameValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void Name_CanNotBeEmpty()
        {
            var model = new NameModel {Name = ""};
            var validator = new NameValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void Name_CanNotBeWhiteSpace()
        {
            var model = new NameModel {Name = " "};
            var validator = new NameValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void Name_CanBeChar()
        {
            var model = new NameModel {Name = "x"};
            var validator = new NameValidator();
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}