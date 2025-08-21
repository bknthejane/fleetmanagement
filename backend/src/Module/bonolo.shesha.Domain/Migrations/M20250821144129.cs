using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250821144129)]
    public class M20250821144129 : Migration
    {
        public override void Up()
        {
            Create.ForeignKey("FK_Incidents_Vehicle")
                .FromTable("shesha_Incidents").ForeignColumn("VehicleId")
                .ToTable("shesha_Vehicles").PrimaryColumn("Id");

            Create.ForeignKey("FK_Incidents_Driver")
                .FromTable("shesha_Incidents").ForeignColumn("DriverId")
                .ToTable("Core_Persons").PrimaryColumn("Id");

            Create.ForeignKey("FK_Incidents_Municipality")
                .FromTable("shesha_Incidents").ForeignColumn("MunicipalityId")
                .ToTable("shesha_Municipalities").PrimaryColumn("Id");

            Create.ForeignKey("FK_JobCards_Incident")
                .FromTable("shesha_JobCards").ForeignColumn("IncidentId")
                .ToTable("shesha_Incidents").PrimaryColumn("Id");

            Create.ForeignKey("FK_JobCards_Vehicle")
                .FromTable("shesha_JobCards").ForeignColumn("VehicleId")
                .ToTable("shesha_Vehicles").PrimaryColumn("Id");

            Create.ForeignKey("FK_JobCards_Driver")
                .FromTable("shesha_JobCards").ForeignColumn("DriverId")
                .ToTable("Core_Persons").PrimaryColumn("Id");

            Create.ForeignKey("FK_JobCards_Supervisor")
                .FromTable("shesha_JobCards").ForeignColumn("SupervisorId")
                .ToTable("Core_Persons").PrimaryColumn("Id");

            Create.ForeignKey("FK_JobCards_AssignedMechanic")
                .FromTable("shesha_JobCards").ForeignColumn("AssignedMechanicId")
                .ToTable("Core_Persons").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_JobCards_AssignedMechanic").OnTable("shesha_JobCards");
            Delete.ForeignKey("FK_JobCards_Supervisor").OnTable("shesha_JobCards");
            Delete.ForeignKey("FK_JobCards_Driver").OnTable("shesha_JobCards");
            Delete.ForeignKey("FK_JobCards_Vehicle").OnTable("shesha_JobCards");
            Delete.ForeignKey("FK_JobCards_Incident").OnTable("shesha_JobCards");

            Delete.ForeignKey("FK_Incidents_Municipality").OnTable("shesha_Incidents");
            Delete.ForeignKey("FK_Incidents_Driver").OnTable("shesha_Incidents");
            Delete.ForeignKey("FK_Incidents_Vehicle").OnTable("shesha_Incidents");
        }
    }
}
