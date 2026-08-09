// ====================================================
// PORTFOLIO SITE — MAIN JAVASCRIPT, THEMES & TRANSLATE
// ====================================================

document.addEventListener('DOMContentLoaded', () => {

  // ===== GOOGLE TRANSLATE TR / EN SWITCHER =====
  const langTrBtn = document.getElementById('lang-tr-btn');
  const langEnBtn = document.getElementById('lang-en-btn');

  function setTranslateCookie(val) {
    const domain = window.location.hostname;
    const expires = new Date(Date.now() + 30 * 86400000).toUTCString();
    
    // Clear all existing cookie variations
    document.cookie = "googtrans=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = `googtrans=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; domain=${domain};`;
    document.cookie = `googtrans=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; domain=.${domain};`;

    if (val) {
      document.cookie = `googtrans=${val}; expires=${expires}; path=/;`;
      if (domain && domain !== 'localhost') {
        document.cookie = `googtrans=${val}; expires=${expires}; path=/; domain=${domain};`;
        document.cookie = `googtrans=${val}; expires=${expires}; path=/; domain=.${domain};`;
      }
    }
  }

  // Check language state
  const currentLang = localStorage.getItem('portfolio-lang') || 'tr';

  if (currentLang === 'en') {
    if (langEnBtn && langTrBtn) {
      langEnBtn.classList.add('active');
      langTrBtn.classList.remove('active');
    }
  } else {
    // FORCE TR: Ensure no /tr/en cookie remains
    setTranslateCookie('/tr/tr');
    if (langTrBtn && langEnBtn) {
      langTrBtn.classList.add('active');
      langEnBtn.classList.remove('active');
    }
  }

  if (langTrBtn && langEnBtn) {
    // EN CLICK: Trigger English Translation
    langEnBtn.addEventListener('click', (e) => {
      e.preventDefault();
      if (langEnBtn.classList.contains('active')) return;
      
      localStorage.setItem('portfolio-lang', 'en');
      setTranslateCookie('/tr/en');
      
      const select = document.querySelector('.goog-te-combo');
      if (select) {
        select.value = 'en';
        select.dispatchEvent(new Event('change'));
      }
      window.location.reload();
    });

    // TR CLICK: Restore 100% Original Turkish State
    langTrBtn.addEventListener('click', (e) => {
      e.preventDefault();
      if (langTrBtn.classList.contains('active')) return;
      
      localStorage.setItem('portfolio-lang', 'tr');
      setTranslateCookie('/tr/tr');
      
      const select = document.querySelector('.goog-te-combo');
      if (select) {
        select.value = 'tr';
        select.dispatchEvent(new Event('change'));
      }
      window.location.reload();
    });
  }

  // ===== LIGHT / DARK THEME SWITCHER =====
  const themeBtn = document.getElementById('theme-toggle-btn');
  const savedTheme = localStorage.getItem('portfolio-theme') || 'dark';
  
  function applyTheme(theme) {
    if (theme === 'light') {
      document.documentElement.setAttribute('data-theme', 'light');
      if (themeBtn) {
        themeBtn.setAttribute('title', 'Karanlık Temaya Geç');
        themeBtn.innerHTML = `<span>☀️ Aydınlık</span>`;
      }
    } else {
      document.documentElement.removeAttribute('data-theme');
      if (themeBtn) {
        themeBtn.setAttribute('title', 'Aydınlık Temaya Geç');
        themeBtn.innerHTML = `<span>🌙 Karanlık</span>`;
      }
    }
  }

  // Initial apply
  applyTheme(savedTheme);

  if (themeBtn) {
    themeBtn.addEventListener('click', () => {
      const current = document.documentElement.getAttribute('data-theme');
      const nextTheme = current === 'light' ? 'dark' : 'light';
      localStorage.setItem('portfolio-theme', nextTheme);
      applyTheme(nextTheme);
    });
  }

  // ===== NAVBAR SCROLL =====
  const navbar = document.querySelector('.navbar');
  if (navbar) {
    window.addEventListener('scroll', () => {
      navbar.classList.toggle('scrolled', window.scrollY > 50);
    }, { passive: true });
  }

  // ===== HAMBURGER MENU =====
  const hamburger = document.querySelector('.hamburger');
  const navLinks = document.querySelector('.nav-links');
  if (hamburger && navLinks) {
    hamburger.addEventListener('click', () => {
      navLinks.classList.toggle('open');
      hamburger.setAttribute('aria-expanded', navLinks.classList.contains('open'));
    });
    // Close on link click
    navLinks.querySelectorAll('a').forEach(link => {
      link.addEventListener('click', () => navLinks.classList.remove('open'));
    });
  }

  // ===== ACTIVE NAV SECTION =====
  const sections = document.querySelectorAll('section[id]');
  const navItems = document.querySelectorAll('.nav-links a[href^="#"]');
  if (sections.length && navItems.length) {
    const observer = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          const id = e.target.id;
          navItems.forEach(link => {
            link.classList.toggle('active', link.getAttribute('href') === `#${id}`);
          });
        }
      });
    }, { rootMargin: '-40% 0px -55% 0px' });
    sections.forEach(s => observer.observe(s));
  }

  // ===== AUTO-DISMISS ALERTS =====
  document.querySelectorAll('.alert-auto-dismiss').forEach(alert => {
    setTimeout(() => {
      alert.style.opacity = '0';
      alert.style.transition = 'opacity 0.5s ease';
      setTimeout(() => alert.remove(), 500);
    }, 4000);
  });

  // ===== TSPARTICLES BACKGROUND =====
  if (typeof tsParticles !== 'undefined') {
    tsParticles.load("tsparticles", {
      background: { color: { value: "transparent" } },
      fpsLimit: 60,
      interactivity: {
        detectsOn: "window",
        events: {
          onHover: { enable: true, mode: "grab" },
          resize: true
        },
        modes: {
          grab: { distance: 180, links: { opacity: 0.35, color: "#397BFF" } }
        }
      },
      particles: {
        color: { value: "#397BFF" },
        links: {
          color: "#397BFF",
          distance: 160,
          enable: true,
          opacity: 0.15,
          width: 1
        },
        move: {
          enable: true,
          speed: 0.8,
          direction: "none",
          random: false,
          straight: false,
          outModes: { default: "bounce" }
        },
        number: {
          density: { enable: true, area: 800 },
          value: 30
        },
        opacity: { value: 0.4 },
        shape: { type: "circle" },
        size: { value: { min: 1, max: 3 } }
      },
      detectRetina: true
    });
  }

});
