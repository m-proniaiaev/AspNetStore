namespace Store.Core.Services.Common.Interfaces
{
    public interface IHasher
    {
        (string salt, string hash) Hash(string password);
  
        bool CheckHash(string salt, string hash, string requestedPassword);
    }
}