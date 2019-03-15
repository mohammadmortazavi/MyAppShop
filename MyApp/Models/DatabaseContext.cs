﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyApp.Models
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext,Migrations.Configuration>());
        }
        public DbSet <User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Setting>Settings { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<GenderGategory> GenderGategories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }


    }

}