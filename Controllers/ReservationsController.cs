using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using LibraryTest.Services;
using System.Security.Claims;

namespace LibraryTest.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ReservationService _ReservationService;
        public ReservationsController(ReservationService service)
        {
            _ReservationService = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewReservation(string isbn)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ReservationService.CreateNewReservation(isbn, userId);
            return View();
        }

        public IActionResult ReservationListForUserID()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var AllReserv = _ReservationService.GetAllReservationForUser(userId).Result;
            List<string> NameList = new List<string>();
            foreach (var reserv in AllReserv)
            {
                var BookName = _ReservationService.GetBookNameByISBN(reserv.ReservedBookISBN);
                NameList.Add(BookName);
            }

            var ReservWithName = AllReserv.Zip(NameList, Tuple.Create);
            ViewBag.reservations = ReservWithName;
            return View();
        }
        [HttpPost]
        public  IActionResult ReturnBook(string isbn)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ReservationService.DeleteReservation(isbn, userId);
            return RedirectToAction("ReservationListForUserID", "Reservations");
        }
    }   
}
