using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.WebUI.Models;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.WebUI.Controllers {
    public class PinController : Controller {
        // GET: Pin
        private IPrinter Printer;
        private ICinemaRepository Repository;
        public int PageSize = 4;

        public PinController(ICinemaRepository repPar, IPrinter printer) {
            Repository = repPar;
            Printer = printer;
        }

        /// <summary>
        /// ActionResult tringering the pinView
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        [HttpPost]
        public ActionResult PinView(Int64 ShowID) {
            Show show = Repository.Shows.Where(x => x.ShowID == ShowID).ElementAtOrDefault(0);
            ViewBag.MovieTitle = show.Movie.Title;
            ViewBag.Is3D = show.Is3D;
            System.Diagnostics.Debug.WriteLine("PinController - PinView - Movie Title = " + show.Movie.Title);
            ViewBag.Amount3DBrilTariffs = 0;
            ViewBag.AmountPopcornArrangementTariffs = 0;

            //Determine amount of required seats
            int amountOfRequiredSeats = 0;
            int amountOfDifferentTariffs = 0;
            for (int i = 0; i < Request.Form.Count; i++) {
                //Determine Amount 3D Bril Tariffs
                if (Request.Form.GetKey(i).Equals("3D Bril")) {
                    if (Int32.Parse(Request.Form.Get(i)) != 0) {
                        ViewBag.Amount3DBrilTariffs = Request.Form.Get(i);
                    }
                }

                //Determine Amount Popcorn Arrangement Tariffs
                if (Request.Form.GetKey(i).Equals("Popcorn Arrangement")) {
                    if (Int32.Parse(Request.Form.Get(i)) != 0) {
                        ViewBag.AmountPopcornArrangementTariffs = Request.Form.Get(i);
                    }
                }

                if (!Request.Form.GetKey(i).Equals("3D Bril") && !Request.Form.GetKey(i).Equals("Popcorn Arrangement") && !Request.Form.GetKey(i).Equals("totalPriceOrder")) {
                    amountOfRequiredSeats += Int32.Parse(Request.Form.Get(i));

                    if (Int32.Parse(Request.Form.Get(i)) != 0) {
                        amountOfDifferentTariffs++;
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("PinController - PinView - Totaal aantal stoelen nodig: " + amountOfRequiredSeats);

            if (amountOfRequiredSeats != 0) {
                //Get seats
                IEnumerable<Seat> assignedSeats = show.AssignSeats(amountOfRequiredSeats);
                int currentSeat = 0;

                System.Diagnostics.Debug.WriteLine("PinController - PinView - Request Form Count = " + Request.Form.Count);

                System.Diagnostics.Debug.WriteLine("PinController - PinView - Totaal aantal assigned seats: " + assignedSeats.Count());

                if (assignedSeats == null) {
                    System.Diagnostics.Debug.WriteLine("PinController - PinView - AssignedSeats is null");
                }

                foreach (Seat s in assignedSeats) {
                    System.Diagnostics.Debug.WriteLine("PinController - PinView - Seat: " + s.SeatID + " - Row: " + s.RowNumber);
                }

                //Make new order
                Order order = new Order { Status = "Besteld" };
                order.GenerateRandomNumber();
                Session["OrderID"] = order.OrderID;
                List<Ticket> tickets = new List<Ticket>();

                //Make new tickets
                int y = 0;
                while (y < amountOfDifferentTariffs) {
                    System.Diagnostics.Debug.WriteLine("Tariff: " + Repository.GetTariff(Request.Form.GetKey(y)).Name);
                    System.Diagnostics.Debug.WriteLine("Show: " + show.Movie.Title);

                    for (int p = 0; p < Request.Form.Count; p++) {
                        if (Request.Form.GetKey(p).Equals(Request.Form.GetKey(y))) {
                            int amount = Int32.Parse(Request.Form.Get(p)); //aantal per tarief
                            for (int c = 0; c < amount; c++) {
                                Ticket newTicket = new Ticket {
                                    Tariff = Repository.GetTariff(Request.Form.GetKey(y)),
                                    Show = show,
                                    Seat = assignedSeats.ElementAt(currentSeat),
                                    Order = order
                                };
                                currentSeat++;
                                tickets.Add(newTicket);
                            }
                        }
                    }
                    y++;
                }

                order.Tickets = tickets;

                double totaalprijs = 0.00;
                for (int a = 0; a < Request.Form.Count; a++) {
                    if (Request.Form.GetKey(a).Equals("totalPriceOrder")) {
                        totaalprijs = double.Parse(Request.Form.Get(a), System.Globalization.CultureInfo.InvariantCulture);
                    }
                }

                order.Totaalprijs = totaalprijs;

                Repository.AddOrder(order);

                /*foreach(Ticket b in tickets) {
                     System.Diagnostics.Debug.WriteLine("Ticket: " + b.Tariff.Name);
                     Repository.addTickets(b);
                 }*/

                return View("PinView", order);
            } else {
                return RedirectToAction("Order", "Cinema", new { ShowID = ShowID });
            }
        }

        public ActionResult PinViewReservation(Order order)
        {
            return View("PinView", order);
        }


        /// <summary>
        /// ActionResult trigering the PinComplete
        /// </summary>
        /// <param name="ShowID"></param>
        /// <returns></returns>
        
        public ActionResult PinComplete(Int64 OrderID, int Amount3DBrilTariffs = 0, int AmountPopcornArrangementTariffs = 0) {
            System.Diagnostics.Debug.WriteLine("ID:" + OrderID);
            Repository.ChangeOrderStatus(OrderID, "Voltooid");
            Order order = null;
            List<Order> orders = new List<Order>();
            orders = Repository.Orders.ToList<Order>();

            foreach (Order o in orders) {
                if (o.OrderID == OrderID) {
                    order = o;
                }
            }

            //Created on 3/5/2015-7:48
            //Create a list, and find the tickets associated to that show
            List<Ticket> Tickets = Repository.Tickets.ToList<Ticket>();
            System.Diagnostics.Debug.WriteLine("PinController - PinComplete - Amount 3D Brillen = " + Amount3DBrilTariffs);
            System.Diagnostics.Debug.WriteLine("PinController - PinComplete - Amount Popcorn = " + AmountPopcornArrangementTariffs);

            int id = 0;
            DateTime orderdate = new DateTime();

            foreach (Ticket t in Tickets.Where(x => x.OrderID == OrderID)) {
                if (t.OrderID == OrderID) {
                    id = (int)t.OrderID;
                    orderdate = t.Show.DateTime;
                }
            };

            Printer.PrintBiosTicket(Tickets.Where(x => x.OrderID == OrderID).ToList(), id);

            Printer.Print3DTicket(Amount3DBrilTariffs, id, orderdate);

            Printer.PrintPopcornTicket(AmountPopcornArrangementTariffs, id, orderdate);
            return View("PinComplete", order);

        }
    }
}