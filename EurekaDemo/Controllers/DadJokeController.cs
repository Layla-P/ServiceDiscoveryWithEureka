using EurekaDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EurekaDemo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DadJokeController : ControllerBase
	{
		

		private readonly ILogger<DadJokeController> _logger;
		private readonly HttpClient _client;

		public DadJokeController(ILogger<DadJokeController> logger, HttpClient client)
		{
			_logger = logger;
			_client = client;
		}

		[HttpGet]
		public async Task<string> Get()
		{
			_client.DefaultRequestHeaders.Add("Accept", "application/json");

			var joke = await _client.GetFromJsonAsync<DadJokeResponse>("https://icanhazdadjoke.com/");

			return joke.Joke;
		}
	}
}
