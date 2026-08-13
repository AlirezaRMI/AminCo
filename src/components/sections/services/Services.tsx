
"use client";

import { useCallback, useEffect, useState } from "react";
import useEmblaCarousel from "embla-carousel-react";
import { useTranslations } from "next-intl";
import { motion } from "framer-motion";
import PillowCard from "@/components/ui/PillowCard";

interface ServiceCard {
  title: string;
  text: string;
}

export default function Services() {
  const t = useTranslations("services");
  const cards = t.raw("cards") as ServiceCard[];

  const [emblaRef, emblaApi] = useEmblaCarousel({
    loop: true,
    direction: "rtl",
    align: "center",
    slidesToScroll: 1,
  });

  const [selectedIndex, setSelectedIndex] = useState(0);
  const [hoveredIndex, setHoveredIndex] = useState<number | null>(null);

  const onSelect = useCallback(() => {
    if (!emblaApi) return;
    setSelectedIndex(emblaApi.selectedScrollSnap());
  }, [emblaApi]);

  useEffect(() => {
    if (!emblaApi) return;
    emblaApi.on("select", onSelect);
    onSelect();
  }, [emblaApi, onSelect]);

  useEffect(() => {
    if (!emblaApi) return;
    const interval = setInterval(() => emblaApi.scrollNext(), 3500);
    return () => clearInterval(interval);
  }, [emblaApi]);

  return (
    <section className="w-full px-4 md:px-10 py-16 md:py-24 overflow-hidden">
    
      <div className="relative">
        <div className="overflow-hidden" ref={emblaRef}>
          <div className="flex">
            {cards.map((card, i) => {
              const isActive = hoveredIndex === i;
              return (
                <div
                  key={i}
                  className="flex-[0_0_85%] sm:flex-[0_0_60%] md:flex-[0_0_40%] lg:flex-[0_0_24%] px-2 md:px-3"
                >
                  <motion.div
                    initial={{ opacity: 0, y: 30, rotate: -3 }}
                    whileInView={{ opacity: 1, y: 0, rotate: 0 }}
                    viewport={{ once: true, amount: 0.3 }}
                    transition={{
                      duration: 0.7,
                      delay: i * 0.1,
                      ease: [0.16, 1, 0.3, 1],
                    }}
                    whileHover={{ scale: 1.05 }}
                    onHoverStart={() => setHoveredIndex(i)}
                    onHoverEnd={() => setHoveredIndex(null)}
                    onTouchStart={() => setHoveredIndex(i)}
                    onTouchEnd={() => setHoveredIndex(null)}
                    className="relative h-full min-h-[280px] cursor-pointer"
                  >
                    <PillowCard fill={isActive ? "url(#silverGradient)" : "#1f1f1f"} />

                    <svg width="0" height="0" className="absolute">
                      <defs>
                        <linearGradient id="silverGradient" x1="0%" y1="0%" x2="100%" y2="100%">
                          <stop offset="0%" stopColor="#f2f2f2" />
                          <stop offset="50%" stopColor="#d8d8d8" />
                          <stop offset="100%" stopColor="#f5f5f5" />
                        </linearGradient>
                      </defs>
                    </svg>

                    <div className="relative z-10 h-full flex flex-col items-center justify-center px-8 py-10 text-center">
                      <h3
                        className={`text-lg md:text-xl font-bold mb-3 transition-colors duration-500 ${
                          isActive ? "text-black" : "text-white"
                        }`}
                      >
                        {card.title}
                      </h3>
                 
                    </div>
                  </motion.div>
                </div>
              );
            })}
          </div>
        </div>

        <div className="flex items-center justify-center gap-2 mt-8">
          {cards.map((_, i) => (
            <button
              key={i}
              onClick={() => emblaApi?.scrollTo(i)}
              aria-label={`slide-${i}`}
              className={`h-1.5 rounded-full transition-all duration-300 ${
                i === selectedIndex ? "w-6 bg-accent" : "w-1.5 bg-white/20"
              }`}
            />
          ))}
        </div>
      </div>
    </section>
  );
}