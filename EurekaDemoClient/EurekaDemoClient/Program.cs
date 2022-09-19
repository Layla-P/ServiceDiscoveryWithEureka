using System;
using System.Threading.Tasks;
using EurekaDemoClient.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace EurekaDemoClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			await Host.CreateDefaultBuilder(args)
			   .ConfigureServices(services =>
			   {


				services.AddHttpClient("dadjoke", c =>
				{
					   c.BaseAddress = new Uri("http://EurekaDemo/api/dadjoke/");
				})
					.AddServiceDiscovery()
					.AddTypedClient<DadJokeClient>(); 

				   services.AddServiceDiscovery(client => client.UseEureka());
				   services.AddHostedService<ConsoleFetchService>();
			   })
			   .RunConsoleAsync();
		}
	}
}
