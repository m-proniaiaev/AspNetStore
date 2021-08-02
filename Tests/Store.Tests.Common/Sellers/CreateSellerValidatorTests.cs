using FluentValidation.TestHelper;
using Store.Core.Common.Validations.CommandValidation.Sellers;
using Store.Core.Contracts.Enums;
using Store.Core.Services.Sellers.Queries.CreateSeller;
using Xunit;

namespace Store.Tests.Common.Sellers
{
    public class CreateSellerValidatorTests
    {
        [Fact]
        public void ValidationFails_WhenRecordType_IsInvalid()
        {
            var model = new CreateSellerCommand()
            {
                RecordType = new [] { (RecordType) 999 }
            };
            var validator = new CreateSellerCommandValidator();
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.RecordType);
        }
        [Fact]
        public void ValidationFails_WhenRecordType_IsUndefined()
        {
            var model = new CreateSellerCommand()
            {
                RecordType = new [] {RecordType.Undefined}
            };
            var validator = new CreateSellerCommandValidator();
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.RecordType);
        }
    }
}