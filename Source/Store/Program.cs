using System;
using Store.Core.Host;

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
