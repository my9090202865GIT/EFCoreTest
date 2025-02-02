using EFCoreTest.Models.second_dbfirstTry;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly Employee_BookDBContext _context;
        private readonly DbSet<Book> _dbSet;

        public BookRepository(Employee_BookDBContext context)
        {
            _context = context;
            _dbSet = context.Set<Book>();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(x => x.Author).Include(x => x.Language).ToListAsync();
            //return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(Book entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
