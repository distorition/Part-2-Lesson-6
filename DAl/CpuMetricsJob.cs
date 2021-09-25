using Part_2_Lesson_6.CPU.DTO;
using Part_2_Lesson_6.CPU.Repostories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl
{
    public class CpuMetricsJob : IJob//сбор метрики
    {
        private ICpuRepositories repositories;
        private PerformanceCounter counter;
        public CpuMetricsJob(ICpuRepositories cpu)
        {
            repositories = cpu;
            counter = new PerformanceCounter("Processor", " % Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuCountPresent = Convert.ToInt32(counter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            repositories.Create(new CpuMetricsDto { Time = time, value = cpuCountPresent });
            return Task.CompletedTask;
        }
    }
}
