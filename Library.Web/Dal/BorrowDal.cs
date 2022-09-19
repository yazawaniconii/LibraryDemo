using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Dal
{
    public class BorrowDal
    {
        private readonly LibDbContext _context;

        public BorrowDal(LibDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddRecord(Borrow borrow)
        {
            await _context.Borrows.AddAsync(borrow);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Borrow>> GetBorrows(int offset, int limit)
        {
            //查询借阅信息时,也会需要读者和书籍的信息,所以使用了Include方法,会进行子查询
            var borrowList = await _context.Borrows
                .Skip(offset)
                .Take(limit)
                .Include(x => x.Reader)
                .Include(x => x.Book)
                .ToListAsync();
            return borrowList;
        }
    }
}