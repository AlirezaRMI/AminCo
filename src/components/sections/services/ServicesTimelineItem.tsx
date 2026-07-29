import Image from "next/image";
import DotPatternDown from "@/components/ui/DotPatternDown";
interface Props {
  images: [any, any];
  description: string;
  imagesOnLeft: boolean;
}

export default function ServicesTimelineItem({ images, description, imagesOnLeft }: Props) {
  return (
    <div
      className={`flex flex-col md:flex-row items-center gap-6 md:gap-10 ${
        imagesOnLeft ? "" : "md:flex-row-reverse"
      }`}
    >
      {/* دو عکس کنار هم، با آفست عمودی متناوب */}
      <div className="w-full md:w-1/2 flex items-start gap-3">
        <div className="relative w-1/2 aspect-[3/4] rounded-xl overflow-hidden">
          <Image src={images[0]} alt="" fill className="object-cover" />
        </div>
        <div
          className={`relative w-1/2 aspect-[3/4] rounded-xl overflow-hidden ${
            imagesOnLeft ? "mt-8 md:mt-12" : "-mt-8 md:-mt-12"
          }`}
        >
          <Image src={images[1]} alt="" fill className="object-cover" />
        </div>
      </div>

      {/* متن */}
      <div
        className={`w-full md:w-1/2 flex flex-col ${
          imagesOnLeft ? "items-center md:items-end text-center md:text-right" : "items-center md:items-start text-center md:text-left"
        }`}
      >
        <p className="text-sm md:text-base text-white/50 leading-relaxed max-w-sm">
          {description}
        </p>
      </div>
      <DotPatternDown />
    </div>
  );
}