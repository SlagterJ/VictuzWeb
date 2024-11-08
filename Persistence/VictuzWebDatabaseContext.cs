using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;

namespace VictuzWeb.Persistence;

/// <summary>
/// Database context for this application.
/// </summary>
public class VictuzWebDatabaseContext : DbContext
{
    /// <summary>
    /// Gatherings set.
    /// </summary>
    public DbSet<Gathering> Gatherings { get; init; }

    /// <summary>
    /// Roles set.
    /// </summary>
    public DbSet<Role> Roles { get; init; }

    /// <summary>
    /// Suggestions set.
    /// </summary>
    public DbSet<Suggestion> Suggestions { get; init; }

    /// <summary>
    /// Clubs set.
    /// </summary>
    public DbSet<Club> Clubs { get; init; }

    /// <summary>
    /// Users set.
    /// </summary>
    public DbSet<User> Users { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connection =
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

        var userRole = new Role() { Identifier = 1, Name = "User" };

        var adminRole = new Role() { Identifier = 2, Name = "Admin" };

        var nickyUser = new User()
        {
            Identifier = 1,
            Firstname = "Nicky",
            Surname = "Jaspers",
            Username = "GigaChad",
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
            PasswordHash = "password123",
            RoleIdentifier = 1,
        };

        var mielUser = new User()
        {
            Identifier = 2,
            Firstname = "Miel",
            Surname = "Noelanders",
            Username = "MielTheKing",
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
            PasswordHash = "password123456",
            RoleIdentifier = 2,
        };

        modelBuilder.Entity<Role>().HasData(userRole);
        modelBuilder.Entity<Role>().HasData(adminRole);
        modelBuilder.Entity<User>().HasData(nickyUser);
        modelBuilder.Entity<User>().HasData(mielUser);

        modelBuilder
            .Entity<Suggestion>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Suggestion>("Suggestion")
            .HasValue<Gathering>("Gathering");
    }
}
