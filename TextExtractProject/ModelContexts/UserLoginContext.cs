using Microsoft.EntityFrameworkCore;
using TextExtractProject.Models;

public class UserLoginContext : DbContext
{
    public UserLoginContext(DbContextOptions<UserLoginContext> options)
    : base(options)
    {
    }

    public DbSet<UserLogin> UserLoginCredentials { get; set; } = null!;

    public bool ValidateUserCredentials(string userName, string password)
    {
        // Check if the username and password match a user in the database
        return UserLoginCredentials.Any(u => u.UserName == userName && u.Password == password);
    }
}


