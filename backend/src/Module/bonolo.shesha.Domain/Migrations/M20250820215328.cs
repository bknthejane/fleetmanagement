using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250820215328)]
    public class M20250820215328 : Migration
    {
        public override void Up()
        {
            Alter.Table("Core_Persons")
                .AddColumn("AssignedVehicleId").AsGuid().Nullable()
                .AddColumn("AssignedVehicleFleetNumber").AsString(50).Nullable();
        }

        public override void Down()
        {
            Delete.Column("AssignedVehicleId").FromTable("Core_Persons");
            Delete.Column("AssignedVehicleFleetNumber").FromTable("Core_Persons");
        }
    }
}
