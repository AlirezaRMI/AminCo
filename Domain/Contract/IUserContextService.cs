namespace Domain.Contract
{
    public interface IUserContextService
    {
        /// <summary>
        /// شناسه کاربر فعلی (از Claim NameIdentifier یا sub)
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// لیست نقش‌های کاربر فعلی (از Claimهای Role)
        /// </summary>
        string[] Roles { get; }

        /// <summary>
        /// لیست دسترسی‌های سطحی (Permissions) کاربر – در صورت وجود
        /// </summary>
        string[] Permissions { get; }

        /// <summary>
        /// آیا کاربر لاگین کرده؟
        /// </summary>
        bool IsAuthenticated { get; }
    }
}