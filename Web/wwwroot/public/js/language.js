async function setLanguage(lang) {
  localStorage.setItem('lang', lang);
  try {
    const response = await fetch('/public/js/language/' + lang + '.json');
    if (!response.ok) throw new Error('HTTP ' + response.status);
    const translations = await response.json();
    document.querySelectorAll('[data-i18n]').forEach(el => {
      const key = el.getAttribute('data-i18n');
      if (translations[key]) {
        el.textContent = translations[key];
      }
    });
    document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
      const key = el.getAttribute('data-i18n-placeholder');
      if (translations[key]) {
        el.setAttribute('placeholder', translations[key]);
      }
    });
  } catch (err) {
    console.warn('Failed to load language file:', err);
  }
}