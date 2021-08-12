namespace Store.Core.Authorization.PasswordProcessor
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}