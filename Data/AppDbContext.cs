namespace webdotnetapp.Data
{
    using Microsoft.EntityFrameworkCore;
    using webdotnetapp.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<FlashcardTag> FlashcardTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);

                
                entity.HasMany(u => u.Collections)
                      .WithOne(c => c.User)
                      .HasForeignKey(c => c.UserId)
                      .IsRequired(false);
            });


            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.HasMany(c => c.Flashcards).WithOne(f => f.Collection).HasForeignKey(f => f.CollectionId);
            });

            modelBuilder.Entity<Flashcard>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(100);
                entity.Property(f => f.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<FlashcardTag>()
                .HasKey(ft => new { ft.FlashcardId, ft.TagId });

            modelBuilder.Entity<FlashcardTag>()
                .HasOne(ft => ft.Flashcard)
                .WithMany(f => f.FlashcardTags)
                .HasForeignKey(ft => ft.FlashcardId);

            modelBuilder.Entity<FlashcardTag>()
                .HasOne(ft => ft.Tag)
                .WithMany(t => t.FlashcardTags)
                .HasForeignKey(ft => ft.TagId);
        }
    }

}
