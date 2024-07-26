using Business_Layer.Book_Logic;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDetails : ControllerBase
    {
        private readonly IGetUserLogic _bookService;
        public BookDetails(IGetUserLogic bookDetailsService)
        {
            _bookService = bookDetailsService ?? throw new ArgumentNullException(nameof(bookDetailsService));
        }

        [HttpGet("GetAllBookDetails")]
        public ActionResult<IEnumerable<Book>> GetAllBookDetails()
        {
            try
            {
                var bookDetails = _bookService.GetAllBookDetails();
                return Ok(bookDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetBookDetailsById/{id}")]
        public ActionResult<Book> GetBookDetailsById(int id)
        {
            try
            {
                var bookDetails = _bookService.GetBookDetailsById(id);

                if (bookDetails == null)
                {
                    return NotFound();
                }

                return Ok(bookDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("GetBorrowedBookDetailsById/{id}")]
        public ActionResult<Borrow_Lent_Details> GetBorrowedBookDetailsById(int id)
        {
            try
            {
                var bookDetails = _bookService.GetBorrowedBookDetailsById(id);

                if (bookDetails == null)
                {
                    return NotFound();
                }

                return Ok(bookDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        


        

        [HttpGet("SearchAvailableBooks/{searchValue}")]
        public ActionResult<IEnumerable<Book>> SearchAvailableBooks(string searchValue)
        {
            try
            {
                var searchResults = _bookService.SearchAvailableBooks(searchValue);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost("AddBookDetails")]
        public IActionResult AddBookDetails([FromBody] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Invalid data");
                }

                var createdBook = _bookService.AddBookDetails(book);

                return Ok(createdBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }

        [HttpGet("GetBooksByUserId/{userId}")]
        public ActionResult<List<Book>> GetBooksByUserId(int userId)
        {
            try
            {
                var rentalAgreements = _bookService.GetBooksByUserId(userId);
                return Ok(rentalAgreements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        

        [HttpGet("GetAllBorrowedBookByUserId/{userId}")]
        public ActionResult<List<Borrow_Lent_Details>> GetAllBorrowedBookByUserId(int userId)
        {
            try
            {
                var rentalAgreements = _bookService.GetAllBorrowedBookByUserId(userId);
                return Ok(rentalAgreements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetBooksByBookId/{bookId}")]
        public ActionResult<List<Book>> GetBooksByBookId(int bookId)
        {
            try
            {
                var books = _bookService.GetBooksByBookId(bookId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("BorrowedBookDetails")]
        public IActionResult BorrowedBookDetails([FromBody] Borrow_Lent_Details book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Invalid data");
                }

                var borrowedBook = _bookService.BorrowedBookDetails(book);

                return Ok(borrowedBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }

        [HttpGet("GetAllBorrowedBook")]
        public ActionResult<IEnumerable<Borrow_Lent_Details>> GetAllBorrowedBook()
        {
            try
            {
                var Details = _bookService.GetAllBorrowedBook();
                return Ok(Details);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut("UpdateBorrowedBookDetails/{borrowedBookId}")]
        public IActionResult UpdateBorrowedBookDetails(int borrowedBookId, [FromBody] Borrow_Lent_Details updatedBookDetails)
        {
            try
            {
                if (updatedBookDetails == null || borrowedBookId != updatedBookDetails.BorrowedBookId)
                {
                    return BadRequest();
                }

                _bookService.UpdateBorrowedBookDetails(updatedBookDetails);


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateBookDetails/{id}")]
        public IActionResult UpdateBookDetails(int id, [FromBody] Book updatedBookDetails)
        {
            try
            {
                if (updatedBookDetails == null || id != updatedBookDetails.BookId)
                {
                    return BadRequest();
                }

                _bookService.UpdateBookDetails(updatedBookDetails);

             
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
