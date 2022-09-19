using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Dal
{
    public class ReaderDal
    {
        private readonly LibDbContext _context;

        public ReaderDal(LibDbContext context)
        {
            _context = context;
        }

        public async Task<Reader> GetById(int id)
        {
            //这里尝试使用原生SQL语句,并使用插值防止SQL注入
            //需要ReaderType,使用Include查询外键
            var userArray = await _context.Readers.FromSqlInterpolated(
                    $"select * from Readers where Id = {id}")
                .Include(x => x.Type)
                .ToArrayAsync();
            if (userArray.Length == 0)
            {
                return null;
            }
            return userArray[0];
        }

        public async Task<int> Add(Reader reader, int typeId)
        {
            //typeId为主键,所以使用SingleOrDefault,default为null
            var readerType = await _context.ReaderTypes.SingleOrDefaultAsync(
                x => x.Id == typeId);
            //读者类型不存在,返回-1
            if (readerType == null)
            {
                return -1;
            }
            reader.Type = readerType;
            await _context.Readers.AddAsync(reader);
            //可返回数据库修改行数
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Reader>> GetReaders(int offset, int limit)
        {
            var readerList = await _context.Readers
                .Include(x => x.Type)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            return readerList;
        }

    }
}