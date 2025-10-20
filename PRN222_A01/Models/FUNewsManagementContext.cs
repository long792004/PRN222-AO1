using Microsoft.EntityFrameworkCore;

namespace NguyenThanhLong_SE18C.NET_A01.Models
{
    public class FUNewsManagementContext : DbContext
    {
        public FUNewsManagementContext()
        {
        }

        public FUNewsManagementContext(DbContextOptions<FUNewsManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<NewsArticle> NewsArticles { get; set; }
        public virtual DbSet<NewsTag> NewsTags { get; set; }
        public virtual DbSet<SystemAccount> SystemAccounts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category self-referencing relationship
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.CategoryID);

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.ParentCategoryID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure SystemAccount
            modelBuilder.Entity<SystemAccount>(entity =>
            {
                entity.ToTable("SystemAccount");
                entity.HasKey(e => e.AccountID);
            });

            // Configure Tag
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");
                entity.HasKey(e => e.TagID);
            });

            // Configure NewsArticle
            modelBuilder.Entity<NewsArticle>(entity =>
            {
                entity.ToTable("NewsArticle");
                entity.HasKey(e => e.NewsArticleID);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.NewsArticles)
                    .HasForeignKey(d => d.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.CreatedNewsArticles)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure NewsTag composite key
            modelBuilder.Entity<NewsTag>(entity =>
            {
                entity.ToTable("NewsTag");
                entity.HasKey(e => new { e.NewsArticleID, e.TagID });

                entity.HasOne(d => d.NewsArticle)
                    .WithMany(p => p.NewsTags)
                    .HasForeignKey(d => d.NewsArticleID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.NewsTags)
                    .HasForeignKey(d => d.TagID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
