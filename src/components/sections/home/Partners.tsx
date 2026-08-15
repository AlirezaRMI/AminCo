"use client";

import { useState } from "react";
import { useTranslations } from "next-intl";
import { motion } from "framer-motion";
import DotPattern from "@/components/ui/DotPattern";

const logos = Array.from({ length: 8 }).map((_, i) => i);

function LogoPill() {
  return (
    <div className="flex items-center justify-center px-10 py-6 shrink-0">
      <span className="text-xl md:text-2xl font-black tracking-wide text-white/70">
        AZARIN
      </span>
    </div>
  );
}

export default function Partners() {
  const t = useTranslations("partners");
  const [isPaused, setIsPaused] = useState(false);

  return (
    <section className="w-full px-4 md:px-10 py-4">
      <div className="relative overflow-hidden pt-10 md:pt-14 pb-4">
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true, amount: 0.5 }}
          transition={{ duration: 0.6 }}
          className="relative z-10 text-center px-4 mb-4"
        >
          <div className="flex justify-center mb-2">
            <DotPattern direction="down" />
          </div>

          <h2 className="text-xl md:text-3xl leading-relaxed">
            <span
              style={{
                background: "linear-gradient(90deg, #9a9a9a 0%, #d8d8d8 100%)",
                WebkitBackgroundClip: "text",
                backgroundClip: "text",
                color: "transparent",
              }}
            >
              {t("titleNormal")}
            </span>{" "}
            <span className="relative inline-block font-bold text-white">
              {t("titleBold")}
              <span
                className="absolute left-0 right-0 -bottom-1 h-2 blur-sm rounded-full"
                style={{ background: "rgba(224, 67, 92, 0.5)" }}
              />
            </span>
          </h2>
          <p className="text-xs md:text-sm text-white/40 max-w-xl mx-auto mt-3 leading-relaxed">
            {t("subtitle")}
          </p>
        </motion.div>

        {/* یه نوار تک، بدون پس‌زمینه */}
        <div
          className="relative flex overflow-hidden"
          onMouseEnter={() => setIsPaused(true)}
          onMouseLeave={() => setIsPaused(false)}
          onTouchStart={() => setIsPaused(true)}
          onTouchEnd={() => setIsPaused(false)}
        >
          <div
            className="flex shrink-0"
            style={{
              animation: "marqueeRight 25s linear infinite",
              animationPlayState: isPaused ? "paused" : "running",
            }}
          >
            {[...logos, ...logos].map((_, i) => (
              <LogoPill key={i} />
            ))}
          </div>
        </div>
      </div>
    </section>
  );
}