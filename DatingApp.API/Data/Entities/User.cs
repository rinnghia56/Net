using System.ComponentModel.DataAnnotations;
namespace DatingApp.API.Data.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(256)]
        public string username { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}