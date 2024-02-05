using Estate.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Estate.Persistance.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgencyAppUser> AgencyAppUsers { get; set; }
        public DbSet<AppUserImage> AppUserImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<BlogReply> BlogReplies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Corporate> Corporates { get; set; }
        public DbSet<ExteriorType> ExteriorTypes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<ParkingType> ParkingTypes { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductExteriorType> ProductExteriorTypes { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductParkingType> ProductParkingTypes { get; set; }
        public DbSet<ProductReply> ProductReplies { get; set; }
        public DbSet<ProductRoofType> ProductRoofTypes { get; set; }
        public DbSet<ProductViewType> ProductViewTypes { get; set; }
        public DbSet<RoofType> RoofTypes { get; set; }
        public DbSet<ViewType> ViewTypes { get; set; }
        public DbSet<Settings> Settings { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //    base.OnModelCreating(modelBuilder);
        //}
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in entities)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreateAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdateAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
