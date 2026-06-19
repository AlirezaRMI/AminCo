using Microsoft.AspNetCore.Mvc;
using Application.Logics.Intefaces;
using Application.DTOs.CustomDesignRequests;
using System.Security.Claims;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(
        ICustomDesignRequestService customDesignRequestService,
        ILogger<ContactController> logger)
        : ControllerBase
    {
        [HttpPost("submit")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Submit([FromForm] ContactFormModel model)
        {
            try
            {
                logger.LogInformation("📩 Contact form received: {FullName}, {Phone}", model.FullName, model.PhoneNumber);

                if (string.IsNullOrWhiteSpace(model.FullName))
                    return BadRequest(new { success = false, message = "لطفاً نام خود را وارد کنید." });

                if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                    return BadRequest(new { success = false, message = "لطفاً شماره تماس را وارد کنید." });

                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized(new { success = false, message = "لطفاً وارد حساب کاربری خود شوید." });
                }

                var dto = new CreateCustomDesignRequestDto
                {
                    FullName = model.FullName,
                    Email = userEmail,
                    Phone = model.PhoneNumber,
                    Description = model.Description ?? ""
                };

                await customDesignRequestService.CreateAsync(dto);
                logger.LogInformation("✅ Contact saved for {Email}", userEmail);

                return Ok(new { success = true, message = "پیام شما با موفقیت دریافت شد. به زودی با شما تماس می‌گیریم." });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Error in Submit: {Message}", ex.Message);
                return StatusCode(500, new { success = false, message = "خطای سرور: " + ex.Message });
            }
        }
    }

    public class ContactFormModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public bool AgreePrivacy { get; set; }
    }
}