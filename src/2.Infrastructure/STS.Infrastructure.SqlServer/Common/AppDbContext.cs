using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Category;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Enums;
using STS.Infrastructure.SqlServer.Configurations.BaseData;
using STS.Infrastructure.SqlServer.Configurations.Category;
using STS.Infrastructure.SqlServer.Configurations.Feature;
using STS.Infrastructure.SqlServer.Configurations.User;
namespace STS.Infrastructure.SqlServer.Common;
public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
       
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ExpertSkillsConfigs());
        builder.ApplyConfiguration(new OrderConfigs());
        builder.ApplyConfiguration(new FeedBackConfigs());
        builder.ApplyConfiguration(new CityConfigs());
        builder.ApplyConfiguration(new MainCategoryConfigs());
        builder.ApplyConfiguration(new ClientConfigs());
        builder.ApplyConfiguration(new ExpertConfigs());
        builder.ApplyConfiguration(new PictureConfigs());
        builder.ApplyConfiguration(new SubCategoryConfigs());
        builder.ApplyConfiguration(new SuggestionConfigs());
        builder.ApplyConfiguration(new TaskItemConfigs());
        //builder.ApplyConfiguration(new OrderStatusConfigs());

        builder.Entity<ApplicationUser>().HasQueryFilter(x => !(bool)x.SoftDelete);
        builder.Entity<MainCategory>().HasQueryFilter(x => !(bool)x.SoftDelete);
        builder.Entity<SubCategory>().HasQueryFilter(x => !(bool)x.SoftDelete);
        builder.Entity<TaskItem>().HasQueryFilter(x => !(bool)x.SoftDelete);
        //builder.Entity<Suggestion>().HasQueryFilter(x => !(bool)x.SoftDelete);
        //builder.Entity<Order>().HasQueryFilter(x => !(bool)x.SoftDelete);

        UserConfiguration.SeedUsers(builder);

        base.OnModelCreating(builder);
    }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Expert> Experts { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<ExpertSkills> ExpertSkills { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<FeedBack> FeedBacks { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
}
