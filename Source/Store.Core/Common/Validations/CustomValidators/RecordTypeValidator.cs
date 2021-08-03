using FluentValidation;
using Store.Core.Contracts.Enums;

namespace Store.Core.Common.Validations.CustomValidators
{
    public static class RecordTypeValidator
    {
        private static readonly string Message = "Record type can not be undefined!";
       
        public static IRuleBuilderOptions<T, RecordType> ValidateType<T>(this IRuleBuilder<T, RecordType> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Specify at least one RecordType!")
                .IsInEnum() 
                .WithMessage(Message);
        }
    }
}