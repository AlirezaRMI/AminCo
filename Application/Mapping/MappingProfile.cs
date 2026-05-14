// Mappings/MappingProfile.cs

using Application.DTOs.AboutUs;
using Application.DTOs.Articles;
using Application.DTOs.Carts;
using Application.DTOs.Categories;
using Application.DTOs.ContactInfo;
using Application.DTOs.CustomDesignRequests;
using Application.DTOs.DiscountCodes;
using Application.DTOs.Invoices;
using Application.DTOs.OrderItems;
using Application.DTOs.Orders;
using Application.DTOs.PortfolioImages;
using Application.DTOs.Portfolios;
using Application.DTOs.Products;
using Application.DTOs.Roles;
using Application.DTOs.Sections;
using Application.DTOs.SpecialSales;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Entites;
using OrderItemDto = Application.DTOs.Orders.OrderItemDto;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Section
            CreateMap<Section, SectionDto>();
            CreateMap<CreateSectionDto, Section>();
            CreateMap<UpdateSectionDto, Section>();

            // Category
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.Name));
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Product
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.EffectivePrice, opt => opt.MapFrom(src =>
                    src.SpecialSales.Any(ss => ss.IsActive) ? src.SpecialSales.First(ss => ss.IsActive).SalePrice :
                    src.DiscountPrice ?? src.Price));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // DiscountCode
            CreateMap<DiscountCode, DiscountCodeDto>();
            CreateMap<CreateDiscountCodeDto, DiscountCode>();
            CreateMap<UpdateDiscountCodeDto, DiscountCode>();

            // SpecialSale
            CreateMap<SpecialSale, SpecialSaleDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<CreateSpecialSaleDto, SpecialSale>();
            CreateMap<UpdateSpecialSaleDto, SpecialSale>();

            // Article
            CreateMap<Article, ArticleDto>();
            CreateMap<CreateArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();

            // CustomDesignRequest
            CreateMap<CustomDesignRequest, CustomDesignRequestDto>();
            CreateMap<CreateCustomDesignRequestDto, CustomDesignRequest>();
            CreateMap<UpdateCustomDesignRequestDto, CustomDesignRequest>();

            // AboutUs (تک رکورد)
            CreateMap<AboutUs, AboutUsDto>();
            CreateMap<UpdateAboutUsDto, AboutUs>();

            // ContactInfo (تک رکورد)
            CreateMap<ContactInfo, ContactInfoDto>();
            CreateMap<UpdateContactInfoDto, ContactInfo>();

            // Portfolio
            CreateMap<Portfolio, PortfolioDto>();
            CreateMap<CreatePortfolioDto, Portfolio>();
            CreateMap<UpdatePortfolioDto, Portfolio>();

            // PortfolioImage
            CreateMap<PortfolioImage, PortfolioImageDto>();
            CreateMap<CreatePortfolioImageDto, PortfolioImage>();
            CreateMap<UpdatePortfolioImageDto, PortfolioImage>();

            // User
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // بعداً در سرویس مقداردهی می‌شود
            CreateMap<UpdateUserDto, User>();

            // Cart
            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.MainImageUrl));
            CreateMap<AddToCartDto, CartItem>();
            CreateMap<UpdateCartItemDto, CartItem>();

            // Order
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderStatusDto, Order>();

            // OrderItem
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.MainImageUrl));
            CreateMap<CreateOrderItemDto, OrderItem>();

            // Invoice
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<CreateInvoiceDto, Invoice>();
            
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}