import Navbar from "@/components/layout/Navbar";
import Footer from "@/components/layout/Footer";
import AboutHero from "@/components/sections/about/Hero";
import IntroText from "@/components/sections/about/IntroText";
import VideoSection from "@/components/sections/about/VideoSection";
import Timeline from "@/components/sections/about/Timeline";
import ContactCard from "@/components/sections/about/ContactCard";
import MapSection from "@/components/sections/about/MapSection";
import ConsultationForm from "@/components/sections/home/ConsultationForm";

export default function AboutPage() {
  return (
    <main className="min-h-screen">
      <Navbar overlay />
      <AboutHero />
      <IntroText />
      <VideoSection />
      <Timeline />
      <ContactCard />
      <MapSection />
      <ConsultationForm />
      <Footer />
    </main>
  );
}