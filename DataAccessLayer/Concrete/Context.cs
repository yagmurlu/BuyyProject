using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Context:DbContext
    {
        //Market Module       
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockField> StockFields { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Image> Images { get; set; }

        //Order Module
        public DbSet<Order> Orders { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }
       
        //User Module
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Messages> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<PasswordReset> PasswordResets { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolesPermissions> RolesPermissions { get; set; }

        public Context()
        : base("MysqlConnection") //This 'DefaultConnection' should be equal to the connection string name on Web.config.
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder builder) {
            builder.Entity<Role>().HasIndex(r => r.RoleName).IsUnique();
            builder.Entity<Permission>().HasIndex(p => p.PermissionName).IsUnique();
        }

    }
}
