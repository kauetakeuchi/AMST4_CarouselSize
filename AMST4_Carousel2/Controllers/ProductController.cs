using System;
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
    public class ProductController : Controller
    {
        private readonly ApplicationDataContext _context;

        public ProductController(ApplicationDataContext context)
        {
            _context = context;
        }

        public IActionResult ProductList()
        {
            var product = _context.Product.ToList();
            return View(product);
        }

        public IActionResult ProductDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Product
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult AddProduct()
        {
            ViewBag.CategoryList = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        [HttpPost]

        public IActionResult AddProduct(Product product)
        {
                product.Id = Guid.NewGuid();
                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction("ProductList");
        }

        public IActionResult ProductEdit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]

        public IActionResult ProductEdit(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ProductList");
            }
            
            return View(product);
        }

        public IActionResult ProductDelete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Product
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("ProductDelete")]

        public IActionResult DeleteConfirmed(Guid id)
        {
            var product = _context.Product.Find(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
