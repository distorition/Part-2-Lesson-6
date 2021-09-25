using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Part_2_Lesson_6.DAl.DTO;
using Part_2_Lesson_6.DAl.Host;
using Part_2_Lesson_6.DAl.Jobs;
using Part_2_Lesson_6.Interfaces;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IMetricAgentClient, MetricAgentsClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            services.AddHttpClient();
            services.AddHttpClient<IMetricAgentClient, MetricAgentsClient>().AddTransientHttpErrorPolicy(io => io.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            services.AddControllers();

            services.AddFluentMigratorCore()
              .ConfigureRunner(rb => rb.AddSQLite().WithGlobalConnectionString(ConnectionString).ScanIn(typeof(Startup).Assembly).For.Migrations()).AddLogging(ld => ld.AddFluentMigratorConsole());
            services.AddSingleton<IJobFactory, SengeltonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricsJobs>();
            services.AddSingleton(new JobSheldure(jobType: typeof(CpuMetricsJobs), cronExpresion: "0/5 * * * * ?"));

            services.AddSingleton<HddMetricsJobs>();
            services.AddSingleton(new JobSheldure(jobType: typeof(HddMetricsJobs), cronExpresion: "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedServices>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Part_2_Lesson_6", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Part_2_Lesson_6 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
