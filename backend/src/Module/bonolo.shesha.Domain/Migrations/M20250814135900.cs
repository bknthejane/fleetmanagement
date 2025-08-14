using FluentMigrator;
using Shesha.FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Domain.Migrations
{
    [Migration(20250814135900)]
    public class M20250814135900 : Migration
    {
        public override void Up()
        {
            Create.Table("shesha_Municipalities")
                .WithIdAsGuid("Id")
                .WithFullAuditColumns()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Type").AsString().Nullable()
                .WithColumn("Address").AsString().Nullable()
                .WithColumn("ContactPerson").AsString().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("ContactNumber").AsString().Nullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
