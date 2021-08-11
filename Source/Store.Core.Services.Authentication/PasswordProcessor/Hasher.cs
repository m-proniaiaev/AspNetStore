using System;
using System.Linq;
using System.Security.Cryptography;
using Store.Core.Services.AuthHost.Common.Interfaces;

namespace Store.Core.Services.AuthHost.PasswordProcessor
{
    public class Hasher : IHasher
    {
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit
        private readonly HashingOptions _options;

        public Hasher(HashingOptions options)
        {
            _options = options;
        }
        
        public (string salt, string pass) Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                _options.Iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return ($"{salt}", $"{_options.Iterations}.{key}");
        }

        public bool CheckHash(string salt, string hash, string requestedPassword)
        {
            var passHashValues = hash.Split('.', 2);

            if (passHashValues.Length != 2)
                throw new ArgumentException("Invalid hash!");
            
            var iterations = Convert.ToInt32(passHashValues[0]);
            
            if (iterations != _options.Iterations)
                throw new ArgumentException("Invalid hash!");
            
            var key = Convert.FromBase64String(passHashValues[2]);
            var saltValue = Convert.FromBase64String(salt);

            using var algorithm = new Rfc2898DeriveBytes(
                                    requestedPassword,
                                    saltValue,
                                    iterations,
                                    HashAlgorithmName.SHA256);
            
            var keyToCheck = algorithm.GetBytes(KeySize);

            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}