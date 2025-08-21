using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250820220221)]
    public class M20250820220221 : Migration
    {
        public override void Up()
        {
            Rename.Column("AssignedVehicleId").OnTable("Core_Persons").To("shesha_AssignedVehicleId");
            Rename.Column("AssignedVehicleFleetNumber").OnTable("Core_Persons").To("shesha_AssignedVehicleFleetNumber");
        }

        public override void Down()
        {
            Rename.Column("shesha_AssignedVehicleId").OnTable("Core_Persons").To("AssignedVehicleId");
            Rename.Column("shesha_AssignedVehicleFleetNumber").OnTable("Core_Persons").To("AssignedVehicleFleetNumber");
        }
    }
}
