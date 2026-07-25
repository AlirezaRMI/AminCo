import { NextIntlClientProvider } from "next-intl";
import { getMessages } from "next-intl/server";
import { routing } from "@/i18n/routing";
import { notFound } from "next/navigation";
import localFont from "next/font/local";
import { Inter } from "next/font/google";
import "../globals.css";

const yekanBakh = localFont({
  src: [
    {
      path: "../../../public/fonts/BYekan.ttf",
      weight: "400",
      style: "normal",
    },
    {
      path: "../../../public/fonts/BYekanBold.ttf",
      weight: "700",
      style: "normal",
    },
  ],
  variable: "--font-yekan-bakh",
});

const inter = Inter({
  subsets: ["latin"],
  variable: "--font-inter",
});

export default async function LocaleLayout({
  children,
  params,
}: {
  children: React.ReactNode;
  params: Promise<{ locale: string }>;
}) {
  const { locale } = await params;

  if (!routing.locales.includes(locale as any)) {
    notFound();
  }

  const messages = await getMessages();
 const dir = "rtl"
  const fontVariable = locale === "fa" ? "font-yekan-bakh" : "font-inter";

  return (
    <html lang={locale} dir={dir}>
      <body
        className={`${yekanBakh.variable} ${inter.variable} ${fontVariable} bg-background text-white antialiased`}
      >
        <NextIntlClientProvider messages={messages}>
          {children}
        </NextIntlClientProvider>
      </body>
    </html>
  );
}