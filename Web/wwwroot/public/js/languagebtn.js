let isEN = true;

function toggleLang() {
    isEN = !isEN;

    const btns = ['langBtn', 'langBtn_mobile'];
    for (let i = 0; i < btns.length; i++) {
        const btn = document.getElementById(btns[i]);
        if (btn) {
            if (isEN) btn.classList.remove('rtl');
            else btn.classList.add('rtl');
        }
    }
    
    const pairs = [
        { en: 'enOpt', fa: 'faOpt' },
        { en: 'enOpt_mobile', fa: 'faOpt_mobile' }
    ];
    for (let i = 0; i < pairs.length; i++) {
        const enEl = document.getElementById(pairs[i].en);
        const faEl = document.getElementById(pairs[i].fa);
        if (enEl && faEl) {
            if (isEN) {
                enEl.className = 'lang-option active';
                faEl.className = 'lang-option inactive';
            } else {
                enEl.className = 'lang-option inactive';
                faEl.className = 'lang-option active';
            }
        }
    }

    setLanguage(isEN ? 'en' : 'fa');
}

var savedLang = localStorage.getItem('lang') || 'en';
if (savedLang === 'fa') {
    isEN = false;
    toggleLang(); 
} else {
    setLanguage('en');
}