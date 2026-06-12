window.addEventListener('load', () => {

  document.querySelectorAll('header .reveal').forEach(el => {
    el.classList.add('visible');
  });

  
  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add('visible');
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 0.15 });

  document.querySelectorAll('.reveal').forEach(el => {

    if (!el.closest('header')) {
      observer.observe(el);
    }
  });
});