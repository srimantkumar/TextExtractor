using System.ComponentModel.DataAnnotations;

namespace TextExtractProject.Models
{
	public class UserLogin
	{
        public UserLogin(long userId, string userName, string password)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

