using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery.Eureka.AppInfo;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EurekaDemoClient
{
	internal class ConsoleFetchService : IHostedService
	{
		private readonly DiscoveryClient _discoveryClient;
		private readonly ILogger<ConsoleFetchService> _logger;
		private readonly HttpClient _client;

		public ConsoleFetchService(IDiscoveryClient discoveryClient, ILogger<ConsoleFetchService> logger)
		{
			_discoveryClient = discoveryClient as DiscoveryClient;
			_logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			// Get what applications have been fetched
			var apps = _discoveryClient.Applications.GetRegisteredApplications();

			foreach(var app in apps)
			{
				var thing = app.Instances[0];

				_logger.LogInformation(@$"Name:{app.Name} and hostname: {thing.HostName} and 
					port:{thing.Port}");
				
			}

				var response = await _client.GetStreamAsync("Http://Eurekademo/api/dadjoke");

			
			// Try to find app with name "MyApp", it is registered in the Register console application
			//var app = apps.GetRegisteredApplication("EurekaDemo");
			//if (app != null)
			//{
			//	// Print the instance info, and then exit loop
			//	_logger.LogInformation("Successfully fetched application: {0} ", app);
			//}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _discoveryClient.ShutdownAsync();
		}
	}
}