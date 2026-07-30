"use client";

import { useCallback, useEffect, useState } from "react";
import Image from "next/image";
import useEmblaCarousel from "embla-carousel-react";
import { useTranslations } from "next-intl";
import { ArrowLeft, ArrowRight } from "lucide-react";

interface Props {
  images: any[];
}

export default function ProjectGallery({ images }: Props) {
  const t = useTranslations("projectGallery");

  const [emblaRef, emblaApi] = useEmblaCarousel({
    loop: false,
    direction: "rtl",
    align: "start",
    containScroll: false,
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
    <section className="relative px-4 md:px-10 py-10 md:py-14">
      {/* سایه‌ی روشن بالای گالری - افکت نورافکن */}
      <div
        className="absolute top-0 left-1/2 -translate-x-1/2 w-[600px] h-[300px] pointer-events-none"
        style={{
          background: "radial-gradient(ellipse 50% 100% at 50% 0%, rgba(255,255,255,0.08) 0%, transparent 70%)",
        }}
      />

      <h2 className="relative  z-10 text-xl md:text-2xl font-bold text-white text-center max-w-6xl mx-auto mb-8">
        {t("title")}
      </h2>

      <div className="relative z-10 max-w-6xl mx-auto">
        <div className="overflow-visible" ref={emblaRef}>
          <div className="flex items-end gap-3 md:gap-4">
            {images.map((image, i) => {
              const isActive = i === selectedIndex;
              return (
                <div
                  key={i}
                  className="flex-[0_0_60%] sm:flex-[0_0_35%] md:flex-[0_0_19%] transition-all duration-500"
                >
                  <div
                    className="relative overflow-hidden transition-all duration-500 rounded-2xl"
                    style={{ height: isActive ? "380px" : "280px" }}
                  >
                    <Image
                      src={image}
                      alt=""
                      fill
                      className="object-cover transition-all duration-500"
                      style={{
                        filter: isActive ? "brightness(1)" : "brightness(0.5)",
                      }}
                    />

                    {isActive && (
                      <>
                        <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/10 to-transparent" />
                        <div className="absolute bottom-0 right-0 left-0 p-5 z-10">
                          <p className="text-white text-sm md:text-base font-bold leading-relaxed text-center">
                            {t("itemTitle")}
                          </p>
                        </div>
                      </>
                    )}
                  </div>
                </div>
              );
            })}
          </div>
        </div>

        <div className="hidden md:flex absolute top-[140px] left-1/2 -translate-x-1/2 items-center bg-background/90 backdrop-blur-sm rounded-full overflow-hidden z-20 shadow-lg">
          <button onClick={scrollPrev} aria-label="prev" className="flex items-center justify-center w-12 h-12 text-white hover:bg-white/10">
            <ArrowLeft size={18} />
          </button>
          <div className="w-px h-6 bg-white/15" />
          <button onClick={scrollNext} aria-label="next" className="flex items-center justify-center w-12 h-12 text-white hover:bg-white/10">
            <ArrowRight size={18} />
          </button>
        </div>
      </div>

      <p className="relative z-10 text-center text-sm text-white/40 max-w-md mx-auto mt-6 leading-relaxed">
        {t("subtitle")}
      </p>
    </section>
  );
}