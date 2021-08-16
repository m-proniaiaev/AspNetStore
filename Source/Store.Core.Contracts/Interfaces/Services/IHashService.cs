namespace Store.Core.Contracts.Interfaces.Services
{
    public interface IHashService
    {
        (string salt, string hash) Hash(string password);
  
        bool CheckHash(string salt, string hash, string requestedPassword);
    }
}