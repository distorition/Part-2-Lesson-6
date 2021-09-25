using Part_2_Lesson_6.Hdd.Repositories;
using Part_2_Lesson_6.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl.Jobs
{
    public class HddMetricsJobs:IJob
    {
        public IHddRepositories hdd;
        public IMetricAgentClient client;

        public HddMetricsJobs(IHddRepositories repositories,IMetricAgentClient metric)
        {
            hdd = repositories;
            client = metric;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
