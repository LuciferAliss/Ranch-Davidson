using Microsoft.EntityFrameworkCore;

public class GameContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public GameContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=resources/db/db.db");
    }
}