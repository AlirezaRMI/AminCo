
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";
import LogoBadge from "@/components/ui/LogoBadge";
import { interactive } from "@/lib/utils";

export default function Footer() {
  const t = useTranslations("footer");
  const links = t.raw("links") as Record<string, string>;

  const linkHrefs: Record<string, string> = {
    home: "/",
    services: "/services",
    blog: "/blog",
    contact: "/about",
  };

  return (
    <footer className="relative bg-card pt-16 pb-8 px-4 text-center">
      <div className="absolute -top-12 left-1/2 -translate-x-1/2">
        <LogoBadge />
      </div>

      <h2 className="text-xl md:text-2xl font-bold text-white mb-3 mt-4">
        {t("companyName")}
      </h2>

      <p className="max-w-md mx-auto text-sm text-white/50 mb-8 leading-relaxed">
        {t("description")}
      </p>

      <ul className="flex items-center justify-center gap-6 flex-wrap text-sm text-white/70 mb-8">
        {Object.entries(links).map(([key, label]) => (
          <li key={key}>
            <Link href={linkHrefs[key]} className={`hover:text-white ${interactive}`}>
              {label}
            </Link>
          </li>
        ))}
      </ul>

      <p className="text-xs text-white/30">{t("copyright")}</p>
    </footer>
  );
}