
using Dapper;
using Microsoft.Data.Sqlite;
using Part_2_Lesson_6.CPU.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.CPU.Repostories
{
    public class CpuMetricsRepository:IRepositorie<CpuMetricsDto>
    {
        private const string connectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public  void Create(CpuMetricsDto item)
        {
            using (var connect=  new SqliteConnection(connectionString))
            {
                connect.Execute("INSERT INTO cpumetrics (value,time,agentId) VALUES(@value,@time,@agetnId)",
                    new
                    {
                        value = item.value,
                        time = item.Time,
                        agetn=item.agetnId
                       
                    });
            }
        }

        public void Delete(int id)
        {
            using(var connection=new SqliteConnection(connectionString))
            {
                connection.Execute("DELETE FROM cpumetrics WHERE @id=id",
                    new
                    {
                        id = id
                    });
            }
        }

        public IList<CpuMetricsDto> GetAll()
        {
           using(var connection=new SqliteConnection(connectionString))
            {
                return connection.Query<CpuMetricsDto>("SELECT id,Time,value,agentId FROM cpumetrics ").ToList();
            }
        }

        public CpuMetricsDto GEtById(int id)
        {
            using(var cinnect= new SqliteConnection(connectionString))
            {
                return cinnect.QuerySingle<CpuMetricsDto> ("SELECT id,Time,value,agentId FROM cpumetrics WHERE @id=id ",
                    new
                    {
                        id = id

                    });
            }
        }

        public void Update(CpuMetricsDto item)
        {
           using(var con=new SqliteConnection(connectionString))
            {
                con.Execute("UPDATE cpumetrics SET @value=value,@Time=Time,@agentId=agentId WHERE @id=id",
                    new
                    {
                        id = item.id,
                        Time = item.Time,
                        value = item.value,
                        agentId = item.agetnId
                    });
            }
        }
    }
}
