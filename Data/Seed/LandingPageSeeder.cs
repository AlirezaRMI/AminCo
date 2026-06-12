using Domain.Entites;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Data.Context;

namespace Data.Seed
{
    public static class LandingPageSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AmincoDbContext>();
            
            if (!context.AboutUs.Any())
            {
                context.AboutUs.Add(new AboutUs
                {
                    Title = "Amin Co – Industrial Kitchen Equipment Manufacturer in Iran",
                    Content = "Amin Co has been designing and manufacturing industrial kitchen equipment in Iran since 2008. We serve restaurants, hotels, cafeterias, and catering companies across Tehran, Isfahan, Mashhad, and other major cities. Our products include stainless steel worktables, industrial ovens, ventilation hoods, refrigeration units, and custom-made kitchen solutions. With a team of 50+ engineers and technicians, we have completed over 300 projects nationwide.",
                    ImageUrl = "/public/img/about_us/aminco-about.jpg",
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true
                });
            }
            
            // 2. ContactInfo (بدون Id)
            if (!context.ContactInfo.Any())
            {
                context.ContactInfo.Add(new ContactInfo
                {
                    Phone = "+98 21 88521436",
                    Email = "info@amin-co.ir",
                    Address = "No. 189, Valiasr St., Above Valiasr Square, Tehran, Iran",
                    WorkingHours = "Saturday to Wednesday 9:00–17:00, Thursday 9:00–13:00",
                    GoogleMapUrl = "https://goo.gl/maps/example",
                    IsActive = true
                });
            }
            
            // 3. Articles (News)
            if (!context.Articles.Any())
            {
                var articles = new List<Article>
                {
                    new Article
                    {
                        Title = "How to Choose Industrial Kitchen Equipment in Iran",
                        Slug = "choose-industrial-kitchen-equipment-iran",
                        ShortDescription = "Practical tips for selecting ovens, hoods, and refrigeration units based on Iranian market conditions.",
                        Content = "Full article content goes here. (Practical advice on local suppliers, energy efficiency, and after-sales service.)",
                        MainImageUrl = "/public/img/news/kitchen-guide.jpg",
                        PublishDate = DateTime.UtcNow.AddDays(-10),
                        ViewCount = 145,
                        IsPublished = true,
                        IsActive = true
                    },
                    new Article
                    {
                        Title = "Latest Trends in Hotel Kitchen Design – Iran Market",
                        Slug = "hotel-kitchen-trends-iran",
                        ShortDescription = "What 5-star hotels in Tehran are installing: smart ventilation, modular stainless steel islands, and combi ovens.",
                        Content = "Full article content here. (Real examples from Parsian, Espinas, and Azadi hotels.)",
                        MainImageUrl = "/public/img/news/hotel-kitchen-trends.jpg",
                        PublishDate = DateTime.UtcNow.AddDays(-18),
                        ViewCount = 98,
                        IsPublished = true,
                        IsActive = true
                    },
                    new Article
                    {
                        Title = "Why Amin Co is Trusted by Top Restaurants in Iran",
                        Slug = "aminco-trusted-iran",
                        ShortDescription = "300+ successful projects, 2-year warranty, and free installation across the country.",
                        Content = "Full article content here. (Client testimonials from well-known Tehran restaurants like Divan, Alborz, and Naderi.)",
                        MainImageUrl = "/public/img/news/aminco-trusted.jpg",
                        PublishDate = DateTime.UtcNow.AddDays(-25),
                        ViewCount = 230,
                        IsPublished = true,
                        IsActive = true
                    }
                };
                context.Articles.AddRange(articles);
            }
            
            // 4. Portfolios (Latest projects for swiper)
            if (!context.Portfolios.Any())
            {
                var portfolio1 = new Portfolio
                {
                    Title = "Sofia Restaurant Kitchen – Tehran",
                    Slug = "sofia-restaurant-tehran",
                    Description = "Complete installation of a 120m² industrial kitchen including 4-burner gas range, two convection ovens, stainless steel prep tables, and a 2-door reach-in freezer.",
                    ClientName = "Sofia Restaurant, Tehran",
                    ProjectDate = DateTime.UtcNow.AddMonths(-3),
                    Category = PortfolioCategory.Restaurant,
                    DisplayOrder = 1,
                    IsActive = true
                };
                var portfolio2 = new Portfolio
                {
                    Title = "Parsian Hotel Central Kitchen – Tehran",
                    Slug = "parsian-hotel-kitchen",
                    Description = "Design and setup of a central kitchen for a 5-star hotel, serving 800+ meals daily. Included combi ovens, tilting frying pans, blast chillers, and full ventilation system.",
                    ClientName = "Parsian Hotel Group",
                    ProjectDate = DateTime.UtcNow.AddMonths(-8),
                    Category = PortfolioCategory.Hotel,
                    DisplayOrder = 2,
                    IsActive = true
                };
                var portfolio3 = new Portfolio
                {
                    Title = "Mashhad Catering Company – Industrial Kitchen",
                    Slug = "mashhad-catering-kitchen",
                    Description = "A 300m² central kitchen for a large catering company, featuring steam jacketed kettles, a rational oven, walk-in cooler, and dishwashing area.",
                    ClientName = "Catering Khorasan, Mashhad",
                    ProjectDate = DateTime.UtcNow.AddMonths(-12),
                    Category = PortfolioCategory.Catering,
                    DisplayOrder = 3,
                    IsActive = true
                };
                var portfolio4 = new Portfolio
                {
                    Title = "Isfahan Cafe & Bistro – Modern Setup",
                    Slug = "isfahan-cafe-bistro",
                    Description = "Compact yet professional kitchen for a high-end bistro, including undercounter fridge, espresso machine, ice maker, and custom exhaust hood.",
                    ClientName = "Cafe Naghsh-e Jahan, Isfahan",
                    ProjectDate = DateTime.UtcNow.AddMonths(-5),
                    Category = PortfolioCategory.Cafe,
                    DisplayOrder = 4,
                    IsActive = true
                };
                var portfolio5 = new Portfolio
                {
                    Title = "Shiraz Hospital Kitchen – Industrial Equipment",
                    Slug = "shiraz-hospital-kitchen",
                    Description = "Installation of heavy-duty cooking line, dishwashers, and ventilation for a 200-bed hospital.",
                    ClientName = "Namazi Hospital, Shiraz",
                    ProjectDate = DateTime.UtcNow.AddMonths(-14),
                    Category = PortfolioCategory.Institutional,
                    DisplayOrder = 5,
                    IsActive = true
                };
            
                context.Portfolios.AddRange(portfolio1, portfolio2, portfolio3, portfolio4, portfolio5);
                context.SaveChanges(); // تا Idها ساخته شوند
            
                // Add images
                var images = new List<PortfolioImage>
                {
                    new PortfolioImage { PortfolioId = portfolio1.Id, ImageUrl = "/public/img/portfolio/sofia-1.jpg", Title = "Gas range & ovens", IsMain = true, DisplayOrder = 1 },
                    new PortfolioImage { PortfolioId = portfolio1.Id, ImageUrl = "/public/img/portfolio/sofia-2.jpg", Title = "Stainless tables", IsMain = false, DisplayOrder = 2 },
                    new PortfolioImage { PortfolioId = portfolio2.Id, ImageUrl = "/public/img/portfolio/parsian-1.jpg", Title = "Combi ovens", IsMain = true, DisplayOrder = 1 },
                    new PortfolioImage { PortfolioId = portfolio2.Id, ImageUrl = "/public/img/portfolio/parsian-2.jpg", Title = "Blast chiller", IsMain = false, DisplayOrder = 2 },
                    new PortfolioImage { PortfolioId = portfolio3.Id, ImageUrl = "/public/img/portfolio/mashhad-1.jpg", Title = "Steam kettle", IsMain = true, DisplayOrder = 1 },
                    new PortfolioImage { PortfolioId = portfolio3.Id, ImageUrl = "/public/img/portfolio/mashhad-2.jpg", Title = "Walk-in cooler", IsMain = false, DisplayOrder = 2 },
                    new PortfolioImage { PortfolioId = portfolio4.Id, ImageUrl = "/public/img/portfolio/isfahan-1.jpg", Title = "Espresso machine", IsMain = true, DisplayOrder = 1 },
                    new PortfolioImage { PortfolioId = portfolio4.Id, ImageUrl = "/public/img/portfolio/isfahan-2.jpg", Title = "Exhaust hood", IsMain = false, DisplayOrder = 2 },
                    new PortfolioImage { PortfolioId = portfolio5.Id, ImageUrl = "/public/img/portfolio/shiraz-1.jpg", Title = "Heavy-duty range", IsMain = true, DisplayOrder = 1 },
                };
                context.PortfolioImages.AddRange(images);
            }
            
            // 5. Partners (Our partners & clients)
            if (!context.Partners.Any())
            {
                var partners = new List<Partner>
                {
                    new Partner { Name = "Tehran Steel Industries Co.", LogoUrl = "/public/img/partners/tehran-steel.png", Website = "https://tehransteel.ir", DisplayOrder = 1, IsActive = true },
                    new Partner { Name = "Persian Ventilation System", LogoUrl = "/public/img/partners/persian-vent.png", Website = "https://persianvent.com", DisplayOrder = 2, IsActive = true },
                    new Partner { Name = "Modern Kitchen Installers", LogoUrl = "/public/img/partners/modern-install.png", Website = "#", DisplayOrder = 3, IsActive = true },
                    new Partner { Name = "Azarin Food Industry Group", LogoUrl = "/public/img/partners/azarin.png", Website = "https://azarin.com", DisplayOrder = 4, IsActive = true },
                    new Partner { Name = "Golrang Industrial Group", LogoUrl = "/public/img/partners/golrang.png", Website = "https://golrang.com", DisplayOrder = 5, IsActive = true },
                    new Partner { Name = "Iran Hoteliers Association", LogoUrl = "/public/img/partners/iran-hotel.png", Website = "https://iranhotels.ir", DisplayOrder = 6, IsActive = true }
                };
                context.Partners.AddRange(partners);
            }
            if (!context.Services.Any())
            {
                var services = new List<Service>
                {
                    new Service
                    {
                        Title = "Design & Consultation",
                        Description = "Our experts design your commercial kitchen layout based on your space, menu, and budget. Free initial consultation.",
                        DisplayOrder = 1,
                        IsActive = true
                    },
                    new Service
                    {
                        Title = "Supply of Industrial Equipment",
                        Description = "We supply high-quality stainless steel worktables, ovens, ranges, fryers, refrigerators, and ventilation hoods from top brands.",
                        DisplayOrder = 2,
                        IsActive = true
                    },
                    new Service
                    {
                        Title = "Installation & Commissioning",
                        Description = "Professional installation by certified technicians. We ensure all gas, water, and electrical connections meet safety standards.",
                        DisplayOrder = 3,
                        IsActive = true
                    },
                    new Service
                    {
                        Title = "After-Sales Support & Maintenance",
                        Description = "24/7 support, spare parts availability, and regular maintenance contracts to keep your kitchen running smoothly.",
                        DisplayOrder = 4,
                        IsActive = true
                    }
                };
                context.Services.AddRange(services);
            }

            context.SaveChanges();
        }
    }
}