
using System.ComponentModel.DataAnnotations;


namespace Data_Access_Layer.DTO
{
    public class User_Register_Dto
    {

        [StringLength(100, MinimumLength = 1)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }
        public int TokensAvailable { get; set; }
    }
} 

