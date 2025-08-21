using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250821143552)]
    public class M20250821143552 : Migration
    {
        public override void Up()
        {
            Create.Table("shesha_Incidents")
                .WithIdAsGuid()
                .WithFullAuditColumns()
                .WithColumn("Description").AsString(int.MaxValue).Nullable()
                .WithColumn("IncidentType").AsString(50).Nullable()
                .WithColumn("Department").AsString(50).Nullable()
                .WithColumn("Status").AsString(50).Nullable()
                .WithColumn("DateReported").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("VehicleId").AsGuid().NotNullable()
                .WithColumn("DriverId").AsGuid().NotNullable()
                .WithColumn("MunicipalityId").AsGuid().NotNullable()
                .WithColumn("MunicipalityName").AsString(100).Nullable();

            Create.Table("shesha_JobCards")
                .WithIdAsGuid()
                .WithFullAuditColumns()
                .WithColumn("JobCardNumber").AsString(50).NotNullable()
                .WithColumn("DateOpened").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("Status").AsString(50).NotNullable().WithDefaultValue("Open")
                .WithColumn("Notes").AsString(int.MaxValue).Nullable()
                .WithColumn("Priority").AsString(50).NotNullable().WithDefaultValue("Medium")
                .WithColumn("DateCompleted").AsDateTime().Nullable()
                .WithColumn("IncidentId").AsGuid().NotNullable()
                .WithColumn("VehicleId").AsGuid().NotNullable()
                .WithColumn("DriverId").AsGuid().NotNullable()
                .WithColumn("SupervisorId").AsGuid().Nullable()
                .WithColumn("AssignedMechanicId").AsGuid().Nullable()
                .WithColumn("AssignedMechanicName").AsString(100).Nullable();
        }

        public override void Down()
        {
            Delete.Table("shesha_JobCards");
            Delete.Table("shesha_Incidents");
        }
    }
}
