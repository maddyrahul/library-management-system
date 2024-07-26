
using System.ComponentModel.DataAnnotations;


namespace Data_Access_Layer.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Username must be at least 6 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "TokensAvailable must be a non-negative value.")]
        public int TokensAvailable { get; set; }
    }
}
