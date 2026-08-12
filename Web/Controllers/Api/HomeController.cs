using Application.DTOs.AboutUs;
using Application.DTOs.Articles;
using Application.DTOs.ContactInfo;
using Application.DTOs.Partner;
using Application.DTOs.Portfolios;
using Application.DTOs.Service;
using Microsoft.AspNetCore.Mvc;
using Application.Logics.Intefaces;
using Domain.Common;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(
        IAboutUsService aboutUsService,
        IContactInfoService contactInfoService,
        IArticleService articleService,
        IPortfolioService portfolioService,
        IServiceService serviceService,
        IPartnerService partnerService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ApiResult<HomePageDataDto>> GetHomeData()
        {
            var about = await aboutUsService.GetAsync();
            var contact = await contactInfoService.GetAsync();
            var articles = await articleService.GetAllAsync(onlyPublished: true);
            var portfolios = await portfolioService.GetActivePortfoliosAsync();
            var services = await serviceService.GetAllActiveAsync();
            var partners = await partnerService.GetAllActiveAsync();

            var data = new HomePageDataDto
            {
                AboutUs = about,
                ContactInfo = contact,
                LatestArticles = articles,
                Portfolios = portfolios,
                Services = services,
                Partners = partners
            };

            return new ApiResult<HomePageDataDto>(true, ApiResultStatusCode.Success, data);
        }
    }

    public class HomePageDataDto
    {
        public AboutUsDto AboutUs { get; set; }
        public ContactInfoDto ContactInfo { get; set; }
        public IReadOnlyList<ArticleDto> LatestArticles { get; set; }
        public IReadOnlyList<PortfolioDto> Portfolios { get; set; }
        public IReadOnlyList<ServiceDto> Services { get; set; }
        public IReadOnlyList<PartnerDto> Partners { get; set; }
    }
}