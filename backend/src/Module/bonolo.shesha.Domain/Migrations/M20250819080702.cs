using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250819080702)]
    public class M20250819080702 : Migration
    {

        public override void Up()
        {
            Create.Table("shesha_Municipalities")
                .WithIdAsGuid()
                .WithFullAuditColumns()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Type").AsString().Nullable()
                .WithColumn("Address").AsString().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("ContactNumber").AsString().Nullable()
                .WithForeignKeyColumn("ContactPersonId", "Core_Persons").Nullable();

            Alter.Table("Core_Persons")
                .AddForeignKeyColumn("MunicipalityId", "shesha_Municipalities");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
