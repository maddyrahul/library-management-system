using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Models
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Borrow_Lent_Details> Borrow_Lent_Details { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
            User = Set<User>();
            Book = Set<Book>();
            Borrow_Lent_Details = Set<Borrow_Lent_Details>();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }
    }
}
