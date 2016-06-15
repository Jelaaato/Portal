namespace WebPortal.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SampleIdentityDb : IdentityDbContext<Users>
    {
        public SampleIdentityDb()
            : base("SampleIdentityDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("WebPortalUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("WebPortalRoles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("WebPortalUserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("WebPortalUserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("WebPortalUserLogin");
        }

        static SampleIdentityDb()
        {
            Database.SetInitializer<SampleIdentityDb>(new IdentityDbSeed());
        }

        public static SampleIdentityDb Create()
        {
            return new SampleIdentityDb();
        }
    }

    public class IdentityDbSeed : DropCreateDatabaseIfModelChanges<SampleIdentityDb>
    {
        protected override void Seed(SampleIdentityDb context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(SampleIdentityDb Context)
        {
            //UsersManager UserManager = new UsersManager(new UserStore<Users>(Context));
            //RolesManager RoleManager = new RolesManager(new RoleStore<roles>(Context));

            //string Name = "Administrator";
            //string UserName = "appAdmin";
            //string Password = "P0rt4l@dm!n";
            //string email = "elabetoria@asianhospital.com";

            
        }
    }
}
