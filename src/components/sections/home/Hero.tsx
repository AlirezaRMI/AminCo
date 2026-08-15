"use client";

import Image from "next/image";
import { useTranslations } from "next-intl";
import { Link } from "@/i18n/navigation";

import { ArrowLeft } from "lucide-react";
import heroKitchen from "@/../public/images/heroKitchen.png";
import productCard1 from "@/../public/images/productCard1.png";
import productCard2 from "@/../public/images/productCard2.png";
import { interactive } from "@/lib/utils";
import { ArrowUpLeft } from "lucide-react";
import { useLocale } from "next-intl";
import CountUpNumber from "@/components/ui/CountUpNumber";
import { motion } from "framer-motion";

interface StatItem {
  value: string;
  label: string;
}

export default function Hero() {
  const t = useTranslations("hero");
   const locale = useLocale();
  const isRtl = locale === "fa";
const stats: StatItem[] = [
  t.raw("stats.stat1"),
  t.raw("stats.stat2"),
  t.raw("stats.stat3"),
];

const displayStats: StatItem[] = isRtl ? stats : [...stats].reverse();  

  return (
    <section className="w-full px-4 md:px-10 pt-4">
      <div className="flex flex-col lg:flex-row items-start gap-4 lg:gap-6">
        {/* ستون عکس - Navbar overlay دقیقاً داخل همین کانتینر */}
        <motion.div
  initial={{ opacity: 0, x: 40, scale: 1.05 }}
  animate={{ opacity: 1, x: 0, scale: 1 }}
  transition={{ duration: 0.9, ease: [0.16, 1, 0.3, 1] }}
  className="relative h-[560px] lg:h-[640px] w-full lg:w-[75%] rounded-2xl lg:rounded-3xl overflow-hidden shrink-0"
>
          <Image
            src={heroKitchen}
            alt="آشپزخانه صنعتی"
            fill
            className="object-cover object-[70%_0%]"
            priority
          />

       

<div className="relative z-10 h-full flex flex-col justify-between pl-4 md:pl-10 pr-0 md:pr-2 py-15 mt-6 md:mt-10">
 <div className={`flex flex-col gap-6 max-w-2xl ${isRtl ? "ml-auto text-right" : "mr-auto text-left"}`}>
<h1 className="animate-fade-in-up text-2xl md:text-4xl lg:text-5xl font-medium leading-relaxed">
  {t("titleNormal")}{" "}
  <span className="font-black">{t("titleBold")}</span>
</h1>
  <p className={`animate-fade-in-up-delay-1 text-sm md:text-base text-white/60 leading-relaxed max-w-lg ${isRtl ? "ml-auto" : "mr-auto"}`}>
    {t("subtitle")}
  </p>
  <Link
    href="/services"
    className={`animate-fade-in-up-delay-1 bg-accent text-white text-sm font-bold pr-6 pl-2 py-2 rounded-full w-fit hover:bg-accent-dark transition-colors flex items-center gap-3 ${isRtl ? "ml-auto" : "mr-auto"} ${interactive}`}
  >
    {t("cta")}
    <span
      className="flex items-center justify-center w-9 h-9 rounded-full shrink-0"
      style={{
        background: "linear-gradient(135deg, #e8e8e8 0%, #a8a8a8 50%, #d4d4d4 100%)",
      }}
    >
      <ArrowUpLeft size={16} className="text-black" />
    </span>
  </Link>
</div>

<div
  className={`animate-fade-in-up-delay-2 flex items-center gap-6 md:gap-10 w-full max-w-lg ${
    isRtl ? "mr-auto" : "ml-auto"
  }`}
>
{stats.map((stat: StatItem, i: number) => (
  <div
    key={i}
    className={`text-center flex-1 ${
      i > 0 ? "border-r border-white/30 pr-6 md:pr-10" : ""
    }`}
  >
    <p className="text-xl md:text-2xl font-bold text-white">
      <CountUpNumber value={stat.value} duration={1.8} />
    </p>
    <p className="text-xs text-white/50 mt-1">{stat.label}</p>
  </div>
))}
</div>
</div>
</motion.div>

{/* دسکتاپ بزرگ - فقط از lg به بالا نمایش داده بشه (نه md) */}
<div className="animate-slide-in-left-delay-1 hidden lg:flex lg:flex-col gap-3 w-full lg:w-[20%] h-[560px] lg:mt-24">
  <ProductCard
    image={productCard1}
    href="/services"
    title={t("productCards.card1.title")}
    subtitle={t("productCards.card1.subtitle")}
    bgColor="#7A1F35"
  />
 <ProductCard
  image={productCard2}
  href="/services"
  title={t("productCards.card2.title")}
  subtitle={t("productCards.card2.subtitle")}
  hasOwnBackground
  iconBg="#000000"
  iconColor="text-white"
/>
</div>

{/* موبایل + تبلت - افقی، تا قبل از lg */}
<div className="flex lg:hidden flex-row gap-4 w-full">
<ProductCard
  image={productCard1}
  href="/services"
  title={t("productCards.card1.title")}
  subtitle={t("productCards.card1.subtitle")}
  bgColor="#7A1F35"
/>
<ProductCard
  image={productCard2}
  href="/services"
  title={t("productCards.card2.title")}
  subtitle={t("productCards.card2.subtitle")}
  hasOwnBackground
  iconBg="#000000"
  iconColor="text-white"
/>
</div>
      </div>
    </section>
  );
}

function ProductCard({
  image,
  href,
  title,
  subtitle,
  bgColor,
  hasOwnBackground = false,
  iconBg = "linear-gradient(135deg, #e8e8e8 0%, #a8a8a8 50%, #d4d4d4 100%)",
  iconColor = "text-black",
}: {
  image: any;
  href: string;
  title: string;
  subtitle: string;
  bgColor?: string;
  hasOwnBackground?: boolean;
  iconBg?: string;
  iconColor?: string;
}) {
  return (
    <Link
      href={href}
      className={`group relative w-full h-32 md:h-auto md:flex-1 rounded-xl md:rounded-2xl overflow-hidden flex items-end p-4 ${interactive}`}
      style={!hasOwnBackground ? { backgroundColor: bgColor } : undefined}
    >
      <div className={hasOwnBackground ? "absolute inset-0" : "absolute inset-0 flex items-center justify-center p-4"}>
        <motion.div
          initial={{ scale: 1.25, opacity: 0 }}
          animate={{ scale: 1, opacity: 1 }}
          transition={{ duration: 1, ease: [0.16, 1, 0.3, 1] }}
          className="relative w-full h-full"
        >
          <Image
            src={image}
            alt={title}
            fill
            className={`transition-transform duration-700 group-hover:scale-110 ${
              hasOwnBackground ? "object-cover" : "object-contain"
            }`}
          />
        </motion.div>
      </div>

      {hasOwnBackground && (
        <div className="absolute inset-0 bg-gradient-to-t from-black/60 via-black/10 to-transparent" />
      )}

      <div className="relative z-10 flex items-end justify-between w-full">
        <div className="text-right">
          <p className="text-xs md:text-sm text-white/80">{title}</p>
          <p className="text-sm md:text-base font-bold text-white">{subtitle}</p>
        </div>
        <span
          className="flex items-center justify-center w-8 h-8 rounded-full shrink-0 transition-transform duration-300 group-hover:scale-110"
          style={{ background: iconBg }}
        >
          <ArrowUpLeft size={14} className={iconColor} />
        </span>
      </div>
    </Link>
  );
}