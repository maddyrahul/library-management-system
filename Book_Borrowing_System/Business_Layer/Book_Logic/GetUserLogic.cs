using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;


namespace Business_Layer.Book_Logic
{
    public class GetUserLogic: IGetUserLogic
    {
        private readonly LibraryDbContext _dbContext;

        public GetUserLogic(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Book> GetAllBookDetails()
        {
            var bookDetailsList = _dbContext.Book.ToList();
            return bookDetailsList;
        }

        public User GetUserDetailsById(int userId)
        {
            return _dbContext.User.FirstOrDefault(user => user.UserId == userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public Book GetBookDetailsById(int bookId)
        {
            return _dbContext.Book.FirstOrDefault(book => book.BookId == bookId);
        }

        public List<Borrow_Lent_Details> GetAllBorrowedBookByUserId(int userId)
        {
            var books = _dbContext.Borrow_Lent_Details
               .Where(ra => ra.UserId == userId)
               .Select(ra => new Borrow_Lent_Details
               {
                   BorrowedBookId = ra.BorrowedBookId,
                   BookId = ra.BookId,
                   UserId = ra.UserId,
                   BorrowedDate = ra.BorrowedDate,
                   isReturn=ra.isReturn,
                  

               })
               .ToList();

            return books;
        }

        public Borrow_Lent_Details GetBorrowedBookDetailsById(int borrowedBookId)
        {
            return _dbContext.Borrow_Lent_Details.FirstOrDefault(book => book.BorrowedBookId == borrowedBookId);
        }

        public void UpdateBookDetails(Book updatedBookDetails)
        {
            if (updatedBookDetails == null)
            {
                throw new ArgumentNullException(nameof(updatedBookDetails));
            }

            var existingBookDetails = _dbContext.Book.FirstOrDefault(book => book.BookId == updatedBookDetails.BookId);

            if (existingBookDetails != null)
            {
                existingBookDetails.UserId=updatedBookDetails.UserId;
                existingBookDetails.Author = updatedBookDetails.Author;
                existingBookDetails.Genre = updatedBookDetails.Genre;
                existingBookDetails.Description = updatedBookDetails.Description;
                existingBookDetails.Name = updatedBookDetails.Name;
                existingBookDetails.IsBookAvailable = "Yes";

                _dbContext.SaveChanges();
            }
        }
        
        public void UpdateBorrowedBookDetails(Borrow_Lent_Details updatedBookDetails)
        {
            if (updatedBookDetails == null)
            {
                throw new ArgumentNullException(nameof(updatedBookDetails));
            }

            var existingBookDetails = _dbContext.Borrow_Lent_Details.FirstOrDefault(book => book.BorrowedBookId == updatedBookDetails.BorrowedBookId);
           
            if (existingBookDetails != null)
            {
                existingBookDetails.UserId = updatedBookDetails.UserId;
                existingBookDetails.BookId = updatedBookDetails.BookId;
                existingBookDetails.BorrowedDate = updatedBookDetails.BorrowedDate;
                existingBookDetails.isReturn = updatedBookDetails.isReturn;

                
            }
            _dbContext.SaveChanges();
        }

        // Search
        public List<Book> SearchAvailableBooks(string searchValue)
        {
            try
            {
                var query = _dbContext.Book.AsQueryable();

                // Check if searchValue is not empty
                if (!string.IsNullOrEmpty(searchValue))
                {
                    // Search for Maker, Model, or RentalPrice
                    query = query.Where(book =>
                        book.Name.Contains(searchValue) ||
                        book.Author.Contains(searchValue) ||
                        book.Genre.Contains(searchValue)
                    );
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Book AddBookDetails(Book book)
        {
            var newRentalAgreement = new Book
            {
                UserId = book.UserId,
                Name = book.Name,
                Rating = book.Rating,
                Author = book.Author,
                Genre = book.Genre,
                IsBookAvailable = book.IsBookAvailable,
                Description = book.Description,
            };

            _dbContext.Book.Add(newRentalAgreement);

            // Increment the user's token count
            var user = _dbContext.User.Find(book.UserId);
            if (user != null)
            {
                user.TokensAvailable += 1;
            }

            _dbContext.SaveChanges(); // Save changes to both Book and User entities

            book.BookId = newRentalAgreement.BookId;
            return book;
        }
        public List<Book> GetBooksByUserId(int userId)
        {
            var books = _dbContext.Book
                .Where(ra => ra.UserId == userId)
                .Select(ra => new Book
                {
                    BookId = ra.BookId,
                    UserId = ra.UserId,
                    Name= ra.Name,
                    Author = ra.Author,
                    Rating= ra.Rating,  
                    Genre= ra.Genre,
                    Description= ra.Description,
                    IsBookAvailable= ra.IsBookAvailable,
                    
                })
                .ToList();

            return books;
        }
        public List<Book> GetBooksByBookId(int bookId)
        {
            var books = _dbContext.Book
                .Where(ra => ra.BookId == bookId)
                .Select(ra => new Book
                {
                    BookId = ra.BookId,
                    UserId = ra.UserId,
                    Name = ra.Name,
                    Author = ra.Author,
                    Rating = ra.Rating,
                    Genre = ra.Genre,
                    Description = ra.Description,
                    IsBookAvailable = ra.IsBookAvailable,

                })
                .ToList();

            return books;
        }
        public Borrow_Lent_Details BorrowedBookDetails(Borrow_Lent_Details borrow_Lent_Details)
        {
            var bookborrow = new Borrow_Lent_Details
            {
              
                UserId= borrow_Lent_Details.UserId,
                BookId= borrow_Lent_Details.BookId,
                BorrowedDate = borrow_Lent_Details.BorrowedDate,
                isReturn = borrow_Lent_Details.isReturn,

            };

            _dbContext.Borrow_Lent_Details.Add(bookborrow);

            // Increment the user's token count
            var user = _dbContext.User.Find(borrow_Lent_Details.UserId);
            if (user != null)
            {
                user.TokensAvailable -= 1;
            }

            // Increment the user's token count
            var book = _dbContext.Book.Find(borrow_Lent_Details.BookId);
            if (book != null)
            {
                book.IsBookAvailable = "No";
            }

            _dbContext.SaveChanges(); // Save changes to both Book and User entities
        
            borrow_Lent_Details.BorrowedBookId = bookborrow.BorrowedBookId;

            return borrow_Lent_Details;
        }
        public List<Borrow_Lent_Details> GetAllBorrowedBook()
        {
            return _dbContext.Borrow_Lent_Details.ToList();
        }
     

    }
}
