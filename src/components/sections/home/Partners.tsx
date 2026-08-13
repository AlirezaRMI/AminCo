
"use client";

import { useTranslations } from "next-intl";
import { motion } from "framer-motion";
import DotPattern from "@/components/ui/DotPattern";

const logos = Array.from({ length: 6 }).map((_, i) => i);

function LogoPill({ highlighted = false }: { highlighted?: boolean }) {
  return (
    <div
      className={`flex items-center justify-center px-10 py-6 shrink-0 ${
        highlighted ? "text-black" : "text-white/70"
      }`}
      style={{
        borderRadius: "0 40px 40px 0",
        background: highlighted
          ? "linear-gradient(135deg, #f2f2f2 0%, #b8b8b8 50%, #f5f5f5 100%)"
          : "rgba(255,255,255,0.04)",
      }}
    >
      <span className="text-xl md:text-2xl font-black tracking-wide">AZARIN</span>
    </div>
  );
}

export default function Partners() {
  const t = useTranslations("partners");

  return (
    <section className="w-full px-4 md:px-10 py-4">
      <div className="relative rounded-3xl bg-card overflow-hidden pt-10 md:pt-14 pb-4">
        {/* پترن دکوری بالا-چپ */}
      <div className="relative z-10 flex justify-center mb-4">
  <DotPattern direction="down" />
</div>
        {/* تیتر و پاراگراف */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true, amount: 0.5 }}
          transition={{ duration: 0.6 }}
          className="relative z-10 text-center px-4 mb-12"
        >
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

        {/* ردیف اول - حرکت به راست */}
        <div className="relative flex overflow-hidden mb-3">
          <div className="flex animate-marquee-right shrink-0">
            {[...logos, ...logos].map((_, i) => (
              <LogoPill key={i} highlighted={i === 1} />
            ))}
          </div>
        </div>

        {/* ردیف دوم - حرکت به چپ */}
        <div className="relative flex overflow-hidden">
          <div className="flex animate-marquee-left shrink-0">
            {[...logos, ...logos].map((_, i) => (
              <LogoPill key={i} />
            ))}
          </div>
        </div>
      </div>
    </section>
  );
}