﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OcelotGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            string ip = config["ip"] ?? "127.0.0.1";
            string port = config["port"] ?? "5001";
            Console.WriteLine($"网关地址:ip={ip},port={port}");

            CreateWebHostBuilder(args).
                UseUrls($"http://{ip}:{port}").
                Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((hostingContext, builder) => {
                   builder.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
               }).UseStartup<Startup>();
    }
}
