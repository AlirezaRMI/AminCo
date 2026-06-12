document.addEventListener('DOMContentLoaded', () => {
  document.querySelectorAll('[data-tilt]').forEach(card => {
    card.addEventListener('mousemove', (e) => {
      const rect = card.getBoundingClientRect();
      const x = (e.clientX - rect.left) / rect.width  - 0.5;
      const y = (e.clientY - rect.top)  / rect.height - 0.5;
      const strength = card.dataset.tiltStrength ?? 20;

      card.style.transform = `perspective(600px) rotateY(${x * strength}deg) rotateX(${-y * strength}deg) scale(1.02)`;
    });

    card.addEventListener('mouseleave', () => {
      card.style.transform = 'perspective(600px) rotateY(0) rotateX(0) scale(1)';
    });
  });
});