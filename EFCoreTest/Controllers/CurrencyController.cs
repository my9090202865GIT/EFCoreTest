using EFCoreTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EFCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;

        public CurrencyController(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //var result = this._appDBContext.Currencies.ToList();
            //Func<Currency, bool> currencyFilterDel = cur => cur.Title.Length == 3;

            //var result = await (from cur in _appDBContext.Currencies
            //                    select cur).ToListAsync();
            var query = from Author in _appDBContext.Authors
                        join Book in _appDBContext.Books
                        on Author.Id equals Book.AuthorId
                        select new
                        {
                            bookId = Book.Id,
                            authorId = Book.AuthorId,
                            bookName = Book.Title,
                            AuthorName = Author.Name,
                        };
            var result = await (from cur in _appDBContext.Currencies
                                select new
                                {
                                    CurrencyId = cur.Id,
                                    title = cur.Title
                                }).AsNoTracking().ToListAsync(); // AsNoTracking() tells EFCore not to track entity state changes, this is useful when state is not going to be changed in further operation( read ops/ get req). In Enterprise application improve performance.
            return Ok(query);
        }
        [HttpGet("/[action]/{id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var result = await _appDBContext.Currencies.FindAsync(id);
            return Ok(result);
        }
        [HttpGet("/[action]/{name}")]
        public async Task<IActionResult> GetCurrencyByNameAsync([FromRoute] string name, [FromQuery] string? desc)
        {
            var result = await _appDBContext.Currencies.FirstOrDefaultAsync(cur =>
            cur.Title == name
            && (desc.Length >= 3 || cur.Description == desc));
            return Ok(result);
        }
        [HttpPost("/[action]/")]
        public async Task<IActionResult> GetCurrenciesByIdsAsync([FromBody] List<int> ids)
        {
            //var result = await _appDBContext.Currencies.Where(cur => ids.Contains(cur.Id)).Select(cur => new { CurrId = cur.Id, title = cur.Title }).ToListAsync();
            var result = await _appDBContext.Currencies.AsQueryable().ToListAsync();
            return Ok(result);
        }
    }
}
