namespace Store.Core.Host.Authorization.JWT
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public int AccessTokenExpiration { get; set; }
        //public int RefreshTokenExpiration { get; set; }
    }
}