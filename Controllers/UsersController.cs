﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrmAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using CrmAssistant.Middlewares;
using System.Dynamic;
using CrmAssistant.Models.ViewModels;
using CrmAssistant.Models.Map;
using CrmAssistant.Models.Enums;

namespace CrmAssistant.Controllers
{
    [Authorize]
    [MiddlewareFilter(typeof(AdminMiddleware))]
    public class UsersController : Controller
    {
        private readonly PubContext _context;

        public UsersController(PubContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'PubContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                TempData[Notification.ERROR.ToString()] = "Success creating user";

                return RedirectToAction(nameof(Index));
            }

            TempData[Notification.ERROR.ToString()] = "Error creating user";
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(x => x.Address)
                .Include(x => x.Countries)
                .Include(x => x.Hobbies)
                .Where(x => x.Id == id)
                .SingleAsync();

            if (user == null)
            {
                return NotFound();
            }

            //UserViewModel userViewModel = UserMap.toUserViewModel(user);
            //userViewModel.Hobbies.AddRange(selectListItems);

            ViewBag.Hobbies = _context.Hobbies
                .Select(hobbie => new SelectListItem { Value = hobbie.Id.ToString(), Text = hobbie.Name })
                .ToList();

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            

            if (id != user.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    //User user = UserMap.toUser(userViewModel);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData[Notification.SUCCESS.ToString()] = "Success updating user";
                return RedirectToAction(nameof(Index));
            }

            TempData[Notification.ERROR.ToString()] = "Error updating user";
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'PubContext.Users'  is null.");
            }

            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();

            TempData[Notification.SUCCESS.ToString()] = "Success deleting user";
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
