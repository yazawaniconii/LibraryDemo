using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Web.Dal;
using Library.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly LibDbContext _context;

        //通过构造函数的方式添加连接数据库上下文的服务，服务在Startup.cs注册
        public AccountController(LibDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            //重定向至登录页面前会传递一个当前页面的路径，接收这个字符串用于之后再次重定向
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var readerDal = new ReaderDal(_context);
            var user = await readerDal.GetById(loginUser.Id);
            //用户不存在
            if (user == null)
            {
                ModelState.AddModelError("Id", "User does not exist");
                ViewData["ReturnUrl"] = loginUser.ReturnUrl;
                return View();
            }

            //验证密码是否正确
            if (user.Password == loginUser.Password)
            {
                //若正确,生产保持登录状态的cookie
                //认证系统由微软的库提供,详情可看asp.net core cookie登录文档
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, loginUser.Id.ToString()),
                    //添加AdminRole信息作为凭证信息之一,之后用于鉴权和授权
                    new(ClaimTypes.Role, user.AdminRole.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            }
            else
            {
                //登录失败则跳转回登录界面
                ModelState.AddModelError("Password", "Password is not correct");
                ViewData["ReturnUrl"] = loginUser.ReturnUrl;
                return View();
            }
            //登录成功后重定向,若无重定向路径则重定向至根路径
            return Redirect(loginUser.ReturnUrl ?? "/");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            //详情可看文档
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}