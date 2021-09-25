using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Part_2_Lesson_6.CPU.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.CPU.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetMetricsFromAgents : ControllerBase//тут мы обращаеся за запросом к HttpClient
    {
        private readonly MetricAgentsClient metricAgentsClient;
        private readonly ILogger ilogger;
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            // логируем, что мы пошли в соседний сервис
            ilogger.LogInformation("starn new request");
            // обращение в сервис
            var metrics = metricAgentsClient.GetCpuMetrics(new CpuMetricsRequest
            {
                fromTime = fromTime,
                toTime = toTime
            });
            return Ok(metrics);
        }
    }
}
