using FluentValidation;

namespace Store.Core.Common
{
    public static class PriceValidator
    {
        private static readonly string Message = "The price can only be positive, non-negative value!";
       
        public static IRuleBuilderOptions<T, decimal> ValidateRecordPrice<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder
                .NotNull()
                .GreaterThan(0)
                .WithMessage(Message);
        }
    }
}