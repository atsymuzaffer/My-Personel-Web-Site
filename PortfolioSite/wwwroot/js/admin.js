// ====================================================
// ADMIN PANEL — JAVASCRIPT
// ====================================================

document.addEventListener('DOMContentLoaded', () => {

  // ===== SIDEBAR TOGGLE =====
  const hamburger = document.querySelector('.admin-hamburger');
  const sidebar = document.querySelector('.admin-sidebar');
  const overlay = document.getElementById('sidebar-overlay');

  if (hamburger && sidebar) {
    hamburger.addEventListener('click', () => {
      sidebar.classList.toggle('open');
      if (overlay) overlay.classList.toggle('active');
    });
  }

  if (overlay) {
    overlay.addEventListener('click', () => {
      sidebar.classList.remove('open');
      overlay.classList.remove('active');
    });
  }

  // ===== ACTIVE NAV ITEM =====
  const currentPath = window.location.pathname.toLowerCase();
  document.querySelectorAll('.nav-item').forEach(item => {
    const href = item.getAttribute('href');
    if (href && currentPath.includes(href.toLowerCase().split('/').pop())) {
      item.classList.add('active');
    }
  });

  // ===== DELETE CONFIRM MODAL =====
  document.querySelectorAll('[data-confirm-delete]').forEach(btn => {
    btn.addEventListener('click', (e) => {
      const msg = btn.dataset.confirmDelete || 'Bu kaydı silmek istediğinizden emin misiniz?';
      if (!confirm(msg)) e.preventDefault();
    });
  });

  // ===== FILE UPLOAD PREVIEW =====
  document.querySelectorAll('[data-preview-target]').forEach(input => {
    input.addEventListener('change', () => {
      const targetId = input.dataset.previewTarget;
      const preview = document.getElementById(targetId);
      if (preview && input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = (e) => {
          preview.src = e.target.result;
          preview.style.display = 'block';
        };
        reader.readAsDataURL(input.files[0]);
      }
    });
  });

  // ===== AUTO DISMISS ALERTS =====
  document.querySelectorAll('.alert').forEach(alert => {
    setTimeout(() => {
      alert.style.transition = 'opacity 0.5s ease';
      alert.style.opacity = '0';
      setTimeout(() => alert.remove(), 500);
    }, 5000);
  });

});
