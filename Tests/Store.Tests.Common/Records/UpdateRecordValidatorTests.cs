using FluentValidation.TestHelper;
using Store.Core.Common.Validations.CommandValidation.Records;
using Store.Core.Internal.Records.Queries.UpdateRecord;
using Xunit;

namespace Store.Tests.Common.Records
{
    public class UpdateRecordValidatorTests
    {
        [Fact]
        public void Price_ShouldTrow_WhenZero()
        {
            var command = new UpdateRecordCommand
            {
                Price = 0
            };
            var validator = new UpdateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Price_ShouldTrow_WhenNegative()
        {
            var command = new UpdateRecordCommand
            {
                Price = -1
            };
            var validator = new UpdateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }
        
        [Fact]
        public void Name_ShouldTrow_WhenNull()
        {
            var command = new UpdateRecordCommand
            {
                Name = null
            };
            var validator = new UpdateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Name_ShouldTrow_WhenEmpty()
        {
            var command = new UpdateRecordCommand
            {
                Name = ""
            };
            var validator = new UpdateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ValidationIsCorrect_WhenData_Filled()
        {
            var command = new UpdateRecordCommand
            {
                Name = "Eren",
                Price = 993
            };
            var validator = new UpdateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}