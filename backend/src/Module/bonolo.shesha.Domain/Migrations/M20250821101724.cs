using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250821101724)]
    public class M20250821101724 : Migration
    {
        public override void Up()
        {
            Alter.Table("Core_Persons")
                .AddColumn("shesha_SupervisorId").AsGuid().Nullable();

            Create.ForeignKey("FK_CorePersons_Supervisor")
                .FromTable("Core_Persons").ForeignColumn("shesha_SupervisorId")
                .ToTable("Core_Persons").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_CorePersons_Supervisor").OnTable("Core_Persons");

            Delete.Column("shesha_SupervisorId").FromTable("Core_Persons");
        }
    }
}
