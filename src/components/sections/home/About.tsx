"use client";

import { useState } from "react";
import Image from "next/image";
import { useTranslations } from "next-intl";
import { motion } from "framer-motion";
import { Play } from "lucide-react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faYoutube,
  faFacebook,
  faLinkedin,
  faInstagram,
} from "@fortawesome/free-brands-svg-icons";
import videoThumbnail from "@/../public/images/aboutVideoThumbnail.png";
import { interactive } from "@/lib/utils";

const socials = [
  { icon: faYoutube, href: "#", label: "YouTube" },
  { icon: faFacebook, href: "#", label: "Facebook" },
  { icon: faLinkedin, href: "#", label: "LinkedIn" },
  { icon: faInstagram, href: "#", label: "Instagram" },
];

export default function About() {
  const t = useTranslations("about");
  const [isPlaying, setIsPlaying] = useState(false);
  const [hoveredSocial, setHoveredSocial] = useState<number | null>(null);

  return (
    <section className="relative w-full px-4 md:px-10 py-16 md:py-24 overflow-hidden">
      <div className="absolute top-0 left-0 right-0 flex whitespace-nowrap select-none pointer-events-none overflow-hidden">
        <span className="text-[80px] md:text-[140px] font-black text-white/[0.04] tracking-wider">
          AMIN CO AMIN CO AMIN CO
        </span>
      </div>

      <div className="relative z-10 flex flex-col md:flex-row items-center gap-8 md:gap-12 mt-16 md:mt-24">
        {/* متن سمت راست - همه چیز با ml-auto می‌چسبه راست */}
        <div className="w-full md:w-[35%] flex flex-col gap-4 order-1">
          <motion.h2
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true, amount: 0.5 }}
            transition={{ duration: 0.6 }}
            className="text-2xl md:text-3xl leading-relaxed text-right ml-auto w-fit"
          >
            <span
              style={{
                background: "linear-gradient(90deg, #9a9a9a 0%, #d8d8d8 100%)",
                WebkitBackgroundClip: "text",
                backgroundClip: "text",
                color: "transparent",
              }}
            >
              {t("titleNormal")}
            </span>
            <br />
            <span
              className="font-bold"
              style={{
                background: "linear-gradient(90deg, #b8b8b8 0%, #ffffff 100%)",
                WebkitBackgroundClip: "text",
                backgroundClip: "text",
                color: "transparent",
              }}
            >
              {t("titleBold")}
            </span>
          </motion.h2>

          <motion.p
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true, amount: 0.5 }}
            transition={{ duration: 0.6, delay: 0.1 }}
            className="text-sm md:text-base text-white/50 leading-relaxed max-w-xs text-right ml-auto"
          >
            {t("subtitle")}
          </motion.p>

          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true, amount: 0.5 }}
            transition={{ duration: 0.6, delay: 0.2 }}
            className="flex items-center gap-3 mt-2 ml-auto"
          >
            {socials.map((social, i) => {
              const isHovered = hoveredSocial === i;
              return (
                  <a
                  key={i}
                  href={social.href}
                  aria-label={social.label}
                  onMouseEnter={() => setHoveredSocial(i)}
                  onMouseLeave={() => setHoveredSocial(null)}
                  onTouchStart={() => setHoveredSocial(i)}
                  onTouchEnd={() => setHoveredSocial(null)}
                  className={`flex items-center justify-center w-10 h-10 rounded-full transition-colors duration-300 ${
                    isHovered ? "bg-accent" : "bg-card-light"
                  } ${interactive}`}
                >
                  <FontAwesomeIcon
                    icon={social.icon}
                    className={`text-sm transition-colors duration-300 ${
                      isHovered ? "text-white" : "text-white/70"
                    }`}
                  />
                </a>
              );
            })}
          </motion.div>
        </div>

        {/* ویدیو سمت چپ */}
        <motion.div
          initial={{ opacity: 0, x: -40 }}
          whileInView={{ opacity: 1, x: 0 }}
          viewport={{ once: true, amount: 0.3 }}
          transition={{ duration: 0.7, ease: [0.16, 1, 0.3, 1] }}
          className="relative w-full md:w-[65%] h-[280px] md:h-[380px] overflow-hidden shrink-0 order-2"
          style={{ borderRadius: "200px 0 0 200px" }}
        >
          {!isPlaying ? (
            <>
              <Image src={videoThumbnail} alt="درباره ما" fill className="object-cover" />
              <div className="absolute inset-0 bg-black/20" />
              <button
                onClick={() => setIsPlaying(true)}
                aria-label="پخش ویدیو"
                className={`absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 flex items-center justify-center w-16 h-16 md:w-20 md:h-20 rounded-full bg-accent hover:scale-105 transition-transform ${interactive}`}
              >
                <Play size={26} className="text-white fill-white mr-[-2px]" />
              </button>
            </>
          ) : (
            <video controls autoPlay className="w-full h-full object-cover">
              <source src="/videos/about.mp4" type="video/mp4" />
            </video>
          )}
        </motion.div>
      </div>
    </section>
  );
}