using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EurekaDemoApi
{
	internal class ConsoleFetchService : IHostedService
	{
		private readonly DiscoveryClient _discoveryClient;
		private readonly ILogger<ConsoleFetchService> _logger;
		private readonly HttpClient _httpClient;

		public ConsoleFetchService(IDiscoveryClient discoveryClient, HttpClient client, ILogger<ConsoleFetchService> logger)
		{
			_discoveryClient = discoveryClient as DiscoveryClient;
			_httpClient = client;
			_logger = logger;
		}


		public async Task StartAsync(CancellationToken cancellationToken)
		{

			var apps = _discoveryClient.Applications.GetRegisteredApplications();

			var count = 0;

			_logger.LogInformation(@$"There are {count} apps registered with Eureka");


			foreach (var app in apps)
			{
				var firstInstance = app.Instances[0];
				_logger.LogInformation(@$"Name:{app.Name} and hostname: {firstInstance.HostName} and 
		port:{firstInstance.SecurePort}");
				count++;
				try
				{
					if (app.Name.ToLower() == "dadjokeapi" && !string.IsNullOrEmpty(firstInstance.HomePageUrl))
					{
						_logger.LogError($"{firstInstance.HomePageUrl}dadjoke");
						var response = await _httpClient.GetStringAsync($"{firstInstance.HomePageUrl}dadjoke");
						_logger.LogInformation(response);

					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
				}

			}


			
		}


		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _discoveryClient.ShutdownAsync();
		}

	}
}