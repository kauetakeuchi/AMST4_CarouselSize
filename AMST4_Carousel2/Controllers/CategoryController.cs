using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMST4_Carousel2.Context;
using AMST4_Carousel2.Models;

namespace AMST4_Carousel2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDataContext _context;

        public CategoryController(ApplicationDataContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var category = _context.Category.ToList();
            return View(category);
        }

        public IActionResult CategoryDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Category
                .FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult AddCategory()
        {
            return View();
        } 

        [HttpPost]

        public IActionResult AddCategory(Category category)
        {
                category.Id = Guid.NewGuid();
                _context.Add(category);
                _context.SaveChanges();
                return RedirectToAction("CategoryList");
        }

        public IActionResult CategoryEdit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]

        public IActionResult CategoryEdit(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CategoryList");
            }
            return View(category);
        }

        public IActionResult CategoryDelete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Category
                .FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("CategoryDelete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var category = _context.Category.Find(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }

            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
