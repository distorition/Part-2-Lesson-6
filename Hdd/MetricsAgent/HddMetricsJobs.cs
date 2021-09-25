using Part_2_Lesson_6.Hdd.DTO;
using Part_2_Lesson_6.Hdd.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.Hdd.MetricsAgent
{
    public class HddMetricsJobs:IJob
    {
        private IHddRepositories hdd;
        private PerformanceCounter count;
        public HddMetricsJobs(IHddRepositories repositories)
        {
            hdd = repositories;
            count = new PerformanceCounter("Hdd", "Available MBytes");

        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddCounProces = Convert.ToInt32(count.NextValue());
            var tiem = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            hdd.Create(new HddMetricsDto { value = hddCounProces, Time = tiem });
            return Task.CompletedTask;
        }
    }
}
