using Part_2_Lesson_6.CPU.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.CPU.Response
{
    public class CpuMetricResponse
    {
        public List<CpuMetricsDto> Metric { get; set; }
    }
}
