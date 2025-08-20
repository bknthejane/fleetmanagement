using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250820123024)]
    public class M20250820123024 : Migration
    {
        public override void Up()
        {
            Alter.Table("Core_Persons")
                .AddColumn("Department").AsString().Nullable()
                .AddForeignKeyColumn("Supervisor_MunicipalityId", "shesha_Municipalities").Nullable();
        }

        public override void Down()
        {
            Delete.Column("Department").FromTable("Core_Persons");
            Delete.ForeignKey("FK_CorePersons_Supervisor_MunicipalityId_shesha_Municipalities")
                .OnTable("Core_Persons");
        }
    }
}
