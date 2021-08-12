using FluentValidation;

namespace Store.Core.Services.Common.CustomValidators
{
    public static class SellerValidator
    {
        private static readonly string Message = "Seller can not be empty!";
       
            public static IRuleBuilderOptions<T, string> ValidateSeller<T>(this IRuleBuilder<T, string> ruleBuilder)
            {
                return ruleBuilder
                    .NotEmpty()
                    .WithMessage(Message);
            }
    }
}