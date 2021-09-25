using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Part_2_Lesson_6.CPU.Request;
using Part_2_Lesson_6.CPU.Response;
using Part_2_Lesson_6.Hdd.Request;
using Part_2_Lesson_6.Hdd.Responses;
using Part_2_Lesson_6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Part_2_Lesson_6
{
    public class MetricAgentsClient : IMetricAgentClient//тут мы реализуем сервисы сбора метрик
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger ilogger;
   
        private  MetricAgentsClient(HttpClient http,ILogger logger)
        {
            _httpClient = http;
            ilogger = logger;
        }
        public CpuMetricResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            var fromParametr = request.fromTime.TotalSeconds;
            var toParametr = request.toTime.TotalSeconds;
            var httpRequest=new HttpRequestMessage(HttpMethod.Get, $"{request.id}/api/cpumetrics/from/{fromParametr}/to/{toParametr}");
            try
            {
                HttpResponseMessage respons = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = respons.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<CpuMetricResponse>(responseStream).Result;
            }
            catch(Exception ex)
            {
                ilogger.LogError(ex.Message);
            }
            return null;

        }

        public HddMetricsRespnse GetHddMetrics(HddMetricsRequest request)
        {
            var fromParametr = request.fromTime.TotalSeconds;
            var toParametr = request.toTime.TotalSeconds;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.id}/api/hddmetrics/from/{fromParametr}/to/{toParametr}");
            try
            {
                HttpResponseMessage respons = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = respons.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<HddMetricsRespnse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex.Message);
            }
            return null;
        }
    }
}
