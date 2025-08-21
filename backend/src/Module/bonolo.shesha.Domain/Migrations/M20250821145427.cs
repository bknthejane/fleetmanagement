using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250821145427)]
    public class M20250821145427 : Migration
    {
        public override void Up()
        {
            // Add columns
            Alter.Table("Core_Persons")
                .AddColumn("shesha_AssignedJobCardId").AsGuid().Nullable()
                .AddColumn("shesha_AssignedJobCardNumber").AsString(50).Nullable();

            // Add FK for AssignedJobCardId
            Create.ForeignKey("FK_CorePersons_AssignedJobCard")
                .FromTable("Core_Persons").ForeignColumn("shesha_AssignedJobCardId")
                .ToTable("shesha_JobCards").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_CorePersons_AssignedJobCard").OnTable("Core_Persons");

            Delete.Column("shesha_AssignedJobCardId").FromTable("Core_Persons");
            Delete.Column("shesha_AssignedJobCardNumber").FromTable("Core_Persons");
        }
    }
}
