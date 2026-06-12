using System.Reflection;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AmincoDbContext(DbContextOptions<AmincoDbContext> options) : DbContext(options)
    {
        public DbSet<Section> Sections { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<SpecialSale> SpecialSales { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<CustomDesignRequest> CustomDesignRequests { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioImage> PortfolioImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketReply> TicketReplies { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}