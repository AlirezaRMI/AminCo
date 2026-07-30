"use client";

import { useState } from "react";

import Navbar from "@/components/layout/Navbar";
import Footer from "@/components/layout/Footer";
import ProjectsHero from "@/components/sections/projects/Hero";
import ProjectGallery from "@/components/sections/projects/ProjectGallery";
import PaginationDots from "@/components/sections/projects/PaginationDots";

import p1 from "@/../public/images/project11.png";
import p2 from "@/../public/images/project22.png";
import p3 from "@/../public/images/project33.png";
import p4 from "@/../public/images/project44.png";
import p5 from "@/../public/images/project55.png";

const galleryImages = [p1, p2, p3, p4, p5];

export default function ServicesPage() {
  const [currentPage, setCurrentPage] = useState(0);

  return (
     <main className="min-h-screen">
      <Navbar overlay />
      <ProjectsHero />

      {/* سه گالری پروژه پشت‌سرهم */}
      <ProjectGallery images={galleryImages} />
      <ProjectGallery images={galleryImages} />
      <ProjectGallery images={galleryImages} />

      <PaginationDots total={4} current={currentPage} onChange={setCurrentPage} />

      <Footer />
    </main>
  );
}