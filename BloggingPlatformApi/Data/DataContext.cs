using BloggingPlatformApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatformApi.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags{ get; set; }
        public DbSet<Writer> Writers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTag>().HasKey(pc=> new { pc.ArticleId, pc.TagId});
            modelBuilder.Entity<ArticleTag>()
                .HasOne(a => a.Article)
                .WithMany(at => at.ArticleTags)
                .HasForeignKey(a=> a.ArticleId);
            modelBuilder.Entity<ArticleTag>()
                .HasOne(t => t.Tag)
                .WithMany(at => at.ArticleTags)
                .HasForeignKey(t => t.TagId);
        }
    }
}
