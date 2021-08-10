namespace Store.Core.Services.AuthHost.PasswordProcessor
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}