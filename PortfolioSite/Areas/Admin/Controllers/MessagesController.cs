using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class MessagesController : Controller
{
    private readonly ApplicationDbContext _db;
    public MessagesController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _db.ContactMessages.Where(m => !m.IsDeleted).OrderByDescending(m => m.SentAt).ToListAsync(ct));

    public async Task<IActionResult> Detail(int id, CancellationToken ct)
    {
        var msg = await _db.ContactMessages.FindAsync([id], ct);
        if (msg == null) return NotFound();
        if (!msg.IsRead) { msg.IsRead = true; await _db.SaveChangesAsync(ct); }
        return View(msg);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var msg = await _db.ContactMessages.FindAsync([id], ct);
        if (msg != null) { msg.IsDeleted = true; await _db.SaveChangesAsync(ct); }
        TempData["Success"] = "Mesaj silindi!";
        return RedirectToAction(nameof(Index));
    }
}
