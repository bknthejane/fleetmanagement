using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250821153343)]
    public class M20250821153343 : Migration
    {
        public override void Up()
        {
            Alter.Table("shesha_Incidents")
                .AddColumn("JobCardId").AsGuid().Nullable();
        }

        public override void Down()
        {
            Delete.Column("JobCardId").FromTable("shesha_Incidents");
        }
    }
}
