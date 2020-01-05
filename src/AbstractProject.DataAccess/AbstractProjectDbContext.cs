using AbstractProject.DataAccess.Configurations.Items;
using AbstractProject.Domain.Tables;
using Microsoft.EntityFrameworkCore;

namespace AbstractProject.DataAccess
{
    public class AbstractProjectDbContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }
        
        public AbstractProjectDbContext(DbContextOptions<AbstractProjectDbContext> options) : base(options: options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(modelBuilder: builder);
            builder.ApplyConfiguration(configuration: new ItemEntityConfiguration());
        }
    }
}