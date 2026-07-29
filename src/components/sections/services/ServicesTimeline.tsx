
import { useTranslations } from "next-intl";
import ServicesTimelineItem from "./ServicesTimelineItem";

import img1a from "@/../public/images/servicesTimeline1a.png";
import img1b from "@/../public/images/servicesTimeline1b.png";
import img2a from "@/../public/images/servicesTimeline2a.png";
import img2b from "@/../public/images/servicesTimeline2b.png";
import img3a from "@/../public/images/servicesTimeline3a.png";
import img3b from "@/../public/images/servicesTimeline3b.png";
import img4a from "@/../public/images/servicesTimeline4a.png";
import img4b from "@/../public/images/servicesTimeline4b.png";

interface TimelineItemData {
  description: string;
}

const imagesList: [any, any][] = [
  [img1a, img1b],
  [img2a, img2b],
  [img3a, img3b],
  [img4a, img4b],
];

export default function ServicesTimeline() {
  const t = useTranslations("servicesTimeline");
  const items = t.raw("items") as TimelineItemData[];

  return (
    <section className="px-4 md:px-10 py-12 md:py-20 max-w-6xl mx-auto">
      <div className="flex flex-col gap-16 md:gap-24">
        {items.map((item, index) => (
          <ServicesTimelineItem
            key={index}
            images={imagesList[index]}
            description={item.description}
            imagesOnLeft={index % 2 === 0}
          />
        ))}
        
      </div>
    </section>
  );
}