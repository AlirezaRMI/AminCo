
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { interactive } from "@/lib/utils";

interface Props {
  icon: IconDefinition;
  href: string;
  label: string;
}

export default function SocialIcon({ icon, href, label }: Props) {
  return (
    <a
      href={href}
      aria-label={label}
      className={`flex items-center justify-center w-10 h-10 rounded-full bg-card-light hover:bg-accent transition-colors duration-300 ${interactive}`}
    >
      <FontAwesomeIcon icon={icon} className="text-sm text-white/70" />
    </a>
  );
}