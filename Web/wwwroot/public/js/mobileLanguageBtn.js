let isEN = true;

function toggleLang() {
  isEN = !isEN;
  const btn = document.getElementById('langBtn_mobile');
  const enOpt = document.getElementById('enOpt_mobile');
  const faOpt = document.getElementById('faOpt_mobile');

  if (isEN) {
    btn.classList.remove('rtl');
    enOpt.className = 'lang-option active';
    faOpt.className = 'lang-option inactive';
    setLanguage('en');
  } else {
    btn.classList.add('rtl');
    enOpt.className = 'lang-option inactive';
    faOpt.className = 'lang-option active';
    setLanguage('fa');
  }
}

const savedLang = localStorage.getItem('lang') || 'en';
if (savedLang === 'fa') {
  isEN = false;
  document.getElementById('langBtn_mobile').classList.add('rtl');
  document.getElementById('enOpt_mobile').className = 'lang-option inactive';
  document.getElementById('faOpt_mobile').className = 'lang-option active';
}
setLanguage(savedLang);

