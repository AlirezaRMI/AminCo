function animateCounter(el, duration, easingFn) {
  const target = +el.dataset.target;
  const start  = performance.now();

  function tick(now) {
    const elapsed  = now - start;
    const progress = Math.min(elapsed / duration, 1);
    const eased    = easingFn(progress);         
    const current  = Math.round(eased * target);

    el.textContent = current.toLocaleString('en'); 
    

    if (progress < 1) requestAnimationFrame(tick);
  }

  requestAnimationFrame(tick);
}


const easings = {
  easeOut:   t => 1 - (1 - t) ** 3, 
  linear:    t => t,
  easeIn:    t => t * t,
  easeInOut: t => t < 0.5 ? 2*t*t : 1-( -2 * t+2) **2/2,
};


document.querySelectorAll('[data-target]').forEach(el => {
  animateCounter(el, 3000, easings.easeOut); 
});