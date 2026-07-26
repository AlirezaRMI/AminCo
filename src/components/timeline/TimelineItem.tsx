
import Image from "next/image";
import DotPattern from "@/components/ui/DotPattern";

interface TimelineItemData {
  title: string;
  description: string;
  image: any;
}

interface Props {
  item: TimelineItemData;
  reversed: boolean;
}

export default function TimelineItem({ item, reversed }: Props) {
  return (
    <div
      className={`flex flex-col md:flex-row ${
        reversed ? "md:flex-row-reverse" : ""
      } items-center gap-6 md:gap-12`}
    >
      <div className="relative w-full md:w-1/2 h-[220px] md:h-[280px] rounded-xl overflow-hidden shrink-0">
        <Image
          src={item.image}
          alt={item.title}
          fill
          sizes="(max-width: 768px) 100vw, 50vw"
          className="object-cover"
        />
      </div>

      <div
        className={`w-full md:w-1/2 flex flex-col ${
          reversed
            ? "md:items-start md:text-left"
            : "md:items-end md:text-right"
        } items-center text-center gap-3`}
      >
        <h3 className="text-xl md:text-2xl font-bold text-white">
          {item.title}
        </h3>
        <p className="text-sm md:text-base text-white/60 leading-relaxed max-w-sm">
          {item.description}
        </p>
        <div className="mt-2">
          <DotPattern />
        </div>
      </div>
    </div>
  );
}