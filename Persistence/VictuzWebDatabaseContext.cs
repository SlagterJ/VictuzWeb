using Microsoft.EntityFrameworkCore;

namespace VictuzWeb.Persistence;

/// <summary>
/// Database context for this application.
/// </summary>
public class VictuzWebDatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection =
            @"Data Source=.;Initial Catalog=VictuzWeb;Integrated Security=true;TrustServerCertificate=true;";

        optionsBuilder.UseSqlServer(connection);
    }
}
