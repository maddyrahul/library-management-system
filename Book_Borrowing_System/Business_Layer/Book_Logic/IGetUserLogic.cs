using Data_Access_Layer.Models;

namespace Business_Layer.Book_Logic
{
    public interface IGetUserLogic
    {
        User GetUserDetailsById(int userId);
        Book GetBookDetailsById(int bookId);
        List<Book> GetBooksByBookId(int bookId);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        List<Book> GetAllBookDetails();
        List<Borrow_Lent_Details> GetAllBorrowedBookByUserId(int userId);
        Borrow_Lent_Details GetBorrowedBookDetailsById(int borrowedBookId);
        void UpdateBorrowedBookDetails(Borrow_Lent_Details updatedBookDetails);
        void UpdateBookDetails(Book updatedBookDetails);
        List<Book> SearchAvailableBooks(string searchValue);
        Book AddBookDetails(Book books);
        List<Book> GetBooksByUserId(int userId);
        Borrow_Lent_Details BorrowedBookDetails(Borrow_Lent_Details borrow_Lent_Details);
        List<Borrow_Lent_Details> GetAllBorrowedBook();

    }
}
