using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;

namespace VictuzWeb.Persistence;

/// <summary>
/// Database context for this application.
/// </summary>
public class VictuzWebDatabaseContext : DbContext
{
    public DbSet<Gathering> Gatherings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection =
            @"Data Source=.;Initial Catalog=VictuzWeb;Integrated Security=true;TrustServerCertificate=true;";

        optionsBuilder.UseSqlServer(connection);
    }
}
