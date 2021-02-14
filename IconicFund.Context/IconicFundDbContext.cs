using IconicFund.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IconicFund.Context
{
    public class IconicFundDbContext : DbContext
    {
        public IconicFundDbContext(DbContextOptions<IconicFundDbContext> options) : base(options)
        {

        }

        #region Tables

       
        public DbSet<Department> Departments { get; set; }
      
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<PermissionGroupAdmin> PermissionGroupAdmins { get; set; }


        public DbSet<BasicSystemSetting> BasicSystemSetting { get; set; }


        //===============
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }

        public DbSet<SystemLogging> SystemLogging { get; set; }

        public DbSet<Lkp_DateType> Lkp_DateType { get; set; }
        public DbSet<Lkp_PasswordComplexity> Lkp_PasswordComplexity { get; set; }

        
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////Unique constraint
            //modelBuilder.Entity<Facility>().HasIndex(c => c.CommercialRegistrationNo).IsUnique(true);

            //Composite Primary Key Constraints           
            modelBuilder.Entity<AdminRole>().HasKey(i => new { i.AdminId, i.RoleId });
            modelBuilder.Entity<PermissionGroupAdmin>().HasKey(i => new { i.PermissionGroupCode, i.AdminId });

           
            //Foriegn Key constraints  
            modelBuilder.Entity<Department>().HasOne(r => r.ParentDepartment).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Region>().HasOne(r => r.City).WithMany(c => c.Regions).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdminRole>().HasOne(a => a.Role).WithMany(r => r.Admins).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemLogging>().HasOne(r => r.UserData).WithMany(c => c.LoggingData).OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }


    // un comment this if you get this error 
    //Unable to create an object of type 'IconicFundDbContext'. For the different patterns supported at design time


    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IconicFundDbContext>
    //{
    //    public IconicFundDbContext CreateDbContext(string[] args)
    //    {

    //        var builder = new DbContextOptionsBuilder<IconicFundDbContext>();
    //        var connectionString = "Server=localhost;Database=IODB;Trusted_Connection=True;MultipleActiveResultSets=true";
    //        builder.UseSqlServer(connectionString);
    //        return new IconicFundDbContext(builder.Options);
    //    }
    //}
}
