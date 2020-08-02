using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicWorld.Data;
using MusicWorld.Data.Models;
using MusicWorld.Services;
using MusicWorld.Services.Contracts;

namespace MusicWorld.Controllers
{
    public class CatalogsController : Controller
    {
        private readonly MusicWorldContext _context; 
        public CatalogsController(MusicWorldContext context)
        {
            _context = context;  
        }

        // GET: Catalogs
        public async Task<IActionResult> Index()
        {
         
            if (AccountService.UserName=="admin")
            { 
                var musicWorldContext = _context.Catalogs.Include(c => c.User);
                return View(await musicWorldContext.ToListAsync());
            }
            else
            {
                var usrCatalog = _context.Catalogs;
                return View(await  usrCatalog.ToListAsync());
            }
        }

        // GET: Catalogs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.User)
                .Include(x=>x.Songs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: Catalogs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");

            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Name");

            ViewBag.Songs = _context.Songs.ToList();

            return View();
        }

        // POST: Catalogs/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]//[Bind("Id,Name,Description,UserId")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UserId,SongId")] Catalog catalog,string[] SongId)
        {
            if (ModelState.IsValid)
            {  
                var u = AccountService.UsrId;
                var usr = _context.Users.FirstOrDefault(x => x.Id == u);
                catalog.UserId = usr.Id;  
                catalog.User = usr;
                foreach (var s in SongId)
                {
                    var song = _context.Songs.FirstOrDefault(x => x.Name == s); 
                    catalog.Songs.Add(song);
                }                 
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", catalog.UserId);
            ViewData["SongId"] = new SelectList(_context.Users, "Id", "Name",catalog.Songs);

            
            return View(catalog);
        }

        //[HttpGet]
        //public IActionResult SaveSong()
        //{
        //    Song song = new Song(); 

        //}

        [HttpPost]
        public IActionResult SaveSong(string songId)
        {
            if (ModelState.IsValid)
            { 
                var s = _context.Songs.FirstOrDefault(x => x.Id == songId);

                var usr = _context.Catalogs.FirstOrDefault(x => x.UserId == AccountService.UsrId);

                if (usr != null && s != null)
                {
                    var u = usr.Songs.FirstOrDefault(x => x.Name == s.Name);
                    if (u==null)
                    { 
                        return Json(new { status = "error", message = "This song is already added in playlist" });
                    }
                    else
                    {
                        usr.Songs.Add(s); 
                        _context.SaveChanges();
                        return this.View();
                    }
                }
                else { return NotFound(); }
            }
            return NotFound();
        }

        // GET: Catalogs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", catalog.UserId);
            ViewBag.Songs = _context.Songs.ToList();
            return View(catalog);
        }

        // POST: Catalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,UserId")] Catalog catalog, string[] SongId)
        {
            if (id != catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var u = AccountService.UsrId;
                    var usr = _context.Users.FirstOrDefault(x => x.Id == u);
                    catalog.UserId = usr.Id;
                    catalog.User = usr;
                    foreach (var s in SongId)
                    {
                        var song = _context.Songs.FirstOrDefault(x => x.Name == s);
                        catalog.Songs.Add(song);
                    }
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", catalog.UserId);
            return View(catalog);
        }

        // GET: Catalogs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);
            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(string id)
        {
            return _context.Catalogs.Any(e => e.Id == id);
        }
    }
}
