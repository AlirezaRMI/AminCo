
"use client";

import { useState } from "react";
import { useTranslations } from "next-intl";
import DotPatternDown from "@/components/ui/DotPatternDown";
import { interactive } from "@/lib/utils";

export default function ConsultationForm() {
  const t = useTranslations("consultation");
  const [agreed, setAgreed] = useState(false);

  const fields = [
    { id: "name", type: "text", ...t.raw("fields.name") },
    { id: "phone", type: "tel", ...t.raw("fields.phone") },
    { id: "message", type: "textarea", ...t.raw("fields.message") },
  ];

  return (
    <section className="relative px-4 md:px-10 py-16 md:py-24 overflow-hidden">
      <div
        className="absolute inset-0 opacity-10 pointer-events-none"
        style={{
          background:
            "radial-gradient(ellipse 60% 50% at 30% 40%, #e0435c 0%, transparent 70%)",
        }}
      />
      <div
        className="absolute -top-20 -left-20 w-96 h-96 opacity-5 pointer-events-none blur-3xl"
        style={{
          background: "#e0435c",
          clipPath: "polygon(50% 0%, 100% 100%, 0% 100%)",
        }}
      />

      <div className="relative max-w-2xl mx-auto text-center">
        <div className="flex justify-center mb-4">
          <DotPatternDown />
        </div>

        <h2 className="text-xl md:text-3xl mb-10">
          <span className="text-white/60">{t("titleBefore")}</span>{" "}
          <span className="relative inline-block text-white font-bold">
            {t("titleEmphasis")}
            <span
              className="absolute left-0 right-0 -bottom-1 h-2 blur-sm rounded-full"
              style={{ background: "rgba(224, 67, 92, 0.5)" }}
            />
          </span>
        </h2>

        <form className="flex flex-col gap-5 bg-card rounded-2xl p-6 md:p-10 text-right">
          {fields.map((field) =>
            field.type === "textarea" ? (
              <div key={field.id} className="flex flex-col gap-2">
                <label htmlFor={field.id} className="text-sm text-white/60">
                  {field.label}
                </label>
                <textarea
                  id={field.id}
                  placeholder={field.placeholder}
                  rows={4}
                  className="bg-card-light rounded-lg px-4 py-3 text-sm text-white placeholder:text-white/30 outline-none focus:ring-1 focus:ring-accent resize-none"
                />
              </div>
            ) : (
              <div key={field.id} className="flex flex-col gap-2">
                <label htmlFor={field.id} className="text-sm text-white/60">
                  {field.label}
                </label>
                <input
                  id={field.id}
                  type={field.type}
                  placeholder={field.placeholder}
                  className="bg-card-light rounded-lg px-4 py-3 text-sm text-white placeholder:text-white/30 outline-none focus:ring-1 focus:ring-accent"
                />
              </div>
            )
          )}

          <label className="flex items-center gap-2 text-xs text-white/50">
            <input
              type="checkbox"
              checked={agreed}
              onChange={(e) => setAgreed(e.target.checked)}
              className="accent-accent shrink-0"
            />
            <span className="flex-1 text-right">{t("privacyText")}</span>
          </label>

          <button
            type="submit"
            className={`mt-2 w-full bg-accent text-white font-bold py-3 rounded-full hover:bg-accent-dark transition-colors ${interactive}`}
          >
            {t("submit")}
          </button>
        </form>
      </div>
    </section>
  );
}