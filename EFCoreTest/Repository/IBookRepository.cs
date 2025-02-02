using EFCoreTest.Models.second_dbfirstTry;

namespace EFCoreTest.Repository
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task AddAsync(Book entity);
        Task UpdateAsync(Book entity);
        Task DeleteAsync(int id);
    }
}