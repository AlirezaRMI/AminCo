

import { ArrowDown } from "lucide-react";

interface ArrowItem {
  size: number;
  color: string;
  top: number;
  left: number;
  delay: number;
}

const arrows: ArrowItem[] = [
  { size: 18, color: "text-accent", top: 0, left: 20, delay: 0 },
  { size: 12, color: "text-white/25", top: 14, left: 0, delay: 0.2 },
  { size: 10, color: "text-white/15", top: 22, left: 32, delay: 0.4 },
  { size: 14, color: "text-white/30", top: 4, left: 40, delay: 0.1 },
];

export default function DotPatternDown() {
  return (
    <div className="relative w-16 h-11">
      {arrows.map((arrow, i) => (
        <ArrowDown
          key={i}
          size={arrow.size}
          strokeWidth={2.5}
          className={`absolute animate-arrow-float-down ${arrow.color}`}
          style={{
            top: arrow.top,
            left: arrow.left,
            animationDelay: `${arrow.delay}s`,
          }}
        />
      ))}
    </div>
  );
}