using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Lurch.Karma.Migrator.Migrations
{
    [Migration(201810040001)]
    public class AddUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("KarmaAmount").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
