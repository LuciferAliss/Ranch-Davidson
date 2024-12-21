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

public class SettingContext : DbContext
{
    public DbSet<SettingModel> Settings { get; set; }

    public SettingContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=resources/db/db.db");
    }
}

public class SaveContext : DbContext
{
    public DbSet<SaveModel> Save { get; set; }

    public SaveContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=resources/db/db.db");
    }
}

public class StatisticsContext : DbContext
{
    public DbSet<StatisticsModel> Statistics { get; set; }

    public StatisticsContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=resources/db/db.db");
    }
}