using Dapper;
using Microsoft.Data.Sqlite;
using Part_2_Lesson_6.CPU.Repostories;
using Part_2_Lesson_6.Hdd.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.Hdd.Repositories
{
    public class HddMetricsRepository : IRepositorie<HddMetricsDto>
    {
        private const string connectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";
        
        public void Create(HddMetricsDto item)
        {
            using (var conncet= new SqliteConnection(connectionString))
            {
                conncet.Execute("INSERT INTO hddmetrics (value,time,id) VALUES(@value,@id,@time)",
                    new
                    {
                        value = item.value,
                        time=item.Time,
                        id=item.id

                    });
            }
        }

        public void Delete(int id)
        {
            using (var conect=new SqliteConnection(connectionString))
            {
                conect.Execute("DELETE FROM hddmetrics WHERE @id=id",
                    new
                    {
                        id=id
                    });
            }
        }

        public IList<HddMetricsDto> GetAll()
        {
            using(var conect= new SqliteConnection(connectionString))
            {
                return conect.Query<HddMetricsDto>("SELECT id, Time, value, agentId FROM hddmetrics").ToList();
            }
        }

        public HddMetricsDto GEtById(int id)
        {
            using ( var conect= new SqliteConnection(connectionString))
            {
                return conect.QuerySingle<HddMetricsDto>("SELECT id,Time,value,agentId FROM hddmetrics WHERE @id=id ",
                    new
                    {
                        id = id
                    });
            }
        }

        public void Update(HddMetricsDto item)
        {
            using(var connection=new SqliteConnection(connectionString))
            {
                connection.Execute("UPDATE hddmetrics SET @value=value,@Time=Time,@agentId=agentId WHERE @id=id",
                    new
                    {
                        id = item.id,
                        Time = item.Time,
                        value = item.value,

                    });
            }
        }
    }
}
