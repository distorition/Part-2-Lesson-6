using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part_2_Lesson_6.Hdd.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.Hdd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        public IHttpClientFactory httpClient;
        public HddMetricsController(IHttpClientFactory factory)
        {
            httpClient = factory;
        }

        [HttpGet("agetid/{agetnid}/fromTime/{fromTime}/toTime/{toTime}")]
        public IActionResult GetHddMetrics([FromRoute] int agentid,[FromRoute] TimeSpan toTime,[FromRoute] TimeSpan fromTime)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:50343/api/hddmetrics/from/1/to/999999?var=val&var1=val1");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            var client = httpClient.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if(response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync<HddMetricsRespnse>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            }
            return Ok();

        }
    }
}
