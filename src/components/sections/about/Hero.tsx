"use client";

import { useState } from "react";
import Image from "next/image";
import { useTranslations } from "next-intl";
import { ChevronDown } from "lucide-react";
import heroKitchenAbout from "@/../public/images/heroKitchenAbout.png";
import { interactive } from "@/lib/utils";

export default function AboutHero() {
  const t = useTranslations("aboutHero");
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
      <div className="relative h-[420px] md:h-[520px] w-full rounded-2xl md:rounded-3xl overflow-hidden">
        <Image
          src={heroKitchenAbout}
          alt="آشپزخانه صنعتی"
          fill
          className="object-cover object-[center_-20%]"
          priority
        />
        <div className="absolute inset-0 bg-black/30" />

        <div className="relative z-10 flex h-full flex-col items-center justify-center text-center px-4">
          <h1 className="animate-fade-in-up text-2xl md:text-4xl font-bold text-white">
            {t("title")}
          </h1>
          <p className="animate-fade-in-up-delay-1 mt-4 max-w-md text-sm md:text-base text-white/70">
            {t("subtitle")}
          </p>
          <button
            onClick={scrollToNext}
            aria-label="scroll down"
            className={`animate-fade-in-up-delay-2 mt-8 md:mt-10 flex items-center justify-center w-12 h-12 rounded-full border border-white/40 transition-transform duration-300 hover:scale-110 hover:border-white/70 ${
              isClicked ? "animate-pulse-click" : ""
            } ${interactive}`}
          >
            <ChevronDown size={22} className="text-white/80 animate-bounce" />
          </button>
        </div>
      </div>
    </section>
  );
}