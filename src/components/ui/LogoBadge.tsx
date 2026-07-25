
export default function LogoBadge() {
  return (
    <div className="relative w-24 h-24 md:w-28 md:h-28 rounded-full bg-background flex items-center justify-center shadow-lg">
      {/* حلقه داخلی با سایه inset برای حس فرورفتگی */}
      <div className="w-[88%] h-[88%] rounded-full bg-card-light shadow-[inset_0_4px_10px_rgba(0,0,0,0.7)] flex items-center justify-center">
        <div className="w-14 h-14 md:w-16 md:h-16 rounded-full border-2 border-accent flex items-center justify-center">
          <div className="w-3 h-3 rounded-full bg-accent" />
        </div>
      </div>
    </div>
  );
}