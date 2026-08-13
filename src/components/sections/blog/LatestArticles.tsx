"use client";

import { useState } from "react";
import Image from "next/image";
import { motion } from "framer-motion";
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";
import { Clock, User, ArrowUpLeft, ChevronDown, ChevronLeft, ChevronRight } from "lucide-react";
import newsImg1 from "@/../public/images/news1.png";
import newsImg2 from "@/../public/images/news2.png";
import newsImg3 from "@/../public/images/news3.png";
import cardArrowBg from "@/../public/images/cardArrowBg.png";
import { interactive } from "@/lib/utils";

interface ArticleItem {
  title: string;
  text: string;
  author: string;
  time: string;
}

const images = [newsImg1, newsImg2, newsImg3, newsImg1, newsImg2, newsImg3];

export default function LatestArticles() {
  const t = useTranslations("latestArticles");
  const items = t.raw("items") as ArticleItem[];
  const sortOptions = t.raw("sortOptions") as string[];

  const [hoveredIndex, setHoveredIndex] = useState<number | null>(null);
  const [sortOpen, setSortOpen] = useState(false);
  const [selectedSort, setSelectedSort] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const totalPages = 4;

  return (
    <section className="w-full px-4 md:px-10 py-16 md:py-24">
      <div className="flex flex-col md:flex-row items-center justify-between gap-4 mb-10">
        <h2 className="text-xl md:text-2xl font-bold text-white order-1 md:order-2">
          {t("title")}
        </h2>

        <div className="relative order-2 md:order-1">
          <button
            onClick={() => setSortOpen(!sortOpen)}
            className={`flex items-center gap-2 text-sm text-white/60 ${interactive}`}
          >
            {t("sortLabel")} <span className="text-white">{sortOptions[selectedSort]}</span>
            <ChevronDown size={14} className={`transition-transform ${sortOpen ? "rotate-180" : ""}`} />
          </button>

          {sortOpen && (
            <ul className="absolute top-full right-0 mt-2 min-w-[160px] rounded-md bg-card border border-border-subtle py-2 shadow-lg z-20">
              {sortOptions.map((option, i) => (
                <li key={i}>
                  <button
                    onClick={() => {
                      setSelectedSort(i);
                      setSortOpen(false);
                    }}
                    className={`block w-full text-right px-4 py-2 text-sm hover:bg-card-light ${
                      i === selectedSort ? "text-accent" : "text-white/70"
                    } ${interactive}`}
                  >
                    {option}
                  </button>
                </li>
              ))}
            </ul>
          )}
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {items.map((item, i) => {
          const isHovered = hoveredIndex === i;
          return (
            <motion.div
              key={i}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true, amount: 0.2 }}
              transition={{ duration: 0.6, delay: (i % 3) * 0.1 }}
              className="flex flex-col overflow-hidden border border-white/10 rounded-2xl"
            >
              <div
                className="relative w-full h-56 overflow-hidden cursor-pointer"
                onMouseEnter={() => setHoveredIndex(i)}
                onMouseLeave={() => setHoveredIndex(null)}
                onTouchStart={() => setHoveredIndex(i)}
                onTouchEnd={() => setHoveredIndex(null)}
              >
                <Image
                  src={images[i]}
                  alt={item.title}
                  fill
                  className="object-cover transition-transform duration-700"
                  style={{ transform: isHovered ? "scale(1.08)" : "scale(1)" }}
                />
                <div
                  className="absolute inset-0 transition-opacity duration-300"
                  style={{ background: "rgba(224, 67, 92, 0.35)", opacity: isHovered ? 1 : 0 }}
                />
                <div
                  className="absolute inset-0 flex items-center justify-center transition-all duration-300"
                  style={{ opacity: isHovered ? 1 : 0, transform: isHovered ? "scale(1)" : "scale(0.85)" }}
                >
                  <Link
                    href="/blog"
                    className={`flex items-center gap-2 bg-white text-black text-sm font-bold px-5 py-3 rounded-full ${interactive}`}
                  >
                    <ArrowUpLeft size={16} />
                    {t("readMore")}
                  </Link>
                </div>
              </div>

              <div className="relative bg-card px-6 pt-6 pb-6 flex-1 flex flex-col rounded-b-2xl overflow-hidden">
                <Image src={cardArrowBg} alt="" fill className="object-cover pointer-events-none" />

                <h3 className="relative z-10 text-white text-base md:text-lg font-bold text-center mb-3">
                  {item.title}
                </h3>
                <p className="relative z-10 text-white/50 text-sm text-center leading-relaxed mb-6">
                  {item.text}
                </p>
                <div className="relative z-10 mt-auto flex items-center justify-between text-xs text-white/40">
                  <div className="flex items-center gap-1.5">
                    <User size={14} />
                    <span>{item.author}</span>
                  </div>
                  <div className="flex items-center gap-1.5">
                    <Clock size={14} />
                    <span>{item.time}</span>
                  </div>
                </div>
              </div>
            </motion.div>
          );
        })}
      </div>

      {/* Pagination */}
      <div className="flex items-center justify-center gap-2 mt-12">
        <button
          onClick={() => setCurrentPage((p) => Math.max(0, p - 1))}
          aria-label="prev"
          className={`flex items-center justify-center w-9 h-9 text-white/60 hover:text-white ${interactive}`}
        >
          <ChevronRight size={18} />
        </button>

        {Array.from({ length: totalPages }).map((_, i) => (
          <button
            key={i}
            onClick={() => setCurrentPage(i)}
            className={`w-9 h-9 rounded-full text-sm transition-colors ${
              i === currentPage ? "bg-white text-black font-bold" : "text-white/60 hover:text-white"
            } ${interactive}`}
          >
            {i + 1}
          </button>
        ))}

        <button
          onClick={() => setCurrentPage((p) => Math.min(totalPages - 1, p + 1))}
          aria-label="next"
          className={`flex items-center justify-center w-9 h-9 text-white/60 hover:text-white ${interactive}`}
        >
          <ChevronLeft size={18} />
        </button>
      </div>
    </section>
  );
}