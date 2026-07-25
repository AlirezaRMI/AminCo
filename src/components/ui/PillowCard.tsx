export default function PillowCard({
  fill,
  className = "",
}: {
  fill: string;
  className?: string;
}) {
  return (
    <svg
      viewBox="0 0 300 340"
      preserveAspectRatio="none"
      className={`absolute inset-0 w-full h-full ${className}`}
    >
      <path
        d="M 20,50
           C 90,10 210,10 280,50
           L 280,290
           C 210,330 90,330 20,290
           Z"
        fill={fill}
        style={{ transition: "fill 0.5s ease" }}
      />
    </svg>
  );
}