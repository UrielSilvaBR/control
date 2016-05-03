namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Control.DAL.Data.ControlContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Control.DAL.Data.ControlContext context)
        {

        }
    }
}
