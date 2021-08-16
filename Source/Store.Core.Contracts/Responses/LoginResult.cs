namespace Store.Core.Contracts.Responses
{
    public class LoginResult
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string Type { get; set; }
    }
}