using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Application.Logics.Intefaces;
using System.Diagnostics;


namespace Web.Controllers
{
    public class HomeController(
        IArticleService articleService,
        IPortfolioService portfolioService,
        IAboutUsService aboutUsService,
        IServiceService serviceService,       
        IPartnerService partnerService,  
        IContactInfoService contactInfoService,
        ILogger<HomeController> logger)
        : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel
            {
                AboutUs = await aboutUsService.GetAsync(),
                ContactInfo = await contactInfoService.GetAsync(),
                LatestArticles = await articleService.GetAllAsync(onlyPublished: true),
                Portfolios = await portfolioService.GetActivePortfoliosAsync(),
                Services = await serviceService.GetAllActiveAsync(),
                Partners = await partnerService.GetAllActiveAsync(),
            };
            return View(model);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult UnderConstruction()
        {
            return View("~/Views/Shared/UnderConstruction.cshtml");
        }
    }
}