
import { useTranslations } from "next-intl";
import { Phone, Mail, MapPin } from "lucide-react";
import {
  faYoutube,
  faFacebook,
  faLinkedin,
  faInstagram,
} from "@fortawesome/free-brands-svg-icons";
import SocialIcon from "@/components/ui/SocialIcon";

const socials = [
  { icon: faYoutube, href: "#", label: "YouTube" },
  { icon: faFacebook, href: "#", label: "Facebook" },
  { icon: faLinkedin, href: "#", label: "LinkedIn" },
  { icon: faInstagram, href: "#", label: "Instagram" },
];

export default function ContactCard() {
  const t = useTranslations("contactCard");
  const phones = t.raw("phones") as string[];

  return (
    <section className="px-4 md:px-10 py-12 md:py-20 max-w-6xl mx-auto">
      <div className="flex flex-col md:flex-row items-center gap-10 md:gap-16 bg-card rounded-2xl p-8 md:p-12">
        {/* اطلاعات تماس */}
        <div className="w-full md:w-1/2 flex flex-col gap-6 text-right">
          <h2 className="text-2xl md:text-3xl">
            <span className="text-white/50">{t("titleBefore")}</span>{" "}
            <span className="text-white font-bold">{t("titleEmphasis")}</span>
          </h2>

          <div className="flex items-start gap-3">
            <Phone size={20} className="text-accent mt-1 shrink-0" />
            <div>
              <p className="text-xs text-white/40 mb-1">{t("phoneLabel")}</p>
              <p className="text-sm text-white/80">{phones.join(" - ")}</p>
            </div>
          </div>

          <div className="flex items-start gap-3">
            <Mail size={20} className="text-accent mt-1 shrink-0" />
            <div>
              <p className="text-xs text-white/40 mb-1">{t("emailLabel")}</p>
              <p className="text-sm text-white/80">{t("email")}</p>
            </div>
          </div>

          <div className="flex items-start gap-3">
            <MapPin size={20} className="text-accent mt-1 shrink-0" />
            <div>
              <p className="text-xs text-white/40 mb-1">{t("addressLabel")}</p>
              <p className="text-sm text-white/80">{t("address")}</p>
            </div>
          </div>

          <div className="mt-2">
            <p className="text-sm text-white/50 mb-3">{t("socialText")}</p>
            <div className="flex flex-wrap gap-3">
              {socials.map((social, i) => (
                <SocialIcon key={i} icon={social.icon} href={social.href} label={social.label} />
              ))}
            </div>
          </div>
        </div>

        {/* لوگو */}
        <div className="w-full md:w-1/2 flex items-center justify-center">
          <div className="relative w-full max-w-sm aspect-[4/3] rounded-xl bg-card-light overflow-visible flex items-center justify-center">
            <span className="text-2xl font-bold tracking-widest text-white/15">
              AMIN.CO
            </span>
            <div className="absolute -bottom-5 -right-5 w-20 h-20 rounded-full bg-card border-4 border-card flex items-center justify-center shadow-lg">
              <div className="w-14 h-14 rounded-full border-2 border-accent flex items-center justify-center">
                <div className="w-3 h-3 rounded-full bg-accent" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}