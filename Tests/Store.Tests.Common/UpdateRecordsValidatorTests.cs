using FluentValidation.TestHelper;
using Store.Core.Common.Validations.CommandValidation.Records;
using Store.Core.Services.Records.Queries.UpdateRecord;
using Xunit;

namespace Store.Tests.Common
{
    public class UpdateRecordsValidatorTests
    {
        [Fact]
        public void Price_ShouldTrow_WhenZero()
        {
            var command = new UpdateRecordQuery
            {
                Price = 0
            };
            var validator = new UpdateRecordQueryValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }
        
        [Fact]
        public void Price_ShouldTrow_WhenNegative()
        {
            var command = new UpdateRecordQuery
            {
                Price = -1
            };
            var validator = new UpdateRecordQueryValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }
        
        [Fact]
        public void Name_ShouldTrow_WhenNull()
        {
            var command = new UpdateRecordQuery
            {
                Name = null
            };
            var validator = new UpdateRecordQueryValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Name_ShouldTrow_WhenEmpty()
        {
            var command = new UpdateRecordQuery
            {
                Name = ""
            };
            var validator = new UpdateRecordQueryValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ValidationIsCorrect_WhenData_Filled()
        {
            var command = new UpdateRecordQuery
            {
                Name = "Eren",
                Price = 993
            };
            var validator = new UpdateRecordQueryValidator();
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}