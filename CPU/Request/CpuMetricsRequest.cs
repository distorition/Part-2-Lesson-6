using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.CPU.Request
{
    public class CpuMetricsRequest
    {
        public TimeSpan fromTime { get; set; }
        public TimeSpan toTime { get; set; }
        public Uri id { get; set; }
    }
}
