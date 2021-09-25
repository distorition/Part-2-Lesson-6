using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl.Jobs
{
    public class SengeltonJobFactory:IJobFactory//планировщик
    {
        private IServiceProvider serviceProvider;
        public SengeltonJobFactory(IServiceProvider service)
        {
            serviceProvider = service;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            
        }

     }
}
