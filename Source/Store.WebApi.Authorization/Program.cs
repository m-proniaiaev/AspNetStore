using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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