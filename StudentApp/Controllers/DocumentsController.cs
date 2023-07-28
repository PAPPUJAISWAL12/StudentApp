using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly StudentBlogContext _context;
        private readonly IWebHostEnvironment _env;

        public DocumentsController(StudentBlogContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var docList = await _context.DocumentCategoryViews.ToListAsync();
            return View(docList);
        }
        public  IActionResult TeamList()
        {
            var docList = _context.DocumentCategoryViews.Where(x=>x.CategoryName=="Team" ).OrderByDescending(x=>x.DocId).ToList();
            
            return PartialView("_TeamList",docList);
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.DocCategory)
                .FirstOrDefaultAsync(m => m.DocId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            var catlist = _context.DocumentCategories.ToList();
            ViewData["docList"] = new SelectList(catlist, "DocCategoryId", nameof(DocumentCategory.CategoryName));
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocId,DocCategoryId,Title,Descriptions,FileUpload")] Document document)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(document.FileUpload.FileName);
            string imgpath = Path.Combine(_env.WebRootPath,"docImage",filename);
            using(FileStream streamread = new FileStream(imgpath, FileMode.Create))
            {
                document.FileUpload.CopyTo(streamread);
            }
             document.DocFile=filename;

             _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["DocCategoryId"] = new SelectList(_context.DocumentCategories, "DocCategoryId", "DocCategoryId", document.DocCategoryId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocId,DocCategoryId,Title,Descriptions")] Document document)
        {
            if (id != document.DocId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.DocId))
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
            ViewData["DocCategoryId"] = new SelectList(_context.DocumentCategories, "DocCategoryId", "DocCategoryId", document.DocCategoryId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.DocCategory)
                .FirstOrDefaultAsync(m => m.DocId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Documents == null)
            {
                return Problem("Entity set 'StudentBlogContext.Documents'  is null.");
            }
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
          return (_context.Documents?.Any(e => e.DocId == id)).GetValueOrDefault();
        }
    }
}
