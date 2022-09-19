
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EurekaDemoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly HttpClient _client;
		public ValuesController(IHttpClientFactory clientFactory)
		{
			_client = clientFactory.CreateClient("DiscoveryRoundRobin");
		}

		[HttpGet]
		public async Task<string> Get()
		{
			
			return await _client.GetStringAsync(new Uri("https://eurekademo/DadJoke"));
		}
	}
}
