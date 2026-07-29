import Navbar from "@/components/layout/Navbar";
import Footer from "@/components/layout/Footer"
import ServicesHero from "@/components/sections/services/Hero";
import Services from "@/components/sections/services/Services";
import ServicesTimeline from "@/components/sections/services/ServicesTimeline";

export default function ServicesPage() {
  return (
    <main className="min-h-screen">
      <Navbar overlay />
      <ServicesHero />
      <Services />
      <ServicesTimeline />
      <Footer />
    </main>
  );
}