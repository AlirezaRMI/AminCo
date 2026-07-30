
"use client";

import { useCallback, useEffect, useState } from "react";
import Image from "next/image";
import useEmblaCarousel from "embla-carousel-react";
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";
import { ChevronLeft, ChevronRight } from "lucide-react";
import blogHero from "@/../public/images/blogHero.png";

import { interactive } from "@/lib/utils";

interface SlideData {
  title: string;
  subtitle: string;
}

const images = [blogHero];

export default function BlogHero() {
  const t = useTranslations("blogHero");
  const slides = t.raw("slides") as SlideData[];

  const [emblaRef, emblaApi] = useEmblaCarousel({
    loop: true,
    direction: "rtl",
  });
  const [selectedIndex, setSelectedIndex] = useState(0);

  const onSelect = useCallback(() => {
    if (!emblaApi) return;
    setSelectedIndex(emblaApi.selectedScrollSnap());
  }, [emblaApi]);

  useEffect(() => {
    if (!emblaApi) return;
    emblaApi.on("select", onSelect);
    onSelect();
  }, [emblaApi, onSelect]);

  const scrollPrev = useCallback(() => emblaApi?.scrollPrev(), [emblaApi]);
  const scrollNext = useCallback(() => emblaApi?.scrollNext(), [emblaApi]);

  return (
    <section className="relative w-full h-[500px] md:h-[600px] overflow-hidden">
      <div className="overflow-hidden h-full" ref={emblaRef}>
        <div className="flex h-full">
          {slides.map((slide, i) => (
            <div key={i} className="relative flex-[0_0_100%] h-full">
              <Image src={images[i]} alt={slide.title} fill className="object-cover" priority={i === 0} />
              <div className="absolute inset-0 bg-black/40" />
            </div>
          ))}
        </div>
      </div>

      {/* بج دسته‌بندی - گوشه بالا-راست */}
      <div className="absolute top-24 md:top-28 right-4 md:right-10 z-10">
        <Link
          href="/blog"
          className={`bg-white text-black text-sm font-bold px-5 py-2.5 rounded-full ${interactive}`}
        >
          {t("category")}
        </Link>
      </div>

      {/* دکمه‌های قبلی/بعدی */}
      <button
        onClick={scrollPrev}
        aria-label="prev"
        className={`absolute top-1/2 -translate-y-1/2 right-4 md:right-6 z-10 flex items-center justify-center w-9 h-9 rounded-full bg-black/30 text-white hover:bg-black/50 ${interactive}`}
      >
        <ChevronRight size={18} />
      </button>
      <button
        onClick={scrollNext}
        aria-label="next"
        className={`absolute top-1/2 -translate-y-1/2 left-4 md:left-6 z-10 flex items-center justify-center w-9 h-9 rounded-full bg-black/30 text-white hover:bg-black/50 ${interactive}`}
      >
        <ChevronLeft size={18} />
      </button>

      {/* محتوای متنی پایین کارت */}
      <div className="absolute bottom-16 md:bottom-20 left-0 right-0 z-10 bg-black/40 backdrop-blur-sm px-6 md:px-10 py-6">
        <h1 className="text-lg md:text-2xl font-bold text-white text-center mb-2">
          {slides[selectedIndex]?.title}
        </h1>
        <p className="text-sm text-white/60 text-center max-w-2xl mx-auto leading-relaxed">
          {slides[selectedIndex]?.subtitle}
          {"  "}
          <Link href="/blog" className={`text-white font-bold underline ${interactive}`}>
            {t("readMore")}
          </Link>
        </p>
      </div>

      {/* نقطه‌های پایین */}
      <div className="absolute bottom-6 left-0 right-0 z-10 flex items-center justify-center gap-2">
        {slides.map((_, i) => (
          <button
            key={i}
            onClick={() => emblaApi?.scrollTo(i)}
            aria-label={`slide-${i}`}
            className={`h-1.5 rounded-full transition-all duration-300 ${
              i === selectedIndex ? "w-6 bg-white" : "w-1.5 bg-white/30"
            }`}
          />
        ))}
      </div>
    </section>
  );
}