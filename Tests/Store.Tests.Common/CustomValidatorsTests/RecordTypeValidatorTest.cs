using FluentValidation;
using FluentValidation.TestHelper;
using Store.Core.Common.Validations.CustomValidators;
using Store.Core.Contracts.Enums;
using Xunit;

namespace Store.Tests.Common.CustomValidatorsTests
{
    public class Model
    {
        public RecordType Type { get; set; }
    }

    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(x => x.Type).ValidateType();
        }
    }

    public class RecordTypeValidatorTest
    {
        [Fact]
        public void RecordType_CanNotBe_Undefined()
        {
            var model = new Model
            {
                Type = RecordType.Undefined 
            };
            var validator = new ModelValidator();
            var res = validator.TestValidate(model);
            res.ShouldHaveValidationErrorFor(x => x.Type);
        }
        
        [Fact]
        public void RecordType_CanNotBe_OutOfEnum()
        {
            var model = new Model
            {
                Type = (RecordType) 1337
            };
            var validator = new ModelValidator();
            var res = validator.TestValidate(model);
            res.ShouldHaveValidationErrorFor(x => x.Type);
        }
    }
}