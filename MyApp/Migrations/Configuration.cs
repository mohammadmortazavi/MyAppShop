namespace MyApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MyApp.Models;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<MyApp.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MyApp.Models.DatabaseContext";
        }

        protected override void Seed(MyApp.Models.DatabaseContext context)
        {
            DatabaseContext db = new DatabaseContext();

            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile("1234", "MD5");
            if (db.Roles.Count() == 0)
            {
                Role role = new Role()
                {
                    RoleName = "admin",
                    RoleTitel = "مدیر"
                };
                db.Roles.Add(role);
                User user = new User()
                {
                    IsActive = true,
                    CodeNumber = "777777",
                    RoleId = role.Id,
                    Mobile = "09110001234",
                    Password = hashpass
                };
                db.Users.Add(user);
                db.SaveChanges();

                Role role2 = new Role()
                {
                    RoleName = "user",
                    RoleTitel = "کاربر"

                };
                db.Roles.Add(role2);
                db.SaveChanges();

                }
        }
    }
}
