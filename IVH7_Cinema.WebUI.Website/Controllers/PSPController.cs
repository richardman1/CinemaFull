using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVH7_Cinema.WebUI.Website.Models;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf.draw;
using System.IO;
using IVH7_Cinema.Domain.Abstract;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace IVH7_Cinema.WebUI.Website.Controllers {
    public class PSPController : BaseController {

        ICinemaRepository Repository;
        private IPrinter Printer;
        private IMailer Mailer;

        public PSPController(ICinemaRepository Repository, IPrinter printer, IMailer mailer) {
            this.Repository = Repository;
            Printer = printer;
            Mailer = mailer;

            ViewData["Cinemas"] = Repository.Cinemas.Select(x => new Cinema { CinemaID = x.CinemaID, Email = x.Email, Phone = x.Phone, Name = x.Name, Address = x.Address, ZipCode = x.ZipCode, City = x.City }).OrderBy(x => x.City).ThenBy(x => x.Name).ToList();
        }

        [HttpPost][ExcludeFromCodeCoverage]
        public ActionResult Information(Int64 ShowID) {
            Show show = Repository.Shows.Where(x => x.ShowID == ShowID).ElementAtOrDefault(0);
            ViewBag.MovieTitle = show.Movie.Title;
            ViewBag.Is3D = show.Is3D;
            System.Diagnostics.Debug.WriteLine("PinController - PinView - Movie Title = " + show.Movie.Title);
            ViewBag.Amount3DBrilTariffs = 0;
            ViewBag.AmountPopcornArrangementTariffs = 0;
            Session["Amount3DBrilTariffs"] = 0;
            Session["AmountPopcornArrangementTariffs"] = 0;
            Session["OrderReturn"] = 0;

            //Determine amount of required seats
            int amountOfRequiredSeats = 0;
            int amountOfDifferentTariffs = 0;
            for (int i = 0; i < Request.Form.Count; i++) {
                //Determine Amount 3D Bril Tariffs
                if (Request.Form.GetKey(i).Equals("3D Bril")) {
                    if (Int32.Parse(Request.Form.Get(i)) != 0) {
                        Session["Amount3DBrilTariffs"] = Request.Form.Get(i);
                    }
                }

                //Determine Amount Popcorn Arrangement Tariffs
                if (Request.Form.GetKey(i).Equals("Popcorn Arrangement")) {
                    if (Int32.Parse(Request.Form.Get(i)) != 0) {
                        Session["AmountPopcornArrangementTariffs"] = Request.Form.Get(i);
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



                Order order = new Order { Status = "Besteld" };
                order.GenerateRandomNumber();
                Session["OrderReturn"] = order.OrderID;
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

                ViewBag.OrderID = order.OrderID;

                return View("InformationPost");
            } else {
                return RedirectToAction("Order", "Home", new { ShowID = ShowID });
            }
        }

        [HttpPost][ExcludeFromCodeCoverage]
        public ViewResult InformationPost(Payment payment, Int64 OrderID) {
            if (ModelState.IsValid) {
                Order order = null;
                order = Repository.GetOrder(OrderID);
                Session["Email"] = payment.Email;
                if (payment.Betaalwijze.Equals("iDeal")) {
                    IDealListViewModel IDealListView = new IDealListViewModel {
                        payment = payment,
                        order = order
                    };
                    Repository.ChangeOrderStatus(order.OrderID, "Voltooid");
                    ViewBag.CustomerInformation = payment;
                    
                    return View("iDealPayment", IDealListView);
                } else if (payment.Betaalwijze.Equals("Creditcard")) {
                    CreditCardListViewModel CreditCardListView = new CreditCardListViewModel {
                        payment = payment,
                        order = order
                    };
                    Repository.ChangeOrderStatus(order.OrderID, "Voltooid");
                    ViewBag.CustomerInformation = payment;
                    return View("CreditCardPayment", CreditCardListView);
                } else {
                    ReserveringListViewModel ReserveringListView = new ReserveringListViewModel {
                        payment = payment,
                        order = order
                    };
                    Repository.ChangeOrderStatus(order.OrderID, "Gereserveerd");
                    ViewBag.CustomerInformation = payment;
                    Mailer.SendShortMessage(Session["Email"].ToString(), OrderID);
                    return View("Reservering", ReserveringListView);
                }
            }
            return View();
        }

        [HttpPost][ExcludeFromCodeCoverage]
        public ViewResult iDealPayment(IDealListViewModel model, Int64 OrderID) {
            if (ModelState.IsValid) {
                if (model.Validation == 7654321) {
                    // create pdf
                    List<Ticket> Tickets = new List<Ticket>();
                    Order order = null;
                    order = Repository.GetOrder(OrderID);
                    foreach (Ticket t in order.Tickets) {
                        Tickets.Add(t);
                    }

                    Printer.PrintA4Ticket(Tickets, Int64.Parse(Session["AmountPopcornArrangementTariffs"].ToString()), Int64.Parse(Session["Amount3DBrilTariffs"].ToString()));

                    Repository.ChangeOrderStatus(OrderID, "Voltooid");

                    model.order = order;

                    //Mailer.SendShortMessage(OrderID);
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/PDFs/");
                    string output = path + OrderID + "_Ticket.pdf";
                    Mailer.sentOrderPDF(Session["Email"].ToString(), OrderID, output);
                    // return view that shows succes
                    return View("IDealBetalingSucces", model);
                } else {
                    Repository.DeleteOrder(OrderID);
                    return View("iDealFout", model);
                }
            }
            return View();
        }

        [HttpPost][ExcludeFromCodeCoverage]
        public ViewResult CreditCardPayment(CreditCardListViewModel model, Int64 OrderID) {
            if (ModelState.IsValid) {
                if (IVH7_Cinema.WebUI.Website.Models.Luhn.LuhnCheck(model.CreditCardNumber.ToString()) == true) {
                    List<Ticket> Tickets = new List<Ticket>();
                    Order order = null;
                    order = Repository.GetOrder(OrderID);
                    foreach (Ticket t in order.Tickets) {
                        Tickets.Add(t);
                    }

                    Printer.PrintA4Ticket(Tickets, Int64.Parse(Session["AmountPopcornArrangementTariffs"].ToString()), Int64.Parse(Session["Amount3DBrilTariffs"].ToString()));

                    Repository.ChangeOrderStatus(OrderID, "Voltooid");

                    model.order = order;
                    // Mailer.SendShortMessage(OrderID);
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/PDFs/");
                    string output = path + OrderID + "_Ticket.pdf";
                    Mailer.sentOrderPDF(Session["Email"].ToString(), OrderID, output);
                    // return view that shows succes                    
                    return View("CreditCardBetalingSucces", model);
                } else {
                    Repository.DeleteOrder(OrderID);
                    return View("CreditcardFout", model);
                }
            }
            return View();
        }

        public ActionResult RemoveOrderOnExit(Int64 orderNr) {
            Repository.DeleteOrder(orderNr);
            return RedirectToAction("Index", "Home");
        }

    }
}