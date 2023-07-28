using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp.Controllers
{
    [Authorize]
    public class DocumentCategoriesController : Controller
    {
        private readonly StudentBlogContext _context;
        
        public DocumentCategoriesController(StudentBlogContext context)
        {
            _context = context;
        }
		[Authorize(Roles ="Admin")]
		// GET: DocumentCategories
		public async Task<IActionResult> Index()
        {
              return _context.DocumentCategories != null ? 
                          View(await _context.DocumentCategories.ToListAsync()) :
                          Problem("Entity set 'StudentBlogContext.DocumentCategories'  is null.");
        }

        // GET: DocumentCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DocumentCategories == null)
            {
                return NotFound();
            }

            var documentCategory = await _context.DocumentCategories
                .FirstOrDefaultAsync(m => m.DocCategoryId == id);
            if (documentCategory == null)
            {
                return NotFound();
            }

            return View(documentCategory);
        }

        // GET: DocumentCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocCategoryId,CategoryName")] DocumentCategory documentCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentCategory);
        }

        // GET: DocumentCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocumentCategories == null)
            {
                return NotFound();
            }

            var documentCategory = await _context.DocumentCategories.FindAsync(id);
            if (documentCategory == null)
            {
                return NotFound();
            }
            return View(documentCategory);
        }

        // POST: DocumentCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocCategoryId,CategoryName")] DocumentCategory documentCategory)
        {
            if (id != documentCategory.DocCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentCategoryExists(documentCategory.DocCategoryId))
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
            return View(documentCategory);
        }

        // GET: DocumentCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DocumentCategories == null)
            {
                return NotFound();
            }

            var documentCategory = await _context.DocumentCategories
                .FirstOrDefaultAsync(m => m.DocCategoryId == id);
            if (documentCategory == null)
            {
                return NotFound();
            }

            return View(documentCategory);
        }

        // POST: DocumentCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DocumentCategories == null)
            {
                return Problem("Entity set 'StudentBlogContext.DocumentCategories'  is null.");
            }
            var documentCategory = await _context.DocumentCategories.FindAsync(id);
            if (documentCategory != null)
            {
                _context.DocumentCategories.Remove(documentCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentCategoryExists(int id)
        {
          return (_context.DocumentCategories?.Any(e => e.DocCategoryId == id)).GetValueOrDefault();
        }
    }
}
