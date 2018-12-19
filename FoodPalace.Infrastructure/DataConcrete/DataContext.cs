using FoodPalace.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Infrastructure
{
   public class DataContext : DbContext
    {
        public DataContext() : base("connection")
        {
            Database.SetInitializer<DataContext>(null); // This is added to prevent recreating existing database
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }
        public DbSet<Permission> Permissios { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }


        ////public static DataContext Create()
        //{
        //    return new DataContext();
        //}
    }
}
