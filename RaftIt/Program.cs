using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RaftIt
{
    class Program
    {
        static void Main(string[] args)
        {
            Args = args;
            var tester = Program.Services.GetService<Workers.SingletonWorker>();
            string id = Guid.NewGuid().ToString();
            tester.Run(id);
        }

        #region "DI"

        private static string[] Args { get; set; }

        private static IServiceProvider _services;

        public static IServiceProvider Services
        {
            get
            {
                if (_services == null)
                {
                    // Create service collection
                    var serviceCollection = new ServiceCollection();

                    // Build DI Stack inc. Logging, Configuration, and Application
                    ConfigureServices(serviceCollection);

                    // Create service provider
                    _services = serviceCollection.BuildServiceProvider();
                }
                return _services;
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            var configurationBuilder = new ConfigurationBuilder();

            if ((Args != null) && (Args.Length > 0)) configurationBuilder.AddCommandLine(Args);

            var config = configurationBuilder.Build();
            services.AddSingleton(config);

            // Logging
            services.AddLogging(loggingBuilder => {
                // This line must be 1st
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);

                // Console is generically cloud friendly
                loggingBuilder.AddConsole();
            });

            // App to run
            services.AddTransient<Workers.ISingletonWorker, Workers.SingletonWorker>();
        }

        #endregion

    }
}
