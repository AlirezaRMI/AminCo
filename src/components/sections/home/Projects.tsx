"use client";

import { useCallback, useEffect, useState } from "react";
import Image from "next/image";
import useEmblaCarousel from "embla-carousel-react";
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";
import { ArrowLeft, ArrowRight } from "lucide-react";
import project1 from "@/../public/images/project1.png";
import project2 from "@/../public/images/project2.jpg";
import project3 from "@/../public/images/project3.png";
import project4 from "@/../public/images/project4.png";
import project5 from "@/../public/images/project5.png";
import viewAllPatternUp from "@/../public/images/viewAllPatternUp.png";
import { interactive } from "@/lib/utils";
import { useInView } from "framer-motion";
import { useRef } from "react";



const images = [project1, project2, project3, project4, project5];

export default function Projects() {
  const t = useTranslations("projects");
  const items = t.raw("items") as ProjectItem[];

  const sectionRef = useRef(null);
  const isInView = useInView(sectionRef, { once: true, amount: 0.4 });

  const [emblaRef, emblaApi] = useEmblaCarousel({
    loop: true,
    direction: "rtl",
    align: "center",
    containScroll: false,
    watchDrag: true,
  });

  const [selectedIndex, setSelectedIndex] = useState(2);

  const onSelect = useCallback(() => {
    if (!emblaApi) return;
    setSelectedIndex(emblaApi.selectedScrollSnap());
  }, [emblaApi]);

  useEffect(() => {
    if (!emblaApi) return;
    emblaApi.on("select", onSelect);
    onSelect();
  }, [emblaApi, onSelect]);

  // چرخش خودکار - فقط وقتی این بخش وارد دید (اسکرول) شد
  useEffect(() => {
    if (!emblaApi || !isInView) return;

    const targetIndex = items.length - 2;
    let hasStopped = false;

    const interval = setInterval(() => {
      if (hasStopped) return;
      emblaApi.scrollNext();
      if (emblaApi.selectedScrollSnap() === targetIndex) {
        hasStopped = true;
        clearInterval(interval);
      }
    }, 600);

    return () => clearInterval(interval);
  }, [emblaApi, isInView, items.length]);

  const scrollPrev = useCallback(() => emblaApi?.scrollPrev(), [emblaApi]);
  const scrollNext = useCallback(() => emblaApi?.scrollNext(), [emblaApi]);

  return (
      <section ref={sectionRef} className="w-full px-4 md:px-10 py-4">
      <div className="relative rounded-3xl bg-card overflow-hidden px-5 md:px-12 py-10 md:py-14">
        {/* پترن دکوری بالا-چپ */}
        <svg
          className="absolute top-0 left-0 w-56 h-56 md:w-72 md:h-72 opacity-[0.06] pointer-events-none"
          viewBox="0 0 300 300"
          fill="none"
        >
          <path d="M -20 60 Q 60 0, 140 60 T 300 60" stroke="white" strokeWidth="1.5" />
          <path d="M -20 110 Q 60 50, 140 110 T 300 110" stroke="white" strokeWidth="1.5" />
          <path d="M -20 160 Q 60 100, 140 160 T 300 160" stroke="white" strokeWidth="1.5" />
        </svg>

        {/* هدر: تیتر راست، دکمه مشاهده همه چپ */}
        <div className="relative z-10 flex flex-col md:flex-row items-start md:items-center justify-between gap-6 mb-10">
          <div className="text-right order-1">
            <h2 className="text-2xl md:text-4xl font-bold text-white">{t("title")}</h2>
            <p className="text-sm md:text-base text-white/50 mt-2">{t("subtitle")}</p>
          </div>

          <Link
            href="/projects"
            className={`relative flex items-center gap-2 text-sm text-white px-5 py-2.5 rounded-full overflow-hidden order-2 ${interactive}`}
          >
          
            <Image src={viewAllPatternUp} alt="" fill className="object-cover -z-10" />
            
            <span className="relative z-10">{t("viewAll")}</span>
            <ArrowLeft size={16} className="relative z-10" />
          </Link>
        </div>

        {/* کاروسل */}
        <div className="relative z-10">
          <div className="overflow-visible" ref={emblaRef}>
            <div className="flex items-end gap-3 md:gap-4">
              {items.map((item, i) => {
                const isActive = i === selectedIndex;
                const isEvenShape = i % 2 === 0;
                return (
                  <div
                    key={i}
                    className="flex-[0_0_55%] sm:flex-[0_0_32%] md:flex-[0_0_19%] transition-all duration-500"
                  >
                    <div
                      className="relative overflow-hidden transition-all duration-500"
                      style={{
                        height: isActive ? "420px" : "300px",
                        borderTopLeftRadius: isEvenShape ? "80px" : "16px",
                        borderTopRightRadius: isEvenShape ? "16px" : "80px",
                        borderBottomLeftRadius: isEvenShape ? "16px" : "80px",
                        borderBottomRightRadius: isEvenShape ? "80px" : "16px",
                      }}
                    >
                      <Image src={images[i]} alt={item.title} fill className="object-cover" />

                      {isActive && (
                        <>
                          <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/20 to-transparent" />
                          <div className="absolute bottom-0 right-0 left-0 p-5 z-10">
                            <p className="text-white text-sm md:text-base font-bold leading-relaxed mb-4">
                              {item.title}
                            </p>
                            <Link
                              href="/projects"
                              className={`flex items-center gap-2 text-xs md:text-sm text-white/80 hover:text-white ${interactive}`}
                            >
                              <ArrowLeft size={14} />
                              {t("viewDetails")}
                            </Link>
                          </div>
                        </>
                      )}
                    </div>
                  </div>
                );
              })}
            </div>
          </div>

          {/* دکمه ناوبری دوطرفه شناور */}
          <div className="hidden md:flex absolute top-[150px] left-1/2 -translate-x-1/2 items-center bg-background/90 backdrop-blur-sm rounded-full overflow-hidden z-20 shadow-lg">
            <button
              onClick={scrollPrev}
              aria-label="prev"
              className={`flex items-center justify-center w-12 h-12 text-white hover:bg-white/10 ${interactive}`}
            >
              <ArrowLeft size={18} />
            </button>
            <div className="w-px h-6 bg-white/15" />
            <button
              onClick={scrollNext}
              aria-label="next"
              className={`flex items-center justify-center w-12 h-12 text-white hover:bg-white/10 ${interactive}`}
            >
              <ArrowRight size={18} />
            </button>
          </div>
        </div>
      </div>
    </section>
  );
}