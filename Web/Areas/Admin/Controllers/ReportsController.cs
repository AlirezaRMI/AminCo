using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Rotativa.AspNetCore;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReportsController(IProductService productService, IOrderService orderService, IUserService userService)
        : Controller
    {
        public IActionResult Index() => View();

        public async Task<IActionResult> ProductsExcel()
        {
            var products = await productService.GetAllAsync();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("محصولات");
            ws.Cells[1, 1].Value = "شناسه";
            ws.Cells[1, 2].Value = "نام";
            ws.Cells[1, 3].Value = "قیمت";
            ws.Cells[1, 4].Value = "قیمت تخفیف";
            ws.Cells[1, 5].Value = "موجودی";
            ws.Cells[1, 6].Value = "دسته‌بندی";
            ws.Cells[1, 7].Value = "وضعیت";

            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                ws.Cells[i + 2, 1].Value = p.Id;
                ws.Cells[i + 2, 2].Value = p.Name;
                ws.Cells[i + 2, 3].Value = p.Price;
                ws.Cells[i + 2, 4].Value = p.DiscountPrice;
                ws.Cells[i + 2, 5].Value = p.StockQuantity;
                ws.Cells[i + 2, 6].Value = p.CategoryName;
                ws.Cells[i + 2, 7].Value = p.IsActive ? "فعال" : "غیرفعال";
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
        }
        
        public async Task<IActionResult> ProductsPdf()
        {
            var products = await productService.GetAllAsync();
            return new ViewAsPdf("ProductsPdf", products)
            {
                FileName = "Products.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
        
        public async Task<IActionResult> OrdersExcel()
        {
            var orders = await orderService.GetAllAsync();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("سفارشات");
            ws.Cells[1, 1].Value = "شناسه سفارش";
            ws.Cells[1, 2].Value = "کاربر";
            ws.Cells[1, 3].Value = "تاریخ";
            ws.Cells[1, 4].Value = "مبلغ کل";
            ws.Cells[1, 5].Value = "وضعیت";
            ws.Cells[1, 6].Value = "پرداخت";

            for (int i = 0; i < orders.Count; i++)
            {
                var o = orders[i];
                ws.Cells[i + 2, 1].Value = o.Id;
                ws.Cells[i + 2, 2].Value = o.UserId;
                ws.Cells[i + 2, 3].Value = o.OrderDate.ToString("yyyy/MM/dd HH:mm");
                ws.Cells[i + 2, 4].Value = o.TotalAmount;
                ws.Cells[i + 2, 5].Value = o.Status.ToString();
                ws.Cells[i + 2, 6].Value = o.IsPaid ? "پرداخت شده" : "پرداخت نشده";
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
        }
        
        public async Task<IActionResult> OrdersPdf()
        {
            var orders = await orderService.GetAllAsync();
            return new ViewAsPdf("OrdersPdf", orders)
            {
                FileName = "Orders.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
        
        public async Task<IActionResult> UsersExcel()
        {
            var lin = new LicenseContext();

            var users = await userService.GetAllAsync();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("کاربران");
            ws.Cells[1, 1].Value = "شناسه";
            ws.Cells[1, 2].Value = "نام کامل";
            ws.Cells[1, 3].Value = "ایمیل";
            ws.Cells[1, 4].Value = "شماره تماس";
            ws.Cells[1, 5].Value = "تاریخ ثبت‌نام";
            ws.Cells[1, 6].Value = "وضعیت";

            for (int i = 0; i < users.Count; i++)
            {
                var u = users[i];
                ws.Cells[i + 2, 1].Value = u.Id;
                ws.Cells[i + 2, 2].Value = u.FullName;
                ws.Cells[i + 2, 3].Value = u.Email;
                ws.Cells[i + 2, 4].Value = u.PhoneNumber;
                ws.Cells[i + 2, 5].Value = u.CreatedAt.ToString("yyyy/MM/dd");
                ws.Cells[i + 2, 6].Value = u.IsActive ? "فعال" : "غیرفعال";
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
        }
        
        public async Task<IActionResult> UsersPdf()
        {
            var users = await userService.GetAllAsync();
            return new ViewAsPdf("UsersPdf", users)
            {
                FileName = "Users.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}