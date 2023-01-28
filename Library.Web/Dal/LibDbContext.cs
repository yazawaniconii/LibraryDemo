using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Book = Library.Web.Entities.Book;
using Borrow = Library.Web.Entities.Borrow;
using Reader = Library.Web.Entities.Reader;
using ReaderType = Library.Web.Entities.ReaderType;

namespace Library.Web.Dal
{
    public class LibDbContext : DbContext
    {
        //继承DbContext基类后,设置构造函数和DbSet,详情可见Entity Framework Core文档
        public LibDbContext(DbContextOptions<LibDbContext> options) : base(options)
        {
        }

        public DbSet<Reader> Readers { get; set; }
        public DbSet<ReaderType> ReaderTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrow> Borrows { get; set; }

        //设置记录log的方法,注册服务时调用
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}