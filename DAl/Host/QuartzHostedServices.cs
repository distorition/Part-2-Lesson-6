using Microsoft.Extensions.Hosting;
using Part_2_Lesson_6.CPU.Repostories;
using Part_2_Lesson_6.DAl.DTO;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl.Host
{
    public class QuartzHostedServices : IHostedService//запуск зада по расписанию 
    {
        private readonly ISchedulerFactory _sheldureFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSheldure> _jobSheldures;
        public QuartzHostedServices(ICpuRepositories repositories, ISchedulerFactory factory, IJobFactory jobfactory, IEnumerable<JobSheldure> jobs)
        {
            _sheldureFactory = factory;
            _jobFactory = jobfactory;
            _jobSheldures = jobs;
        }
        private IScheduler scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            scheduler = await _sheldureFactory.GetScheduler(cancellationToken);
            scheduler.JobFactory = _jobFactory;
            foreach (var jobScheldure in _jobSheldures)
            {
                var job = CreatJobDetail(jobScheldure);
                var trige = CreatTrigger(jobScheldure);
                await scheduler.ScheduleJob(job, trige, cancellationToken);
            }

            await scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreatJobDetail(JobSheldure jobSheldure)
        {
            var jobType = jobSheldure.JobType;
            return JobBuilder.Create(jobType).WithIdentity(jobType.FullName).WithDescription(jobType.Name).Build();
        }

        private static ITrigger CreatTrigger(JobSheldure jobSheldure)
        {
            return TriggerBuilder.Create().WithIdentity($"{jobSheldure.JobType.FullName}.trigger").WithCronSchedule(jobSheldure.CronExpression).WithDescription(jobSheldure.CronExpression).Build();
        }
    }
}
