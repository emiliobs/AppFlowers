namespace FlowersBack.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FlowersBack.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            //asigo permiso por si hhay que hacer cambo interno con daños incluidos:
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FlowersBack.Models.DataContext";
        }

        protected override void Seed(FlowersBack.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
