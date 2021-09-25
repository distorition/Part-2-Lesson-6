using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl.Migrations
{
    public class MiegrtionAgents : Migration
    {
        public override void Down()
        {
            Delete.Table("Agents");
        }

        public override void Up()
        {
            Create.Table("Agents")
                .WithColumn("AgentsId").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentUrl");
        }
    }
}
