
"use client";

import { useEffect, useRef, useState } from "react";
import { motion, useInView } from "framer-motion";

interface Props {
  value: string; // مثلا "۱۹۲۴۶۲" یا "192462"
  duration?: number;
}

export default function CountUpNumber({ value, duration = 1.8 }: Props) {
  const ref = useRef(null);
  const isInView = useInView(ref, { once: true, amount: 0.5 });
  const [display, setDisplay] = useState("0");

  // تبدیل اعداد فارسی به انگلیسی برای محاسبه، بعد برگردوندن به فرمت اصلی
  const persianDigits = "۰۱۲۳۴۵۶۷۸۹";
  const isPersian = /[۰-۹]/.test(value);
  const numericValue = parseInt(
    value.replace(/[۰-۹]/g, (d) => String(persianDigits.indexOf(d))),
    10
  );

  const toPersian = (num: number) =>
    num
      .toString()
      .split("")
      .map((d) => persianDigits[parseInt(d, 10)] ?? d)
      .join("");

  useEffect(() => {
    if (!isInView || isNaN(numericValue)) return;

    let startTime: number | null = null;
    const animate = (timestamp: number) => {
      if (startTime === null) startTime = timestamp;
      const progress = Math.min((timestamp - startTime) / (duration * 1000), 1);
      const eased = 1 - Math.pow(1 - progress, 3); // easeOutCubic
      const current = Math.floor(eased * numericValue);
      setDisplay(isPersian ? toPersian(current) : current.toString());

      if (progress < 1) {
        requestAnimationFrame(animate);
      } else {
        setDisplay(value);
      }
    };
    requestAnimationFrame(animate);
  }, [isInView, numericValue, duration, isPersian, value]);

  return (
    <motion.span
      ref={ref}
      initial={{ opacity: 0, y: 10 }}
      animate={isInView ? { opacity: 1, y: 0 } : {}}
      transition={{ duration: 0.5 }}
    >
      {display}
    </motion.span>
  );
}