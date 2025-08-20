using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250820123613)]
    public class M20250820123613 : Migration
    {
        public override void Up()
        {
            Rename.Column("Department").OnTable("Core_Persons").To("shesha_Department");
        }

        public override void Down()
        {
            Rename.Column("shesha_Department").OnTable("Core_Persons").To("Department");
        }
    }
}
