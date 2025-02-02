using AutoMapper;
using DotNetTry.serviceInject;
using EFCoreTest.Models;
using EFCoreTest.Models.second_dbfirstTry;
using EFCoreTest.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace EFCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(Employee_BookDBContext appDBContext,
        IBookRepository bookRepository,
        IMapper mapper,
        IDemoService demoService,
        HttpClient httpClient) : ControllerBase
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;
        private readonly HttpClient httpClient = httpClient;

        [HttpPost("/AddNewBook")]
        public async Task<IActionResult> AddNewBook([FromBody] Book model)
        {
            await _bookRepository.AddAsync(model);
            //appDBContext.Set<Book>().Add(model);
            //appDBContext.Books.Add(model);
            //appDBContext.Books.AddRange(model); //to add multiple books record.
            //await appDBContext.SaveChangesAsync();
            return Ok(model);
        }
        [HttpPut("/update")]
        public async Task<IActionResult> UpdateBook([FromBody] BookModel model)
        {
            var foundBook = await _bookRepository.GetByIdAsync(model.Id);
            if (foundBook != null)
            {
                foundBook.Title = model.Title;
                foundBook.Description = model.Description;
                foundBook.NoOfPages = model.NoOfPages;
                foundBook.CreatedOn = model.CreatedOn;
            }
            await _bookRepository.UpdateAsync(foundBook);
            //appDBContext.Update(foundBook);

            //appDBContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //manually changing entity state.
            //await appDBContext.SaveChangesAsync();
            return Ok(model);
        }
        [HttpGet("/[action]")]
        public async Task<IActionResult> GetAllBooks()
        {
            //var books = await appDBContext.Books.Where(book => book.IsActive)
            //    .Include(x => x.Author)
            //    .Include(l => l.Language)
            //    .ToListAsync();
            demoService.Description();
            return Ok(_mapper.Map<List<BookModel>>(await _bookRepository.GetAllAsync()));
        }
        [HttpGet("/[action]/{id}")]
        public async Task<IActionResult> GetByIdBookMappedAsync([FromRoute] int id)
        {
            var foundBook = await _bookRepository.GetByIdAsync(id);
            BookModel mappedBook = _mapper.Map<BookModel>(foundBook);
            return Ok(mappedBook);
        }
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int bookId)
        {
            var book = new Book { Id = bookId };
            //appDBContext.Entry(book).State = EntityState.Deleted; //new book type instance is created with actual bookid and using that only record is deleted, rest of the property is not required.
            //In the below approach first db request is made to get the record and then delete query is made.In the above ony delete query is made.
            var foundBook = await appDBContext.Books.FirstOrDefaultAsync(book => book.Id == bookId);
            if (foundBook == null)
            {
                return NotFound();
            }
            appDBContext.Remove(foundBook);
            await appDBContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {
            //foreach (var id in ids)
            //{
            //    var book = new Book { Id = id };
            //    appDBContext.Entry(book).State = EntityState.Deleted; //new book type instance is created with actual bookid and using that only record is deleted, rest of the property is not required.
            //}
            // OR
            //var books = await appDBContext.Books.Where(book => book.IsActive && ids.Contains(book.Id)).ToListAsync();
            //appDBContext.Books.RemoveRange(books);  // multiple sql queries to delete

            //appDBContext.SaveChanges();

            //await appDBContext.Books.Where(book => ids.Contains(book.Id)).ExecuteDeleteAsync(); // single sql query is generated. Since this is a direct SQL operation that doesn’t need to involve the DbContext’s change tracker.

            foreach (var id in ids)
            {
                await _bookRepository.DeleteAsync(id);
            }
            return Ok();
        }

        [HttpPost("/[action]")]
        public async Task<IActionResult> AddNewBookWithSp([FromBody] BookRequest request)
        {
            var result = 0;
            try
            {

                result = await appDBContext.Database.ExecuteSqlRawAsync(
                   "EXEC dbo.SpAddBookWithPriceDetails @Title, @Description, @NoOfPages, @IsActive, @CreatedOn, @LanguageId, @AuthorId, @Currency, @Amount",
                   new SqlParameter("@Title", request.Title),
                   new SqlParameter("@Description", request.Description),
                   new SqlParameter("@NoOfPages", request.NoOfPages),
                   new SqlParameter("@IsActive", request.IsActive),
                   new SqlParameter("@CreatedOn", "2025-01-27 10:30:00"),
                   new SqlParameter("@LanguageId", request.LanguageId),
                   new SqlParameter("@AuthorId", request.AuthorId),
                   new SqlParameter("@Currency", request.Currency),
                   new SqlParameter("@Amount", request.Amount)
               );

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("DB Error.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("SP ran Unsuccessfully.");
            }

            return Ok($"SP ran successfully.-> {result}");
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetDemoBook()
        {

            return Ok(new { result = "Book4" });
        }
        //[HttpGet("/[action]/{id}")]
        //public async Task<IActionResult> GetBooksforLanguage([FromRoute] int id)
        //{
        //    return Ok(await appDBContext.Languages.Where(L => L.Id == id).Include(b => b.Books).ToArrayAsync());
        //} model.
    }
}
