using Store.Core.Host;
using Store.Core.Host.HostBuilder;

namespace SomeStore
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return HostStarter.Start<Startup>(args, "some-store");
        }
    }
}
