using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Dal
{
    public class BookDal
    {
        private readonly LibDbContext _context;

        public BookDal(LibDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetByCode(string code)
        {
            //code是唯一的所以用SingleOrDefault方法,查询不到的default值为null,可重设
            var book = await _context.Books
                .SingleOrDefaultAsync(
                    x => x.Code == code);
            return book;
        }

        public async Task<int> Add(Book book)
        {
            await _context.Books.AddAsync(book);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetBooks(int offset, int limit)
        {
            //查询出来的书为多本,使用List存放
            var bookList = await _context.Books
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            return bookList;
        }
    }
}