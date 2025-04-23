using not_defteri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace not_defteri.Controllers
{
    public class NotsController : Controller
    {
        private readonly NotDefteriDbContext _context;
        public NotsController(NotDefteriDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Nots.ToListAsync());
        }

        public async Task<IActionResult> Detaylar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var not = await _context.Nots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (not == null)
            {
                return NotFound();
            }

            return View(not);

        }

        public IActionResult Olustur()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Olustur([Bind("Name,Content")] Not not)
        { 
            if(ModelState.IsValid)
                {
                    _context.Add(not);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            return View(not);

        }

        //get
        public async Task<IActionResult> Duzenle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }   

            var not = await _context.Nots.FindAsync(id);

            if (not == null)
            {
                return NotFound();
            }

            return View(not);

        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Duzenle(int id,[Bind("Id,Name,Content")] Not not)
        {
            if (id != not.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(not);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotExists(not.Id))
                    {
                        return NotFound();
                    }

                    else
                    {
                        throw;
                    }


                }

                return RedirectToAction(nameof(Index));
            }

            return View(not);
                    
        }

            // GET: /Notlar/Sil/5
            public async Task<IActionResult> Sil(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var not = await _context.Nots
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (not == null)
                {
                    return NotFound();
                }

                return View(not);
            }

            // POST: /Notlar/Sil/5
            [HttpPost, ActionName("Sil")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> SilConfirmed(int id)
            {
                var not = await _context.Nots.FindAsync(id);
                if (not != null)
                {
                    _context.Nots.Remove(not);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }


            private bool NotExists(int id)
            {
                return _context.Nots.Any(x => x.Id == id);
            }



        
    }
}
