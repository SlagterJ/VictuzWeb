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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // specificeer
        modelBuilder
            .Entity<Gathering>()
            .HasMany(gathering => gathering.RegisteredUsers)
            .WithMany(user => user.RegisteredForGatherings)
            .UsingEntity<Dictionary<string, object>>(
                "UserGathering",
                columnLeft =>
                    columnLeft
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UsersIdentifier")
                        .OnDelete(DeleteBehavior.Restrict),
                columnRight =>
                    columnRight
                        .HasOne<Gathering>()
                        .WithMany()
                        .HasForeignKey("GatheringIdentifier")
                        .OnDelete(DeleteBehavior.Restrict)
            );

        modelBuilder
            .Entity<Role>()
            .HasMany(role => role.UsersWithRole)
            .WithOne(user => user.Role)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        modelBuilder
            .Entity<Suggestion>()
            .HasOne(suggestion => suggestion.SuggestedBy)
            .WithMany(user => user.Suggestions)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        modelBuilder.Entity<User>();
        //.HasMany(a => a.RegisteredForGatherings)
        //.WithMany(a => a.RegisteredUsers)
        //.UsingEntity<Dictionary<string, object>>(
        //"UserGathering",
        //j => j.HasOne<Gathering>().WithMany().HasForeignKey("GatheringId"),
        //D => D.HasOne<User>().WithMany().HasForeignKey("UsersId")
        //);
    }
}
