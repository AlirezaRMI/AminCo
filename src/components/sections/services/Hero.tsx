
"use client";

import { useState } from "react";
import Image from "next/image";
import { useTranslations } from "next-intl";
import { ChevronDown } from "lucide-react";
import servicesHeroBg from "@/../public/images/servicesHeroBg.png";
import { interactive } from "@/lib/utils";

export default function ServicesHero() {
  const t = useTranslations("servicesHero");
  const [isClicked, setIsClicked] = useState(false);

  const scrollToNext = () => {
    setIsClicked(true);
    window.scrollTo({
      top: window.innerHeight * 0.9,
      behavior: "smooth",
    });
    setTimeout(() => setIsClicked(false), 300);
  };

  return (
    <section className="w-full px-4 md:px-10 pt-4">
      <div className="relative h-[400px] md:h-[460px] w-full overflow-visible">
        <Image
          src={servicesHeroBg}
          alt="خدمات آمین کو"
          fill
          className="object-full object-center"
          priority
        />

        <div className="relative z-10 h-full flex flex-col justify-center items-center text-center px-3 md:px-10 gap-3">
          <h1 className="animate-fade-in-up text-xl md:text-3xl font-bold text-white max-w-lg leading-relaxed">
            {t("title")}
          </h1>
          <p className="animate-fade-in-up-delay-1 text-sm md:text-base text-white/60 max-w-md">
            {t("subtitle")}
          </p>
        </div>

  <button
  onClick={scrollToNext}
  aria-label="scroll down"
  className={`animate-fade-in-up-delay-2 group absolute flex items-center justify-center w-10 h-10 sm:w-11 sm:h-11 md:w-12 md:h-12 rounded-full border border-white/30 bg-black/20 backdrop-blur-sm transition-all duration-300 hover:scale-110 hover:border-white/70 hover:bg-black/40 ${
    isClicked ? "animate-pulse-click" : ""
  } ${interactive}`}
  style={{
    bottom: "4%",
    right: "4%",
  }}
>
  <ChevronDown
    size={20}
    strokeWidth={2}
    className="text-white/80 animate-bounce transition-all duration-300 group-hover:stroke-[3px] group-hover:text-white"
  />
</button>
      </div>
    </section>
  );
}