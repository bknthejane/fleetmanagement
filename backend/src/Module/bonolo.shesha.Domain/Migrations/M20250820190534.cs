using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250820190534)]
    public class M20250820190534 : Migration
    {
        public override void Up()
        {
            Alter.Table("Core_Persons")
                .AddColumn("shesha_LicenseNumber").AsString().Nullable()
                .AddColumn("shesha_LicenseExpiryDate").AsDateTime().Nullable()
                .AddForeignKeyColumn("Driver_MunicipalityId", "shesha_Municipalities").Nullable();
        }

        public override void Down()
        {
            Delete.Column("shesha_LicenseNumber").FromTable("Core_Persons");
            Delete.Column("shesha_LicenseExpiryDate").FromTable("Core_Persons");

            Delete.ForeignKey("FK_CorePersons_Driver_MunicipalityId_shesha_Municipalities")
                .OnTable("Core_Persons");
        }
    }
}
