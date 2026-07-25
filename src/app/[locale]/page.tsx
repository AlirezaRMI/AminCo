import Navbar from "@/components/layout/Navbar";
import Footer from "@/components/layout/Footer";
import Hero from "@/components/sections/home/Hero";
import BottomCards from "@/components/sections/home/BottomCards";
import Services from "@/components/sections/home/Services";
import Projects from "@/components/sections/home/Projects";
import About from "@/components/sections/home/About";
import Partners from "@/components/sections/home/Partners";
import ConsultBanner from "@/components/sections/home/ConsultBanner";
import News from "@/components/sections/home/News";
import ConsultationForm from "@/components/sections/home/ConsultationForm";

export default function Home() {
  return (
    <main className="min-h-screen">
      <Navbar overlay />
      <Hero />
      <BottomCards />
      <Services />
      <Projects />
      <About />
      <Partners />
      <ConsultBanner />
      <News />
      <ConsultationForm />
      <Footer />
    </main>
  );
}