﻿namespace NinjaDomain.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthDayToNinja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ninjas", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ninjas", "DateOfBirth");
        }
    }
}