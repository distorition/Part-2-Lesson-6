using Part_2_Lesson_6.CPU.Request;
using Part_2_Lesson_6.CPU.Response;
using Part_2_Lesson_6.Hdd.Request;
using Part_2_Lesson_6.Hdd.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.Interfaces
{
    public interface IMetricAgentClient//сервис для сбора метрик 
    {

        CpuMetricResponse GetCpuMetrics(CpuMetricsRequest request);
        HddMetricsRespnse GetHddMetrics(HddMetricsRequest request);

}

}
