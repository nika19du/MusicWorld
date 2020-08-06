using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicWorld.Data;
using MusicWorld.Data.Models;

namespace MusicWorld.Controllers
{
    public class SongsController : Controller
    {
        private readonly MusicWorldContext _context;

        public SongsController(MusicWorldContext context)
        {
            _context = context;
        }

        [HttpGet] 
        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var musicWorldContext = _context.Songs.Include(s => s.Album).Include(s => s.Artist);
            return View(await musicWorldContext.ToListAsync());
        } 
        [HttpPost]
        //POST: Songs
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var song = _context.Songs.Include(x=>x.Album).Include(x=>x.Artist).ToList();
                song = song.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
               
                   return View(song.ToList()); 
            }
            return NotFound();
        }

        [HttpGet] 
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        public JsonResult GetAlbumList(string ArtistId)
        {
            List<Album> AlbumList = _context.Albums.Where(x => x.ArtistId == ArtistId).ToList();
            return Json(AlbumList);//new { Data=AlbumList});
        }//, System.Web.Mvc.JsonRequestBehavior.AllowGet

        [HttpGet] 
        // GET: Songs/Create
        public IActionResult Create()
        {
            //ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name");
            List<Artist> ArtistList = _context.Artists.ToList();
            ViewBag.ArtistId = new SelectList(ArtistList, "Id", "Name");
            //ViewBag["ArtistId"]
            ViewData["Albums"] = _context.Albums;
            ViewData["Artists"] = _context.Artists;

            return View();
        }

        // POST: Songs/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Duration,AlbumId,ArtistId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.ArtistId);
            return View(song);
        }

        // GET: Songs/Edit/5
        [HttpGet] 
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            List<Artist> ArtistList = _context.Artists.ToList();
            ViewBag.ArtistId = new SelectList(ArtistList, "Id", "Name");
            // ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.ArtistId);
            return View(song);
        }

        // POST: Songs/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Duration,AlbumId,ArtistId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.ArtistId);
            return View(song);
        }

        // GET: Songs/Delete/5
        [HttpGet] 
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(string id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
