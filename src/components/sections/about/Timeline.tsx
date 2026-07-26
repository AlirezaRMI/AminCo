
import { useTranslations } from "next-intl";
import timeline1 from "@/../public/images/timeline1.png";
import timeline2 from "@/../public/images/timeline2.png";
import timeline3 from "@/../public/images/timeline3.png";
import timeline4 from "@/../public/images/timeline4.png";
import TimelineItem from "@/components/timeline/TimelineItem";
import TimelineDivider from "@/components/timeline/TimelineDivider";

interface TimelineItemData {
  title: string;
  description: string;
}

const images = [timeline1, timeline2, timeline3, timeline4];

export default function Timeline() {
  const t = useTranslations("timeline");
  const items = t.raw("items") as TimelineItemData[];

  return (
    <section className="relative px-4 md:px-10 py-12 md:py-20 max-w-6xl mx-auto">
      <TimelineDivider />

      <div className="flex flex-col gap-16 md:gap-24">
        {items.map((item, index) => (
          <TimelineItem
            key={index}
            item={{ ...item, image: images[index] }}
            reversed={index % 2 !== 0}
          />
        ))}
      </div>
    </section>
  );
}