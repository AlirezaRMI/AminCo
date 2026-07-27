"use client";

import { useState } from "react";
import { Link } from "@/i18n/navigation";
import { useTranslations } from "next-intl";
import { Menu, X, ChevronDown } from "lucide-react";
import LanguageSwitcher from "./LanguageSwitcher";
import { NavLink } from "@/types/content";
import { interactive } from "@/lib/utils";

const navLinks: NavLink[] = [
  { key: "home", href: "/" },
  {
    key: "services",
    href: "/services",
    submenu: [
      { key: "services", href: "/services/1" },
      { key: "services", href: "/services/2" },
    ],
  },
  { key: "blog", href: "/blog" },
  { key: "contact", href: "/about" }, // موقتاً به about وصل شد، بعداً برگردون به /contact
];

export default function Navbar({ overlay = false }: { overlay?: boolean }) {
  const t = useTranslations("nav");
  const [isOpen, setIsOpen] = useState(false);
  const [openSubmenu, setOpenSubmenu] = useState<string | null>(null);
  const [mobileSubmenu, setMobileSubmenu] = useState<string | null>(null);

  return (
    <header
      style={overlay ? { position: "absolute", top: 0, left: 0, right: 0, zIndex: 50 } : undefined}
      className={`w-full ${overlay ? "bg-transparent" : "relative bg-background z-50"}`}
    >
      <nav className="flex items-center justify-end pt-6 md:pt-8 pb-4 pr-2 md:pr-3 pl-0 w-full">
        {/* موبایل */}
        <div className="flex md:hidden items-center justify-between w-full">
          <button
            className={`text-white ${interactive}`}
            onClick={() => setIsOpen(!isOpen)}
            aria-label="toggle menu"
          >
            {isOpen ? <X size={24} /> : <Menu size={24} />}
          </button>
          <div className="flex items-center gap-3">
            <LanguageSwitcher />
            <Link href="/" className={`text-lg font-bold tracking-wide text-white ${interactive}`}>
              AMIN.CO
            </Link>
          </div>
        </div>

        {/* دسکتاپ */}
       <div className="hidden md:flex items-start gap-20 pl-20">
  <ul className="flex items-center gap-8 text-base text-white/80">
            {navLinks.map((link) => (
              <li
                key={link.href}
                className="relative"
                onMouseEnter={() => link.submenu && setOpenSubmenu(link.href)}
                onMouseLeave={() => link.submenu && setOpenSubmenu(null)}
              >
              <Link
  href={link.href}
  className={`flex items-center gap-1 whitespace-nowrap hover:text-white ${interactive}`}
>
                  {t(link.key)}
                  {link.submenu && (
                    <ChevronDown
                      size={14}
                      className={`transition-transform ${
                        openSubmenu === link.href ? "rotate-180" : ""
                      }`}
                    />
                  )}
                </Link>
                {link.submenu && openSubmenu === link.href && (
                  <ul className="absolute top-full right-0 mt-2 min-w-[160px] rounded-md bg-card border border-border-subtle py-2 shadow-lg">
                    {link.submenu.map((sub, i) => (
                      <li key={i}>
                        <Link
                          href={sub.href}
                          className={`block px-4 py-2 text-sm text-white/70 hover:text-white hover:bg-card-light ${interactive}`}
                        >
                          {t(sub.key)}
                        </Link>
                      </li>
                    ))}
                  </ul>
                )}
              </li>
            ))}
          </ul>

          <div className="flex flex-col items-center gap-2">
    <Link href="/" className={`text-xl font-bold tracking-wide text-white ${interactive}`}>
      AMIN.CO
    </Link>
    <LanguageSwitcher />
          </div>
        </div>
      </nav>

      {/* منوی موبایل باز شونده */}
      {isOpen && (
        <ul className="md:hidden flex flex-col gap-1 px-6 pb-6 text-white/80 text-sm bg-background/95">
          {navLinks.map((link) => (
            <li key={link.href}>
              {link.submenu ? (
                <>
                  <button
                    className={`w-full flex items-center gap-2 py-2 ${interactive}`}
                    onClick={() =>
                      setMobileSubmenu(mobileSubmenu === link.href ? null : link.href)
                    }
                  >
                    {t(link.key)}
                    <ChevronDown
                      size={16}
                      className={`transition-transform ${
                        mobileSubmenu === link.href ? "rotate-180" : ""
                      }`}
                    />
                  </button>
                  {mobileSubmenu === link.href && (
                    <ul className="pr-4 flex flex-col gap-2 pb-2">
                      {link.submenu.map((sub, i) => (
                        <li key={i}>
                          <Link
                            href={sub.href}
                            className={`block py-1 text-white/60 ${interactive}`}
                            onClick={() => setIsOpen(false)}
                          >
                            {t(sub.key)}
                          </Link>
                        </li>
                      ))}
                    </ul>
                  )}
                </>
              ) : (
                <Link
                  href={link.href}
                  className={`block py-2 ${interactive}`}
                  onClick={() => setIsOpen(false)}
                >
                  {t(link.key)}
                </Link>
              )}
            </li>
          ))}
        </ul>
      )}
    </header>
  );
}