using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlickrWebService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlickrWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        const string ApiUrl = "https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=8cdc0ed72eb971dfca9b3d3edcdfe764&tags=fourmi&format=json&nojsoncallback=1";

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        async Task<string> GetPhotoAsync(string ApiUrl)
        {
            //string message = "";
            HttpClient client = new HttpClient();
            PhotoF photo = null;
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            //if (response.IsSuccessStatusCode)
            //{
            //_logger.LogInformation(response.Content.ReadAsStringAsync());
            var message = response.Content.ReadAsStringAsync();
                //photo = await response.Content.ReadAsAsync<Photo>();
            //}
            return await message;
        }
    }
}
