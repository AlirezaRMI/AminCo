
"use client";

import { ChevronLeft, ChevronRight } from "lucide-react";
import { interactive } from "@/lib/utils";

interface Props {
  total: number;
  current: number;
  onChange: (index: number) => void;
}

export default function PaginationDots({ total, current, onChange }: Props) {
  return (
    <div className="flex items-center justify-center gap-2 py-13">
      <button
        onClick={() => onChange(Math.max(0, current - 1))}
        className={`flex items-center justify-center w-8 h-8 text-white/60 hover:text-white ${interactive}`}
        aria-label="prev"
      >
        <ChevronRight size={18} />
      </button>

      {Array.from({ length: total }).map((_, i) => (
        <button
          key={i}
          onClick={() => onChange(i)}
          className={`w-8 h-8 rounded-full text-sm transition-colors ${
            i === current ? "bg-white text-black font-bold" : "text-white/60 hover:text-white"
          } ${interactive}`}
        >
          {i + 1}
        </button>
      ))}

      <button
        onClick={() => onChange(Math.min(total - 1, current + 1))}
        className={`flex items-center justify-center w-8 h-8 text-white/60 hover:text-white ${interactive}`}
        aria-label="next"
      >
        <ChevronLeft size={18} />
      </button>
    </div>
  );
}