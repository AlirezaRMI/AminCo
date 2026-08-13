"use client";

import { usePathname, useRouter } from "@/i18n/navigation";
import { useLocale } from "next-intl";
import { interactive } from "@/lib/utils";

export default function LanguageSwitcher() {
  const pathname = usePathname();
  const router = useRouter();
  const locale = useLocale();

  const handleSwitch = () => {
    const nextLocale = locale === "fa" ? "en" : "fa";

    // اگه مرورگر از View Transition پشتیبانی کنه، انتقال نرم انجام می‌شه
    if (document.startViewTransition) {
      document.startViewTransition(() => {
        router.replace(pathname, { locale: nextLocale });
      });
    } else {
      router.replace(pathname, { locale: nextLocale });
    }
  };

  const isEn = locale === "en";

  return (
    <button
      onClick={handleSwitch}
      aria-label="Switch language"
      className={`relative w-14 h-7 rounded-full bg-card-light border border-white/15 transition-colors duration-300 ${interactive}`}
    >
      {/* دایره‌ی متحرک */}
      <span
        className={`absolute top-0.5 w-6 h-6 rounded-full bg-accent flex items-center justify-center text-[10px] font-bold text-white transition-all duration-300 ease-in-out ${
          isEn ? "right-0.5" : "left-0.5"
        }`}
      >
        {isEn ? "EN" : "FA"}
      </span>
    </button>
  );
}