using Microsoft.EntityFrameworkCore;
using TextExtractProject.Models;

public class UserAdharInformationContext : DbContext
{
    public UserAdharInformationContext(DbContextOptions<UserAdharInformationContext> options)
        : base(options)
    {
    }

    public DbSet<UserAdharInformation> UserAdharInformations { get; set; } = null!;
}