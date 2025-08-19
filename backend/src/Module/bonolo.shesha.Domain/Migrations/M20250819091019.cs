using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace shesha.app.Domain.Migrations
{
    [Migration(20250819091019)]
    public class M20250819091019 : Migration
    {

        public override void Up()
        {
            Alter.Table("Core_Persons")
         // Change "MunicipalityId" to "shesha_MunicipalityId"
         .AddForeignKeyColumn("shesha_MunicipalityId", "shesha_Municipalities");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
