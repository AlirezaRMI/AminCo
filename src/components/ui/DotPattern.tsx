import { ArrowLeft } from "lucide-react";

type Direction = "left" | "right" | "up" | "down";

const rotationMap: Record<Direction, number> = {
  left: 45,
  right: -135,
  up: 135,
  down: -45,
};

const arrows = [
  { size: 18, color: "text-accent", offset1: 0, offset2: 20, delay: 0 },
  { size: 12, color: "text-white/25", offset1: 14, offset2: 0, delay: 0.2 },
  { size: 10, color: "text-white/15", offset1: 22, offset2: 32, delay: 0.4 },
  { size: 14, color: "text-white/30", offset1: 4, offset2: 40, delay: 0.1 },
];

export default function DotPattern({
  direction = "left",
}: {
  direction?: Direction;
}) {
  const isVertical = direction === "up" || direction === "down";
  const rotation = rotationMap[direction];
  const floatAnimation = direction === "down" ? "arrowFloatDown" : "arrowFloat";

  return (
    <div
      className="relative"
      style={
        isVertical
          ? { width: "36px", height: "64px" }
          : { width: "64px", height: "36px" }
      }
    >
      {arrows.map((arrow, i) => (
        <ArrowLeft
          key={i}
          size={arrow.size}
          strokeWidth={2.5}
          className={`absolute ${arrow.color}`}
          style={{
            top: isVertical ? arrow.offset2 : arrow.offset1,
            left: isVertical ? arrow.offset1 : arrow.offset2,
            transform: `rotate(${rotation}deg)`,
            animation: `${floatAnimation} 2s ease-in-out infinite`,
            animationDelay: `${arrow.delay}s`,
          }}
        />
      ))}
    </div>
  );
}