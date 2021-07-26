using System;
using System.Threading;

namespace Store.Core.Extensions.CorrelationIdMiddleware
{
    public static class CorrelationProcessor
    {
        private static readonly AsyncLocal<string> _correlationId = new AsyncLocal<string>();

        public static string CorrelationId
        {
            get
            {
                if(string.IsNullOrEmpty(_correlationId.Value)) SetCorrelationId(Guid.NewGuid().ToString("D"));

                return _correlationId.Value;
            }
        }

        public static void SetCorrelationId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            
            if (id.Length >= 1024)
                throw new ArgumentException("Argument is too long.", nameof(id));

            _correlationId.Value = id;
        }
    }
}