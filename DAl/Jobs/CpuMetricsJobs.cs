using Part_2_Lesson_6.CPU.Repostories;
using Part_2_Lesson_6.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl.Jobs
{
    public class CpuMetricsJobs : IJob//тут мы записываем что то при помощи репозитория (добавляем в репозиторий)
    {
        private ICpuRepositories _cpuRepository;
        private IMetricAgentClient metricAgent;
        public CpuMetricsJobs(ICpuRepositories repositories,IMetricAgentClient agentClient)
        {
            metricAgent = agentClient;
            _cpuRepository = repositories;
        }
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
