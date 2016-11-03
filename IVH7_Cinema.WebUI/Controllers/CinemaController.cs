using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.WebUI.Models;
using System.Globalization;

namespace IVH7_Cinema.WebUI.Controllers
{
    public class CinemaController : Controller
    {
        private ICinemaRepository Repository;
        public int PageSize = 4;

        public CinemaController(ICinemaRepository repPar)
        {
            Repository = repPar;
            ViewBag.ShowID = 0;
        }

        public ViewResult Index()
        {
            return View();
        }

        // GET: Cinema
        public ViewResult FilmIndex(Int64 ShowID = 0, int page = 1)
        {
            ViewBag.ShowID = ShowID;
            ViewBag.Page = page;

            ShowsListViewModel model = new ShowsListViewModel
            {
                Shows = Repository.Shows.Where(s => s.DateTime >= DateTime.Now && s.DateTime < DateTime.Today.AddDays(1) && s.CinemaID == 1).OrderBy(s => s.DateTime.ToString("HH:mm", new CultureInfo("nl-NL"))).Skip((page - 1) * PageSize).Take(PageSize),


                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = Repository.Shows.Where(s => s.DateTime >= DateTime.Now && s.DateTime < DateTime.Today.AddDays(1) && s.CinemaID == 1).OrderBy(s => s.DateTime.ToString("HH:mm", new CultureInfo("nl-NL"))).Count()
                }
            };

            return View("FilmIndex", model);
        }

        public ViewResult Order(Int64 ShowID)
        {
            ViewBag.Tariffs = Repository.Tariffs;
            
            Show show = Repository.Shows.Where(x => x.ShowID == ShowID).First();

            if (show.Movie.Genres.Count == 0 || show.Movie.Languages.Count == 0) {
                return View("Index");
            }

            return View("Order", show);
        }

        public ViewResult DeleteOrder(Int64 orderID)
        {
            Repository.DeleteOrder(orderID);
            return View("Index");
        }

        public ViewResult ReserveringOphalen()
        {
            return View("ReserveringOphalen");
        }

        [HttpPost]
        public ActionResult PinCheck(Int64 code)
        {
            System.Diagnostics.Debug.WriteLine("HTTP POST");
            System.Diagnostics.Debug.WriteLine("" + code);
            if (Repository.Orders.ToList<Order>().Count() != 0)
            {

                foreach (Order r in Repository.Orders.ToList<Order>())
                {
                    if (r.OrderID.Equals(code))
                    {
                        if (r.Status.Equals("Voltooid"))
                        {
                            return RedirectToAction("PinComplete", "Pin", r);
                        }
                        else if(r.Status.Equals("Gereserveerd"))
                        {
                            return RedirectToAction("PinViewReservation", "Pin", r);
                        }
                    }
                }
            }

            return View("NoOrdersFound");
        }


        //public Boolean AssignSeats(int requiredSeats, Show show)
        //{
        //    //Haal de stoelen op
        //    IEnumerable<Seat> Seats = show.AssignSeats(requiredSeats);
        //    //check of de stoelen ook echt leeg zijn
        //    if (Seats.Where(s => s.Vacated == false).Count() == requiredSeats)
        //    {
        //        //als dat zo is zet ze op bezet ( ze zijn nu toegewezen )
        //        Repository.SetVacated(Seats, show);
        //        return true;
        //    }
        //    else
        //    {
        //        //als er een fout is opgetreden return false
        //        return false;
        //    }
        //}
    }
}