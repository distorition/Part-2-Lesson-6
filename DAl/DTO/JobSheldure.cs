using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl.DTO
{
    public class JobSheldure//тут храним расписание запуска когда будет собирать метрики 
    {
        public JobSheldure(Type jobType,string cronExpresion)
        {
            JobType = jobType;
            CronExpression = cronExpresion;
        }

        public Type JobType { get; set; }
        public string CronExpression { get; set; }
    }
}
