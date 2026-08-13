
import { useTranslations } from "next-intl";

export default function IntroText() {
  const t = useTranslations("aboutIntro");

  return (
    <section className="py-12 md:py-16 px-4">
      <p className="max-w-3xl mx-auto text-center text-lg md:text-2xl leading-relaxed text-white/60">
        <span className="text-white font-bold">{t("emphasis1")}</span>
        {t("middle")}
        <span className="text-white font-bold">{t("emphasis2")}</span>
      </p>
    </section>
  );
}