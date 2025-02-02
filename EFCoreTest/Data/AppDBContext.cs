using Microsoft.EntityFrameworkCore;
namespace EFCoreTest.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Title = "INR", Description = "Indian INR" },
                new Currency() { Id = 2, Title = "Dollar", Description = "Dollar" },
                new Currency() { Id = 3, Title = "Euro", Description = "Euro" },
                new Currency() { Id = 4, Title = "Dinar", Description = "Dinar" }
                );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = 1, Title = "HINDI", Description = "Hindi" },
                new Language() { Id = 2, Title = "TAMIL", Description = "Tamil" },
                new Language() { Id = 3, Title = "PUNJABI", Description = "Punjabi" },
                new Language() { Id = 4, Title = "URDU", Description = "Urdu" }
                );
            //base.OnModelCreating(modelBuilder);
            //foreach (var foreignkey in modelBuilder.Model.GetEntityTypes()
            //.SelectMany(e => e.GetForeignKeys()))
            //{
            //    foreignkey.DeleteBehavior = DeleteBehavior.Restrict;
            //}
        }
    }
}
