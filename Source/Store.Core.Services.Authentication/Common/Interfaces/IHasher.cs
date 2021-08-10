namespace Store.Core.Services.AuthHost.Common.Interfaces
{
    public interface IHasher
    {
        (string salt, string pass) Hash(string password);
  
        bool Check(string salt, string hash, string requestedPassword);
    }
}