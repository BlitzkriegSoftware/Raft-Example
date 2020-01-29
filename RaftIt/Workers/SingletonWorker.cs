using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace RaftIt.Workers
{
    public class SingletonWorker : ISingletonWorker
    {
        #region "CTOR"
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _config;

        public SingletonWorker(ILogger<SingletonWorker> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }

        #endregion

        public void Run(string id)
        {
            _logger.LogInformation($"Worker {id}");
            Thread.Sleep(this.Sleep);
        }

        #region "Config"

        public const int Sleep_Default = 1000 * 60;

        public int Sleep
        {
            get
            {
                string c = _config["sleepms"];
                if (!int.TryParse(c, out int i)) i = Sleep_Default;
                return i;
            }
        }

        #endregion
    }
}
