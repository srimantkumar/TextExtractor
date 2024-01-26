using System.ComponentModel.DataAnnotations;

namespace TextExtractProject.Models
{ 
    public class UserAdharInformation 
    {
        public UserAdharInformation(long userId, string userName, string adharDetailsJson, byte[] imageData)
        {
            UserId = userId;
            UserName = userName;
            AdharDetailsJson = adharDetailsJson;
            ImageData = imageData;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId {get; set;}

        [Required]
        public string UserName {get; set;}

        [Required]
        public string AdharDetailsJson {get; set;}

        [Required]
        public byte[] ImageData { get; set; } 
    }
}