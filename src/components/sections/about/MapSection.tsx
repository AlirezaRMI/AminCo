
import { useTranslations } from "next-intl";
import { ExternalLink } from "lucide-react";
import { interactive } from "@/lib/utils";

const embedUrl =
  "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3239.9!2d51.389!3d35.699!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zتهران!5e0!3m2!1sfa!2sir!4v1234567890";
const googleMapsUrl = "https://maps.google.com/?q=تهران";

export default function MapSection() {
  const t = useTranslations("mapSection");

  return (
    <section className="px-4 md:px-10 py-12 md:py-16 max-w-6xl mx-auto">
      <h2 className="text-xl md:text-2xl font-bold text-white text-center mb-8">
        {t("title")}
      </h2>

      <div className="relative w-full h-[320px] md:h-[420px] rounded-2xl overflow-hidden">
        <iframe
          src={embedUrl}
          width="100%"
          height="100%"
          style={{ border: 0 }}
          allowFullScreen
          loading="lazy"
          referrerPolicy="no-referrer-when-downgrade"
          title={t("mapTitle")}
        />

        <a
          href={googleMapsUrl}
          target="_blank"
          rel="noopener noreferrer"
          className={`absolute bottom-4 left-1/2 -translate-x-1/2 flex items-center gap-2 bg-card/90 backdrop-blur-sm text-white text-sm px-5 py-2.5 rounded-full border border-white/10 hover:bg-card ${interactive}`}
        >
          <ExternalLink size={16} />
          {t("viewButton")}
        </a>
      </div>
    </section>
  );
}