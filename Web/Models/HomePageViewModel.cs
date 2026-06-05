using Application.DTOs.AboutUs;
using Application.DTOs.Articles;
using Application.DTOs.ContactInfo;
using Application.DTOs.Portfolios;

namespace Web.Models
{
    public class HomePageViewModel
    {
        public AboutUsDto AboutUs { get; set; }
        public ContactInfoDto ContactInfo { get; set; }
        public IReadOnlyList<ArticleDto> LatestArticles { get; set; }
        public IReadOnlyList<PortfolioDto> Portfolios { get; set; }
        
        public int StatCustomers { get; set; } = 4000;
        public int StatProjects { get; set; } = 34500;
        public int StatYears { get; set; } = 100;
    }
}