using FluentValidation.TestHelper;
using Store.Core.Common.Validations.CommandValidation.Records;
using Store.Core.Services.Records.Queries.CreateRecord;
using Xunit;

namespace Store.Tests.Common.Records
{
    public class CreateRecordValidatorTests
    {
        [Fact]
        public void Price_ShouldTrow_WhenZero()
        {
            var command = new CreateRecordCommand()
            {
                Price = 0
            };
            var validator = new CreateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Price_ShouldTrow_WhenNegative()
        {
            var command = new CreateRecordCommand
            {
                Price = -1
            };
            var validator = new CreateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }
        
        [Fact]
        public void Name_ShouldTrow_WhenNull()
        {
            var command = new CreateRecordCommand
            {
                Name = null
            };
            var validator = new CreateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Name_ShouldTrow_WhenEmpty()
        {
            var command = new CreateRecordCommand
            {
                Name = ""
            };
            var validator = new CreateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ValidationIsCorrect_WhenData_Filled()
        {
            var command = new CreateRecordCommand
            {
                Name = "Eren",
                Price = 993
            };
            var validator = new CreateRecordCommandValidator();
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}