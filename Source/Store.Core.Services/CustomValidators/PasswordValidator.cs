using FluentValidation;

namespace Store.Core.Services.CustomValidators
{
    public static class PasswordValidator
    {
        private const string PassRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$";
        
        private static readonly string Message = "Password should be at least 8 symbols long, contain symbols and latin letters!";
       
        public static IRuleBuilderOptions<T, string> ValidatePassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Matches(PassRegex)
                .WithMessage(Message);
        }
    }
}