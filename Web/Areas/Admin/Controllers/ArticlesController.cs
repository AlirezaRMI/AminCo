using Application.DTOs.Articles;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticlesController(IArticleService articleService) : Controller
    {
        public async Task<IActionResult> Index() => View(await articleService.GetAllAsync(false));
        
        public async Task<IActionResult> Details(long id) => View(await articleService.GetByIdAsync(id));
        
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArticleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await articleService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var article = await articleService.GetByIdAsync(id);
            var dto = new UpdateArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug,
                ShortDescription = article.ShortDescription,
                Content = article.Content,
                MainImageUrl = article.MainImageUrl,
                IsPublished = article.IsPublished
            };
            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateArticleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await articleService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await articleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}