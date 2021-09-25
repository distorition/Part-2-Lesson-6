using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part_2_Lesson_6.CPU.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuMetricController : ControllerBase//получаем ответы от сервиса при помощи HttpClient

    {
        private IHttpClientFactory httpClientFactory;
        public CpuMetricController(IHttpClientFactory httpClient)
        {
            httpClientFactory = httpClient;
        }

        [HttpGet("agetid/{agetnid}/fromTime/{fromTime}/toTime/{toTime}")]
        public IActionResult GetMetricCpu([FromRoute] int agentid,[FromRoute] TimeSpan fromTime,[FromRoute] TimeSpan toTime)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:50343/api/cpumetrics/from/1/to/999999?var=val&var1=val1");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            var client = httpClientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if(response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricRespone = JsonSerializer.DeserializeAsync<CpuMetricResponse>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;

            }
            else
            {

            }
            return Ok();
        }
        public IActionResult AllCpuMetrics([FromRoute] TimeSpan formTime,[FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
