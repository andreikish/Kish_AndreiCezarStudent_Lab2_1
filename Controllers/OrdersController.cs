using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kish_AndreiCezarStudent_Lab2.Data;
using Kish_AndreiCezarStudent_Lab2.Models;

namespace Kish_AndreiCezarStudent_Lab2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Kish_AndreiCezarStudent_Lab2Context _context;

        public OrdersController(Kish_AndreiCezarStudent_Lab2Context context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int? bookId)
        {
            var orders = _context.Order
                .Include(o => o.Book)
                .Include(o => o.Customer)
                .AsQueryable();

            if (bookId.HasValue)
            {
                orders = orders.Where(o => o.BookID == bookId.Value);
            }

            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Book)
                .FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(int? bookId)
        {
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title", bookId);
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name");
            var order = new Order { BookID = bookId, OrderDate = DateTime.Now };
            return View(order);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,CustomerID,BookID,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title", order.BookID);
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            PopulateSelectLists(order.CustomerID, order.BookID);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,CustomerID,BookID,OrderDate")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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

            PopulateSelectLists(order.CustomerID, order.BookID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Book)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopulateSelectLists(int? customerId = null, int? bookId = null)
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", customerId);
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title", bookId);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}

