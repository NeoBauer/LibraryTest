using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryTest.Models;
using LibraryTest.Services;
using System;

namespace LibraryTest.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService service)
        {
            _bookService = service;
        }

        public IActionResult Index()
        {
            return NewBook();
        }

        public IActionResult NewBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                _bookService.Add(model);
                Console.Write("Dodaje ksiazke");
                return RedirectToAction("NewBook");
            }
            return View();
        }

        public async Task<IActionResult> BooksList()
        {
            var result =  await _bookService.GetAllBooks();
            ViewBag.BookList = result;
            return View();

        }

    }
}
