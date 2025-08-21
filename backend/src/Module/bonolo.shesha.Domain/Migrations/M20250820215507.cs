using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250820215507)]
    public class M20250820215507 : Migration
    {
        public override void Up()
        {
            Create.ForeignKey("FK_CorePersons_AssignedVehicleId_Vehicles")
                .FromTable("Core_Persons").ForeignColumn("AssignedVehicleId")
                .ToTable("shesha_Vehicles").PrimaryColumn("Id");

            Create.ForeignKey("FK_Vehicles_AssignedDriver")
                .FromTable("shesha_Vehicles").ForeignColumn("AssignedDriverId")
                .ToTable("Core_Persons").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_CorePersons_AssignedVehicleId_Vehicles").OnTable("Core_Persons");
            Delete.ForeignKey("FK_Vehicles_AssignedDriver").OnTable("shesha_Vehicles");
        }
    }
}
