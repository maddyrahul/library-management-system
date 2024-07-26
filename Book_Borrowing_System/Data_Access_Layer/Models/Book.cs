
using System.ComponentModel.DataAnnotations;


namespace Data_Access_Layer.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

     
        public int UserId { get; set; }

     

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string? Genre { get; set; }

        public string? IsBookAvailable { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

       

       

    }
}
