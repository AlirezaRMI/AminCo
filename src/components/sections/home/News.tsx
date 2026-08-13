"use client";

import { useState } from "react";
import Image from "next/image";
import { motion } from "framer-motion";
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";
import { Clock, User, ArrowUpLeft } from "lucide-react";
import newsImg1 from "@/../public/images/news1.png";
import newsImg2 from "@/../public/images/news2.png";
import newsImg3 from "@/../public/images/news3.png";
import cardArrowBg from "@/../public/images/cardArrowBg.png";
import { interactive } from "@/lib/utils";

interface NewsItem {
  title: string;
  text: string;
  author: string;
  time: string;
}

const images = [newsImg1, newsImg2, newsImg3];

export default function News() {
  const t = useTranslations("news");
  const items = t.raw("items") as NewsItem[];
  const [activeIndex, setActiveIndex] = useState<number | null>(null);

  return (
    <section className="w-full px-4 md:px-10 py-16 md:py-24">
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true, amount: 0.5 }}
        transition={{ duration: 0.6 }}
        className="text-center mb-12"
      >
        <h2 className="text-2xl md:text-4xl font-bold text-white mb-3">
          <span className="relative inline-block">
            {t("title")}
            <span
              className="absolute left-0 right-0 -bottom-1 h-2 blur-sm rounded-full"
              style={{ background: "rgba(224, 67, 92, 0.5)" }}
            />
          </span>
        </h2>
        <p className="text-sm md:text-base text-white/50 max-w-lg mx-auto leading-relaxed">
          {t("subtitle")}
        </p>
      </motion.div>

      {/* گرید کارت‌ها */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {items.map((item, i) => {
          const isActive = activeIndex === i;
          return (
            <motion.div
              key={i}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true, amount: 0.3 }}
              transition={{ duration: 0.6, delay: i * 0.1 }}
              onTouchStart={() => setActiveIndex(i)}
              onTouchEnd={() => setActiveIndex(null)}
              className="group flex flex-col overflow-hidden border border-white/10 rounded-2xl cursor-pointer"
            >
              {/* عکس - هاور/تاچ زرشکی */}
              <div className="relative w-full h-56 overflow-hidden">
                <Image
                  src={images[i]}
                  alt={item.title}
                  fill
                  className="object-cover transition-transform duration-700 group-hover:scale-110"
                />
                <div
                  className={`absolute inset-0 transition-opacity duration-300 ${
                    isActive ? "opacity-100" : "opacity-0 group-hover:opacity-100"
                  }`}
                  style={{ background: "rgba(224, 67, 92, 0.35)" }}
                />
                <div
                  className={`absolute inset-0 flex items-center justify-center transition-all duration-300 ${
                    isActive
                      ? "opacity-100 scale-100"
                      : "opacity-0 scale-90 group-hover:opacity-100 group-hover:scale-100"
                  }`}
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

              {/* بخش تیره - با بک‌گراند پیکانی */}
              <div className="relative bg-card px-6 pt-6 pb-6 flex-1 flex flex-col rounded-b-2xl overflow-hidden">
                <Image
                  src={cardArrowBg}
                  alt=""
                  fill
                  className="object-cover pointer-events-none"
                />

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

      {/* لینک مشاهده همه - بیرون از grid، زیر همه کارت‌ها */}
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true, amount: 0.5 }}
        transition={{ duration: 0.6, delay: 0.3 }}
        className="flex justify-center mt-10"
      >
        <Link
          href="/blog"
          className={`flex items-center gap-2 text-white text-sm border border-white/20 rounded-full px-6 py-3 hover:bg-white/5 transition-colors ${interactive}`}
        >
          {t("viewAll")}
          <ArrowUpLeft size={16} />
        </Link>
      </motion.div>
    </section>
  );
}