﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ThirdPartyMediator
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
           .UseKestrel()
           .UseStartup<Startup>()
           .Build();

            host.Run();
        }
    }
    
}
