using System;
using System.Threading.Tasks;
using Library.Web.Dal;
using Library.Web.Entities;
using Library.Web.Models;
using Library.Web.Models.EnumModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Controllers
{
    // [Authorize]
    [Authorize(Roles = "8")]
    public class AdminController : Controller
    {
        private readonly LibDbContext _context;

        //通过构造函数自动向控制器加入数据库上下文连接的服务，服务的具体注册在Startup.cs
        public AdminController(LibDbContext context)
        {
            _context = context;
        }

        //默认的action，返回默认的视图
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateReader()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReader(ReaderViewModel readerViewModel)
        {
            var rdDal = new ReaderDal(_context);
            var user = await rdDal.GetById(readerViewModel.Id);
            //用户已存在
            if (user != null)
            {
                ModelState.AddModelError("Id", "This user is already existing");
                return View();
            }

            //转换枚举类
            var sexModel = (SexEnum)readerViewModel.Sex;
            string sex = sexModel.ToString();
            var reader = new Reader
            {
                Id = readerViewModel.Id,
                Name = readerViewModel.Name,
                Sex = sex,
                Type = null,
                Dept = readerViewModel.Dept,
                Phone = readerViewModel.Phone,
                Email = readerViewModel.Email,
                DateReg = DateTime.UtcNow,
                Photo = null,
                Status = "有效",
                BorrowQty = 0,
                Password = readerViewModel.Password,
                AdminRole = 0
            };
            //返回值为-1表示该ReaderType不存在
            var statusCode = await rdDal.Add(reader, readerViewModel.Type);
            if (statusCode == -1)
            {
                ModelState.AddModelError("Type", "This ReaderType do not exist");
                return View();
            }

            ModelState.Clear();
            ViewBag.isSuccess = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayReaders(int id)
        {
            //id 为显示第几页，小于1则设为1
            if (id < 1)
            {
                id = 1;
            }

            var rdDal = new ReaderDal(_context);
            //一页显示10条记录
            const int limit = 10;
            //数据库查询时的偏移量
            var offset = (id - 1) * limit;
            var readerList = await rdDal.GetReaders(offset, limit);
            //将数据id通过ViewBag传到视图
            ViewBag.page = id;
            return View(readerList);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel bookViewModel)
        {
            var bkDal = new BookDal(_context);
            var book = await bkDal.GetByCode(bookViewModel.Code);
            //书籍已存在
            if (book != null)
            {
                ModelState.AddModelError("Code", "This book already existed");
                return View();
            }

            var newBook = new Book
            {
                Code = bookViewModel.Code,
                Name = bookViewModel.Name,
                Author = null,
                Press = null,
                DatePress = default,
                Isbn = null,
                Catalog = null,
                Language = 0,
                Pages = 0,
                Price = 0,
                DateIn = DateTime.UtcNow,
                Brief = null,
                Status = "在馆"
            };
            await bkDal.Add(newBook);
            ModelState.Clear();
            //向视图传入添加成功信号
            ViewBag.isSuccess = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayBooks(int id)
        {
            //id 为显示第几页，小于1则设为1
            if (id < 1)
            {
                id = 1;
            }

            var bkDal = new BookDal(_context);
            //一页显示10条记录
            const int limit = 10;
            //数据库查询时的偏移量
            var offset = (id - 1) * limit;
            var readerList = await bkDal.GetBooks(offset, limit);
            //将数据id通过ViewBag传到视图
            ViewBag.page = id;
            return View(readerList);
        }

        [HttpGet]
        public IActionResult Borrow()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Borrow(BorrowViewModel borrowViewModel)
        {
            var rdDal = new ReaderDal(_context);
            var reader = await rdDal.GetById(borrowViewModel.ReaderId);
            //读者不存在
            if (reader == null)
            {
                ModelState.AddModelError("ReaderId", "This reader does not exist");
                return View();
            }

            //未查到读者类别
            if (reader.Type== null)
            {
                ModelState.AddModelError("Id", "Unknown reader type");
                return View();
            }

            //不是有效或者借阅数量大于等于可借数量
            if (reader.Status != "有效" || reader.BorrowQty >= reader.Type.CanLendQty)
            {
                ModelState.AddModelError("ReaderId", "This reader can not borrow books now");
                return View();
            }


            var bkDal = new BookDal(_context);
            var book = await bkDal.GetByCode(borrowViewModel.BookCode);
            //查询书是否在馆
            if (book == null || book.Status != "在馆")
            {
                ModelState.AddModelError("BookCode", "This book is not in Library");
                return View();
            }

            var borrow = new Borrow
            {
                Reader = reader,
                Book = book,
                ContinueTimes = 0,
                //借出时间为当前时间
                DateOut = DateTime.UtcNow,
                DateRetPlan = DateTime.UtcNow.AddDays(reader.Type.CanLendDay),
                DateRetAct = null,
                OverDay = 0,
                OverMoney = 0,
                PunishMoney = 0,
                IsReturned = false,
                OperatorBorrow = null,
                OperatorReturn = null
            };

            var brDal = new BorrowDal(_context);
            //添加借阅记录
            await brDal.AddRecord(borrow);
            //修改读者和书籍属性
            reader.BorrowQty += 1;
            book.Status = "借出";
            await _context.SaveChangesAsync();

            ModelState.Clear();
            ViewBag.isSuccess = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayBorrows(int id)
        {
            //id 为显示第几页，小于1则设为1
            if (id < 1)
            {
                id = 1;
            }

            var brDal = new BorrowDal(_context);
            const int limit = 10;
            var offset = (id - 1) * limit;
            var borrows = await brDal.GetBorrows(offset, limit);
            //将数据id通过ViewBag传到视图
            ViewBag.page = id;
            return View(borrows);
        }

        [HttpGet]
        public IActionResult ReturnBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(BorrowViewModel borrowViewModel)
        {
            var rdDal = new ReaderDal(_context);
            var reader = await rdDal.GetById(borrowViewModel.ReaderId);
            //读者不存在
            if (reader == null)
            {
                ModelState.AddModelError("ReaderId", "This reader does not exist");
                return View();
            }

            var bkDal = new BookDal(_context);
            var book = await bkDal.GetByCode(borrowViewModel.BookCode);
            //书籍不存在
            if (book == null)
            {
                ModelState.AddModelError("BookCode", "This book does not exist");
                return View();
            }

            //查询该读者是否借阅了该书籍
            var borrow = await _context.Borrows
                    .Include(x => x.Reader)
                    .Include(x => x.Book)
                    .SingleOrDefaultAsync(
                        x => x.Reader.Id == reader.Id && x.Book.Code == book.Code && x.IsReturned == false);
            if (borrow == null)
            {
                ModelState.AddModelError("BookCode", "This reader does not borrow the book");
                return View();
            }

            //借阅成功，修改相关属性
            reader.BorrowQty -= 1;
            book.Status = "在馆";
            borrow.IsReturned = true;
            borrow.DateRetAct = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            ViewBag.isSuccess = true;
            return View();
        }

        [HttpGet]
        public IActionResult EditReaderInfo(int id)
        {
            //如果路由中没有传入要修改的读者id（id默认值为0），则重定向
            if (id == 0)
            {
                return RedirectToAction(nameof(DisplayReaders));
            }

            ViewBag.id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditReaderInfo(ReaderViewModel viewModel)
        {
            var rdDal = new ReaderDal(_context);
            var reader = await rdDal.GetById(viewModel.Id);
            //查不到读者就直接返回视图
            if (reader == null )
            {
                ModelState.AddModelError("Id", "This reader does not exist");
                return View();
            }

            //暂不允许修改管理员信息
            if (reader.AdminRole == 8)
            {
                ModelState.AddModelError("Id", "Can not edit the info of admin");
                return View();
            }

            if (viewModel.Name != null) reader.Name = viewModel.Name;
            //转换枚举属性
            var newSex = (SexEnum) viewModel.Sex;
            reader.Sex = newSex.ToString();

            await _context.SaveChangesAsync();
            ViewBag.isSuccess = true;
            return View();
        }

        [HttpGet]
        public IActionResult DeleteReader(int id)
        {
            //如果路由中没有传入要删除的读者id（id默认值为0），则重定向
            if (id == 0)
            {
                RedirectToAction(nameof(DisplayReaders));
            }
            ViewBag.id = id;
            return View();
        }

        public async Task<IActionResult> DeleteReader(ReaderViewModel viewModel)
        {
            var rdDal = new ReaderDal(_context);
            var reader = await rdDal.GetById(viewModel.Id);
            //查询为空则返回
            if (reader == null)
            {
                ModelState.AddModelError("Id", "This user does not exist");
                return View();
            }

            //暂不允许删除管理账号
            if (reader.AdminRole == 8)
            {
                ModelState.AddModelError("Id", "Can not delete an admin account");
                return View();
            }

            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DisplayReaders));
        }
    }
}