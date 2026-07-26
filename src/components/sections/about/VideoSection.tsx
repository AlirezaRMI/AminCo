
"use client";

import { useState } from "react";
import Image from "next/image";
import thumbnail from "@/../public/images/thumbnail.png";
import { Play } from "lucide-react";
import { interactive } from "@/lib/utils";

const videoUrl = "/videos/about.mp4";

export default function VideoSection() {
  const [isPlaying, setIsPlaying] = useState(false);

  return (
    <section className="px-4 md:px-10 pb-12 md:pb-16">
     <div className="relative max-w-4xl mx-auto aspect-video rounded-xl overflow-hidden">
        {!isPlaying ? (
          <>
            <Image
              src={thumbnail}
              alt="video preview"
              fill
              className="object-cover"
            />
            <div className="absolute inset-0 bg-black/20" />
            <button
              onClick={() => setIsPlaying(true)}
              aria-label="play video"
              className={`absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 flex items-center justify-center w-16 h-16 md:w-20 md:h-20 rounded-full bg-accent hover:scale-105 transition-transform ${interactive}`}
            >
              <Play size={28} className="text-white fill-white mr-[-2px]" />
            </button>
          </>
        ) : (
          <video
            src={videoUrl}
            controls
            autoPlay
            className="w-full h-full object-cover"
          />
        )}
      </div>
    </section>
  );
}