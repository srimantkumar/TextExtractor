using System.ComponentModel.DataAnnotations;

namespace TextExtractProject.Models
{
    public class User
    {
        public User(string userName, string fullName)
        {
            UserName = userName;
            FullName = fullName;
            ContactNo = null;
            Email = null;
            Occupation = null;
            DOB = null;
            Gender = null;
            Address = null;
        }

        public User(string userName, string fullName, string contactNo, string email, string occupation, string dOB, string gender, string address)
        {
            UserName = userName;
            FullName = fullName;
            ContactNo = contactNo;
            Email = email;
            Occupation = occupation;
            DOB = dOB;
            Gender = gender;
            Address = address;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        public string ContactNo { get; set; }

        public string Email {get; set;}

        public string Occupation { get; set; }

        public string DOB { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

    }
}