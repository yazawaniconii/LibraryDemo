using System.Linq;
using Library.Web.Entities;

namespace Library.Web.Dal
{
    public class ReaderTypeDal
    {
        private readonly LibDbContext _context;

        public ReaderTypeDal(LibDbContext context)
        {
            _context = context;
        }

        //ReaderType的Dal暂未实现
        public void Add(ReaderType readerType)
        {
        }

        public ReaderType GetById(int id)
        {
            var rt = _context.ReaderTypes.SingleOrDefault(x => x.Id == id);
            return rt;
        }
    }
}