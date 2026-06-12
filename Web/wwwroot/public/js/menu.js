document.addEventListener('DOMContentLoaded', () => {


  const btn = document.getElementById('dropdown-btn');
  const menu = document.getElementById('dropdown-menu');
  const arrow = document.getElementById('dropdown-arrow');

  if (btn && menu && arrow) {
    btn.addEventListener('click', (e) => {
      e.preventDefault();

      const isOpen = menu.classList.contains('open');

      if (isOpen) {
        menu.classList.remove('open');
        arrow.classList.remove('rotate-180');
      } else {
        menu.classList.add('open');
        arrow.classList.add('rotate-180');
      }
    });

    document.addEventListener('click', (e) => {
      const parent = document.getElementById('dropdown-parent');

      if (parent && !parent.contains(e.target)) {
        menu.classList.remove('open');
        arrow.classList.remove('rotate-180');
      }
    });
  }


  const burgerBtn = document.getElementById('burger-btn');
  const mobileMenu = document.getElementById('mobile-menu');
  const burgerLines = document.querySelectorAll('.burger-line');

  let menuOpen = false;

  if (burgerBtn && mobileMenu) {
    burgerBtn.addEventListener('click', () => {
      menuOpen = !menuOpen;

      if (menuOpen) {
        mobileMenu.classList.remove('-translate-y-full');

        burgerLines[0].style.transform = 'translateY(7px) rotate(45deg)';
        burgerLines[1].style.opacity = '0';
        burgerLines[2].style.transform = 'translateY(-7px) rotate(-45deg)';
      } else {
        mobileMenu.classList.add('-translate-y-full');

        burgerLines[0].style.transform = '';
        burgerLines[1].style.opacity = '';
        burgerLines[2].style.transform = '';
      }
    });
  }

  const mobileDropBtn = document.getElementById('mobile-dropdown-btn');
  const mobileDropMenu = document.getElementById('mobile-dropdown');
  const mobileArrow = document.getElementById('mobile-arrow');

  if (mobileDropBtn && mobileDropMenu && mobileArrow) {
    mobileDropBtn.addEventListener('click', (e) => {
      e.preventDefault();

      mobileDropMenu.classList.toggle('open');
      mobileArrow.classList.toggle('rotate-180');

      console.log('dropdown clicked');
      console.log(mobileDropMenu.className);
    });
  }

});
