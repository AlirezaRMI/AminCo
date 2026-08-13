"use client";

import { useState } from "react";
import { motion } from "framer-motion";
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";
import { ArrowUpLeft } from "lucide-react";
import { interactive } from "@/lib/utils";

export default function ConsultBanner() {
  const t = useTranslations("consultBanner");
  const [isPressed, setIsPressed] = useState(false);

  return (
    <section className="w-full px-4 md:px-10 py-4">
      <div className="relative max-w-5xl me-auto">
        {/* کارت نقره‌ای - از راست میاد تو */}
        <motion.div
          initial={{ opacity: 0, x: 60 }}
          whileInView={{ opacity: 1, x: 0 }}
          viewport={{ once: true, amount: 0.4 }}
          transition={{ duration: 0.7, ease: [0.16, 1, 0.3, 1] }}
          className="relative"
          style={{
            borderRadius: "150px 24px 24px 150px",
            background: "linear-gradient(90deg, #e8e8e8 0%, #b8b8b8 55%, #1a1a1a 100%)",
          }}
        >
          <svg
            className="absolute top-0 left-0 w-72 h-full opacity-30 pointer-events-none"
            viewBox="0 0 300 260"
            fill="none"
          >
            <path d="M -20 40 Q 60 -10, 140 40 T 300 40" stroke="#888" strokeWidth="1" />
            <path d="M -20 80 Q 60 30, 140 80 T 300 80" stroke="#888" strokeWidth="1" />
            <path d="M -20 120 Q 60 70, 140 120 T 300 120" stroke="#888" strokeWidth="1" />
            <path d="M -20 160 Q 60 110, 140 160 T 300 160" stroke="#888" strokeWidth="1" />
            <path d="M -20 200 Q 60 150, 140 200 T 300 200" stroke="#888" strokeWidth="1" />
          </svg>

          <div className="relative z-10 flex flex-col md:flex-row items-center justify-between gap-6 px-8 md:px-14 pt-10 md:pt-14 pb-20 md:pb-24">
            <div className="text-center md:text-right order-1">
              <h2 className="text-2xl md:text-3xl leading-relaxed text-black/70">
                {t("titleNormal")}
              </h2>
              <h2 className="text-2xl md:text-4xl font-black text-black -mt-1">
                {t("titleBold")}
              </h2>
            </div>

            <p className="text-sm md:text-base text-black/70 text-center md:text-right max-w-xs order-2">
              {t("subtitle")}
            </p>
          </div>
        </motion.div>

        {/* دکمه - از چپ میاد تو */}
        <motion.div
          initial={{ opacity: 0, x: -60 }}
          whileInView={{ opacity: 1, x: 0 }}
          viewport={{ once: true, amount: 0.4 }}
          transition={{ duration: 0.7, delay: 0.15, ease: [0.16, 1, 0.3, 1] }}
          className="absolute bottom-6 -left-4 md:-left-6 z-20"
        >
          <Link
            href="/contact"
            onMouseDown={() => setIsPressed(true)}
            onMouseUp={() => setIsPressed(false)}
            onMouseLeave={() => setIsPressed(false)}
            onTouchStart={() => setIsPressed(true)}
            onTouchEnd={() => setIsPressed(false)}
            className={`flex items-center gap-3 rounded-full pl-5 pr-2 py-2.5 transition-all duration-300 ${
              isPressed
                ? "bg-accent-dark scale-95 shadow-inner"
                : "bg-accent hover:bg-accent-dark hover:scale-[1.04] shadow-lg"
            } ${interactive}`}
          >
            <span
              className={`text-sm md:text-base transition-all duration-300 text-white ${
                isPressed ? "font-black" : "font-bold"
              }`}
            >
              {t("cta")}
            </span>
            <span
              className={`flex items-center justify-center w-9 h-9 rounded-full shrink-0 transition-all duration-300 ${
                isPressed ? "bg-black/50" : "bg-black/20"
              }`}
            >
              <ArrowUpLeft size={16} className="text-white" />
            </span>
          </Link>
        </motion.div>
      </div>
    </section>
  );
}