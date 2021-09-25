using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.DAl.Migrations
{
    public class MigrationHdd : Migration
    {
        public override void Down()
        {
            Delete.Table("hddmetrics");
        }

        public override void Up()
        {
            Create.Table("hddmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt64()
                .WithColumn("time").AsInt64();
        }
    }
}
