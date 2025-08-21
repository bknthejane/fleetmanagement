using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250820215031)]
    public class M20250820215031 : Migration
    {
        public override void Up()
        {
            Create.Table("shesha_Vehicles")
                .WithIdAsGuid()
                .WithFullAuditColumns()
                .WithColumn("FleetNumber").AsString(50).NotNullable()
                .WithColumn("RegistrationNumber").AsString(50).NotNullable()
                .WithColumn("Model").AsString(50).Nullable()
                .WithColumn("Make").AsString(50).Nullable()
                .WithColumn("LicenseExpiry").AsDateTime().NotNullable()
                .WithColumn("IsActive").AsBoolean().WithDefaultValue(true)
                .WithColumn("MunicipalityId").AsGuid().NotNullable()
                .ForeignKey("FK_Vehicles_Municipality", "shesha_Municipalities", "Id")
                .WithColumn("MunicipalityName").AsString(100).Nullable()
                .WithColumn("AssignedDriverId").AsGuid().Nullable()
                .WithColumn("AssignedDriverName").AsString(100).Nullable();
        }

        public override void Down()
        {
            Delete.Table("shesha_Vehicles");
        }

    }
}
