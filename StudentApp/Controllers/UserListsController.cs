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
    public class UserListsController : Controller
    {
        private readonly StudentBlogContext _context;

        public UserListsController(StudentBlogContext context)
        {
            _context = context;
        }

        // GET: UserLists
        public  IActionResult Index()
        {
            List<UserList> userList = _context.UserLists.ToList();

		   return View(userList);
                         
        }

        // GET: UserLists/Details/5
        public IActionResult Details()
        {
            

            var userList = _context.UserLists.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name)).FirstOrDefault();
               
            

            return View(userList);
        }

        // GET: UserLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,UserPassword,UserAddress,UserRoleType,UserPhone")] UserList userList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userList);
        }

        // GET: UserLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserLists == null)
            {
                return NotFound();
            }

            var userList = await _context.UserLists.FindAsync(id);
            if (userList == null)
            {
                return NotFound();
            }
            return View(userList);
        }

        // POST: UserLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,UserPassword,UserAddress,UserRoleType,UserPhone")] UserList userList)
        {
            if (id != userList.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserListExists(userList.UserId))
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
            return View(userList);
        }

        // GET: UserLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserLists == null)
            {
                return NotFound();
            }

            var userList = await _context.UserLists
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        // POST: UserLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserLists == null)
            {
                return Problem("Entity set 'StudentBlogContext.UserLists'  is null.");
            }
            var userList = await _context.UserLists.FindAsync(id);
            if (userList != null)
            {
                _context.UserLists.Remove(userList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserListExists(int id)
        {
          return (_context.UserLists?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
