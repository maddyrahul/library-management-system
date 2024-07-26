using System.ComponentModel.DataAnnotations;


namespace Data_Access_Layer.Models
{
    public class Borrow_Lent_Details
    {
        [Key]
        public int BorrowedBookId { get; set; }

    
        public int BookId { get; set; }

    
        public int UserId { get; set; }

        [Required]
        public DateTime BorrowedDate { get; set; }

        [Required]
        public int isReturn { get; set; }

    }
}
