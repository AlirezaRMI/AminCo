import Navbar from "@/components/layout/Navbar";
import Footer from "@/components/layout/Footer";
import BlogHero from "@/components/sections/blog/BlogHero";
import LatestArticles from "@/components/sections/blog/LatestArticles";

export default function BlogPage() {
  return (
    <main className="min-h-screen">
      <Navbar overlay />
      <BlogHero />
      <LatestArticles />
      <Footer />
    </main>
  );
}