using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.CPU.DTO
{
    public class CpuMetricsDto
    {
        public int id { get; set; }
        public int value { get; set; }

        public TimeSpan Time { get; set; }
        public CpuMetricsAgent agetnId { get; set; }
    }
}
