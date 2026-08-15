"use client";

import Image from "next/image";
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";
import { ArrowUpLeft } from "lucide-react";
import packageBg from "@/../public/images/packageCardBg.jpg";
import project1 from "@/../public/images/project1.png";
import project2 from "@/../public/images/project2.jpg";
import project3 from "@/../public/images/project3.png";
import { interactive } from "@/lib/utils";

const silverGradient =
  "linear-gradient(135deg, #e8e8e8 0%, #a8a8a8 50%, #d4d4d4 100%)";

export default function BottomCards() {
  const t = useTranslations("bottomCards");

  const projectImages = [project1, project2, project3, packageBg];

  return (
    <section className="w-full px-4 md:px-10 pt-4">
      <div className="flex flex-col md:flex-row gap-4 md:gap-6">
        {/* کارت ۱: پروژه‌ها - دسکتاپ */}
        <div className="animate-card-rise card-lift group hidden md:flex relative w-full md:w-1/2 h-48 md:h-56 rounded-2xl overflow-hidden bg-card items-center justify-between px-6">
          <div className="flex items-center gap-2 lg:gap-3">
            {projectImages.map((img, i) => (
              <div
                key={i}
                className={`relative w-16 h-20 lg:w-20 lg:h-24 xl:w-24 xl:h-28 rounded-[999px] overflow-hidden shrink-0 ${
                  i < 2 ? "hidden lg:block" : "block"
                }`}
              >
                <Image
                  src={img}
                  alt={`project-${i + 1}`}
                  fill
                  className="object-cover card-image-zoom"
                />
              </div>
            ))}
          </div>

          <Link href="/projects" className={`flex flex-col items-start gap-4 ${interactive}`}>
            <span
              className="arrow-hover flex items-center justify-center w-11 h-11 rounded-full shrink-0"
              style={{ background: silverGradient }}
            >
              <ArrowUpLeft size={18} className="text-black" />
            </span>
            <p className="text-white text-base md:text-lg font-bold max-w-[140px]">
              {t("projects.cta")}
            </p>
          </Link>
        </div>

        {/* کارت ۱: پروژه‌ها - موبایل */}
        <div className="animate-card-rise flex md:hidden flex-col w-full rounded-2xl overflow-hidden bg-card p-4 gap-4">
          <Link href="/projects" className={`flex items-center justify-between ${interactive}`}>
            <p className="text-white text-base font-bold">{t("projects.cta")}</p>
            <span
              className="arrow-hover flex items-center justify-center w-10 h-10 rounded-full shrink-0"
              style={{ background: silverGradient }}
            >
              <ArrowUpLeft size={16} className="text-black" />
            </span>
          </Link>

          <div className="flex items-center gap-2">
            {projectImages.map((img, i) => (
              <div
                key={i}
                className="relative flex-1 aspect-[3/4] rounded-2xl overflow-hidden min-w-0"
              >
                <Image src={img} alt={`project-${i + 1}`} fill className="object-cover" />
              </div>
            ))}
          </div>
        </div>

        {/* کارت ۲: پکیج اقتصادی */}
        <div className="animate-card-rise-delay-1 card-lift group relative w-full md:w-1/2 h-48 md:h-56 rounded-2xl overflow-hidden flex bg-card p-2 gap-2">
          <div className="relative w-[80%] h-full rounded-xl overflow-hidden">
            <Image
              src={packageBg}
              alt={t("package.title")}
              fill
              className="object-cover card-image-zoom"
            />
            <div className="absolute inset-0 bg-black/40" />
            <p className="absolute inset-0 flex items-center justify-center z-10 text-white text-sm md:text-base font-bold px-4 text-center">
              {t("package.title")}
            </p>
          </div>

          <Link
            href="/services"
            className={`relative w-[20%] h-full flex flex-col items-start justify-center gap-4 pl-2 pr-1 ${interactive}`}
          >
            <span
              className="arrow-hover flex items-center justify-center w-11 h-11 rounded-full shrink-0"
              style={{ background: silverGradient }}
            >
              <ArrowUpLeft size={18} className="text-black" />
            </span>
            <p className="text-white text-sm md:text-base font-bold leading-snug">
              {t("package.cta")}
            </p>
          </Link>
        </div>
      </div>
    </section>
  );
}