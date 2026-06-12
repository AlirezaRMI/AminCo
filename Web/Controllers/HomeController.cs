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

        [HttpPost]
        public async Task<IActionResult> SubmitContact(ContactFormModel form)
        {
            if (!ModelState.IsValid)
            {
                TempData["ContactError"] = "لطفا اطلاعات را صحیح وارد کنید.";
                return RedirectToAction(nameof(Index));
            }
            
            logger.LogInformation("Contact from {Name} - {Email}", form.FullName, form.Email);
            TempData["ContactSuccess"] = "پیام شما با موفقیت دریافت شد.";
            return RedirectToAction(nameof(Index));
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

  
    public class ContactFormModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public bool AgreePrivacy { get; set; }
        public string Email { get; set; }
    }
}