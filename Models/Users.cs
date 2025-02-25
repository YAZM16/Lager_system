using System.ComponentModel.DataAnnotations;
namespace My_Project_03.Models
{
    

    public class User
    {
        [Key]  // Ensures Entity Framework recognizes UserId as the primary key
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }

}
