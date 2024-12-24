using API_EXAMEN_APP.Models.Books;
using API_EXAMEN_APP.Models.Commands;
using API_EXAMEN_APP.Models.Subscribes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_EXAMEN_APP.Models
{
    public class ApiBbContext : IdentityDbContext<User>
    {
        public ApiBbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<TypeBook> TypeBooks { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<SubType> Subtypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity configurations are applied

            // Mark Identity entities as keyless
            //modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            //modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

            // Configure Book relationships
            //modelBuilder.Entity<Book>()
             //   .HasOne(b => b.Author)
               // .WithMany(a => a.Books)
                //.HasForeignKey(b => b.AuthorId);

            /*modelBuilder.Entity<Book>()
                .HasOne(b => b.Typebook)
                .WithMany(t => t.Books)
                .HasForeignKey(b => b.TypeId);

            // Configure Subscribe relationships
            modelBuilder.Entity<Subscribe>()
                .HasOne(s => s.SubType)
                .WithMany(st => st.Subscribes)
                .HasForeignKey(s => s.TypeID);

            // Configure Command relationships
            modelBuilder.Entity<Command>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Commands)
                .HasForeignKey(c => c.BookId);

            // Add indices to foreign keys
            modelBuilder.Entity<Book>().HasIndex(b => b.AuthorId);
            modelBuilder.Entity<Book>().HasIndex(b => b.TypeId);
            modelBuilder.Entity<Subscribe>().HasIndex(s => s.TypeID);
            modelBuilder.Entity<Command>().HasIndex(c => c.BookId);*/
        }
    }
}
