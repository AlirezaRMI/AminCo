using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Logics.Intefaces;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController(
        IUserService userService,
        IArticleService articleService,
        IPortfolioService portfolioService,
        IOrderService orderService,
        IProductService productService)
        : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalUsers = (await userService.GetAllAsync()).Count,
                TotalArticles = (await articleService.GetAllAsync(false)).Count,
                TotalPortfolios = (await portfolioService.GetAllAsync()).Count,
                TotalProducts = (await productService.GetAllAsync()).Count,
                TotalOrders = (await orderService.GetAllAsync()).Count,
                RecentArticles = (await articleService.GetAllAsync(true)).Take(5).ToList(),
                RecentOrders = (await orderService.GetAllAsync()).Take(5).ToList()
            };
            return View(model);
        }
    }

    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalArticles { get; set; }
        public int TotalPortfolios { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public List<Application.DTOs.Articles.ArticleDto> RecentArticles { get; set; }
        public List<Application.DTOs.Orders.OrderDto> RecentOrders { get; set; }
    }
}