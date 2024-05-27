﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AMST4_Carousel2.Context;
using AMST4_Carousel2.Models;

namespace AMST4_Carousel2.Controllers
{
    public class SizeController : Controller
    {
        private readonly ApplicationDataContext _context;

        public SizeController(ApplicationDataContext context)
        {
            _context = context;
        }

        public IActionResult SizeList()
        {
            var size = _context.Size.ToList();
            return View(size);
        }

        public IActionResult SizeDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = _context.Size
                .FirstOrDefault(m => m.Id == id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        public IActionResult AddSize()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSize(Size size)
        {
                size.Id = Guid.NewGuid();
                _context.Add(size);
                _context.SaveChanges();
                return RedirectToAction("SizeList");
        }

        public IActionResult SizeEdit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = _context.Size.Find(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        [HttpPost]
        public IActionResult SizeEdit(Guid id, Size size)
        {
            if (id != size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(size);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeExists(size.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("SizeList");
            }
            return View(size);
        }

        public IActionResult SizeDelete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = _context.Size
                .FirstOrDefault(m => m.Id == id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        [HttpPost, ActionName("SizeDelete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var size = _context.Size.Find(id);
            if (size != null)
            {
                _context.Size.Remove(size);
            }

            _context.SaveChanges();
            return RedirectToAction("SizeList");
        }

        private bool SizeExists(Guid id)
        {
            return _context.Size.Any(e => e.Id == id);
        }
    }
}
