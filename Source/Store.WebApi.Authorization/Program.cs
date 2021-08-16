using Store.Core.Host.HostBuilder;

namespace Store.WebApi.Authorization
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return HostStarter.Start<Startup>(args, "some-store");
        }
    }
}