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


namespace IVH7_Cinema.Domain.Entities {
    [ExcludeFromCodeCoverage]
    public class PDFPrinter : IPrinter {

         //PageA4PDF.cs (Website)
        public void PrintA4Ticket(List<Ticket> T, Int64 ContainsPopcorn = 0, Int64 Contains3D = 0) {
            foreach (Ticket ticket in T) {
                CreateA4Ticket(T, ContainsPopcorn, Contains3D);
            }
        }

        //PrintTouchTicket(PrintTicketPDF.cs) (TouchScreen)
        public void PrintBiosTicket(List<Ticket> T, int id) {
            foreach (Ticket ticket in T) {
                CreateBiosPDFTicket(T, id);
            }
        }

        //Print3DTicket (TouchScreen)
        public void Print3DTicket(int Amount, int id, DateTime today) {
            for (int i = 1; i < Amount + 1; i++) {
                Create3DTicket(i, Amount, id, today);
            }
        }

        //PrintPopcorn (TouchScreen)
        public void PrintPopcornTicket(int Amount, int id, DateTime today) {
            for (int i = 1; i < Amount + 1; i++) {
                CreatePopcornTicket(i, Amount, id, today);
                CreatePopTicket(i, Amount, id, today);
            }
        }

        private void CreateA4Ticket(List<Ticket> tickets, Int64 ContainsPopcorn, Int64 Contains3D) {
            //List<StringBuilder> Tickets = new List<StringBuilder>();

            //Create the document
            var document = new Document(PageSize.A4);

            //set the output and the writer\
            string path = HttpContext.Current.Server.MapPath("~/PDFs/");
            string orderID = tickets.First().OrderID.ToString();
            var output = new FileStream(path + orderID + "_Ticket.pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(document, output);

            //open the document
            document.Open();

            //Get access to the raw PdfContentBytes
            var cb = writer.DirectContent;

            //Set the font
            var titleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 8, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            foreach (Ticket T in tickets) {
                //set all the essential needed input
                string MovieName = T.Show.Movie.Title;
                string MoviePhoto = T.Show.Movie.ImageURL;
                string MovieDescription = T.Show.Movie.Description;
                string MovieDirector = T.Show.Movie.Director;
                bool is3D = T.Show.Is3D;
                string Language = T.Show.Language.LanguageCode;
                string CardType = T.Tariff.Name;
                string Price = T.Tariff.Price.ToString("n2");
                Int64 Screen = T.Show.Screen.ScreenID;
                Int64 Row = T.Seat.RowNumber;
                Int64 SeatNumber = T.Seat.SeatNumber;
                Int64 TicketNr = T.TicketID;
                DateTime showtime = T.Show.DateTime;
                string KindMovie = "";

                //Create the elements
                if (is3D) {
                    KindMovie = " 3D";
                } else {
                    KindMovie = "";
                }


                var ticketinformation = new Paragraph("Dit kaartje zal enkel aan de ingang geldig zijn. Alleen het eerste kaartje is geldig.");
                ticketinformation.Alignment = Element.ALIGN_LEFT;

                var Header = new Paragraph(MovieName + KindMovie + " (" + Language + ")", titleFont);
                Header.Alignment = Element.ALIGN_LEFT;
                var time = new Paragraph(showtime.ToString("d-M-yyyy HH:mm", new CultureInfo("nl-NL")), subTitleFont);
                time.Alignment = Element.ALIGN_RIGHT;
                var ticketNummer = new Paragraph("Ticketnummer: " + TicketNr, bodyFont);
                ticketNummer.Alignment = Element.ALIGN_LEFT;
                var ScreenRowSeat = new Paragraph("Zaal: " + Screen + " " + "Rij: " + Row + " " + "Stoel: " + SeatNumber, bodyFont);
                ScreenRowSeat.Alignment = Element.ALIGN_LEFT;
                var PriceType = new Paragraph("Prijs:  €" + Price, bodyFont);
                PriceType.Alignment = Element.ALIGN_RIGHT;
                var Type = new Paragraph("Tarief: " + CardType, bodyFont);
                Type.Alignment = Element.ALIGN_LEFT;

                //Add the paragraphs to the document
                document.Add(ticketinformation);

                PdfPTable table = new PdfPTable(2);
                table.DefaultCell.BorderWidth = 0;
                table.HorizontalAlignment = Element.ALIGN_LEFT;

                //Header
                PdfPCell pcell = new PdfPCell();
                pcell.BorderWidth = 0;
                pcell.AddElement(Header);
                table.AddCell(pcell);
                //Tijd
                PdfPCell pcell2 = new PdfPCell();
                pcell2.BorderWidth = 0;
                pcell2.AddElement(time);
                table.AddCell(pcell2);
                //ticketnummer
                PdfPCell pcell5 = new PdfPCell();
                pcell5.BorderWidth = 0;
                pcell5.AddElement(ticketNummer);
                table.AddCell(pcell5);
                //leeg
                PdfPCell pcell6 = new PdfPCell();
                pcell6.BorderWidth = 0;
                pcell6.AddElement(new Paragraph(""));
                table.AddCell(pcell6);
                //ScreenRowSeat
                PdfPCell pcell7 = new PdfPCell();
                pcell7.BorderWidth = 0;
                pcell7.AddElement(ScreenRowSeat);
                table.AddCell(pcell7);
                //Leeg
                PdfPCell pcell8 = new PdfPCell();
                pcell8.BorderWidth = 0;
                pcell8.AddElement(new Paragraph(""));
                table.AddCell(pcell8);
                //Type
                PdfPCell pcell9 = new PdfPCell();
                pcell9.BorderWidth = 0;
                pcell9.AddElement(Type);
                table.AddCell(pcell9);
                //PriceType
                PdfPCell pcell10 = new PdfPCell();
                pcell10.BorderWidth = 0;
                pcell10.AddElement(PriceType);
                table.AddCell(pcell10);

                document.Add(table);

                DottedLineSeparator separator = new DottedLineSeparator();
                Chunk linebreak = new Chunk(separator);
                document.Add(linebreak);
            }

            if (ContainsPopcorn != 0) {
                for (int i = 0; i < ContainsPopcorn; i++) {

                    String showdate = "";
                    String showtime = "";

                    foreach (Ticket T in tickets) {
                        showdate = T.Show.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL"));
                        showtime = T.Show.DateTime.ToString("HH:mm", new CultureInfo("nl-NL"));
                    }

                    var TimeMessage = new Paragraph("Bij de " + showtime + " uur show", bodyFont);
                    TimeMessage.Alignment = Element.ALIGN_LEFT;

                    var DateMessage = new Paragraph("Alleen geldig op de volgende datum:" + showdate, bodyFont);
                    DateMessage.Alignment = Element.ALIGN_LEFT;

                    var popcornmessage = new Paragraph("Met dit kaartje heeft u recht op één popcorn!", titleFont);
                    popcornmessage.Alignment = Element.ALIGN_LEFT;

                    var popmessage = new Paragraph("Met dit kaartje heeft u recht op één frisdrank!", titleFont);
                    popmessage.Alignment = Element.ALIGN_LEFT;

                    DottedLineSeparator separator = new DottedLineSeparator();
                    Chunk linebreak = new Chunk(separator);

                    //Popcorn
                    document.Add(DateMessage);
                    document.Add(TimeMessage);
                    document.Add(popcornmessage);
                    document.Add(linebreak);

                    //Drank
                    document.Add(DateMessage);
                    document.Add(TimeMessage);
                    document.Add(popmessage);
                    document.Add(linebreak);
                }
            }

            if (Contains3D != 0) {
                for (int j = 0; j < Contains3D; j++) {

                    String showdate = "";
                    String showtime = "";

                    foreach (Ticket T in tickets) {
                        showdate = T.Show.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL"));
                        showtime = T.Show.DateTime.ToString("HH:mm", new CultureInfo("nl-NL"));
                    }

                    var TimeMessage = new Paragraph("Bij de " + showtime + " uur show", bodyFont);
                    TimeMessage.Alignment = Element.ALIGN_LEFT;

                    var DateMessage = new Paragraph("Alleen geldig op de volgende datum:" + showdate, bodyFont);
                    DateMessage.Alignment = Element.ALIGN_LEFT;

                    var message = new Paragraph("Met dit kaartje heeft u recht op één 3D bril!", titleFont);
                    message.Alignment = Element.ALIGN_LEFT;

                    DottedLineSeparator separator = new DottedLineSeparator();
                    Chunk linebreak = new Chunk(separator);

                    document.Add(DateMessage);
                    document.Add(TimeMessage);
                    document.Add(message);
                    document.Add(linebreak);
                }

            }
            //close the document
            document.Close();



        }

        private void CreateBiosPDFTicket(List<Ticket> tickets, int id) {
            //List<StringBuilder> Tickets = new List<StringBuilder>();
            foreach (Ticket T in tickets) {
                //set all the essential needed input
                string MovieName = T.Show.Movie.Title;
                bool is3D = T.Show.Is3D;
                string Language = T.Show.Language.LanguageCode;
                string CardType = T.Tariff.Name;
                string Price = T.Tariff.Price.ToString("n2");
                Int64 Screen = T.Show.Screen.ScreenID;
                Int64 Row = T.Seat.RowNumber;
                Int64 SeatNumber = T.Seat.SeatNumber;
                Int64 TicketNr = T.TicketID;
                DateTime showtime = T.Show.DateTime;
                string KindMovie = "";

                //Create the document
                var document = new Document(PageSize.A7);

                //set the output and the writer\
                string path = HttpContext.Current.Server.MapPath("~/PDFs/");
                var output = new FileStream(path + id + "_Ticket_" + TicketNr + ".pdf", FileMode.Create);
                var writer = PdfWriter.GetInstance(document, output);

                //open the document
                document.Open();

                //Set the font
                var titleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var subTitleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                var endingMessageFont = FontFactory.GetFont("Arial", 8, Font.ITALIC);
                var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

                //Create the elements
                if (is3D) {
                    KindMovie = " 3D";
                } else {
                    KindMovie = "";
                }
                var Header = new Paragraph(MovieName + KindMovie + " (" + Language + ")", titleFont);
                Header.Alignment = Element.ALIGN_CENTER;
                var time = new Paragraph(showtime.ToString("d-M-yyyy HH:mm", new CultureInfo("nl-NL")), subTitleFont);
                time.Alignment = Element.ALIGN_CENTER;
                var ticketNummer = new Paragraph("Ticketnr: " + TicketNr);
                ticketNummer.Alignment = Element.ALIGN_CENTER;
                var ScreenRowSeat = new Paragraph("Zaal: " + Screen + " " + "Rij: " + Row + " " + "Stoel: " + SeatNumber, bodyFont);
                ScreenRowSeat.Alignment = Element.ALIGN_CENTER;
                var PriceType = new Paragraph("Prijs: " + Price, bodyFont);
                PriceType.Alignment = Element.ALIGN_CENTER;
                var Type = new Paragraph("Tarief: " + CardType, bodyFont);
                Type.Alignment = Element.ALIGN_CENTER;
                var message = new Paragraph("Enkel het eerste aan de ingang aangeboden ticket zal geldig zijn.", endingMessageFont);
                message.Alignment = Element.ALIGN_CENTER;

                //Add the paragraphs to the document
                document.Add(Header);
                document.Add(time);
                document.Add(ticketNummer);
                document.Add(ScreenRowSeat);
                document.Add(PriceType);
                document.Add(Type);
                document.Add(message);

                //close the document
                document.Close();
            }
        }

        private void Create3DTicket(int Number, int Amount, int id, DateTime today) {
            //Create the document
            var document = new Document(PageSize.A7.Rotate());

            //set the output and the writer
            string path = HttpContext.Current.Server.MapPath("~/PDFs/");
            var output = new FileStream(path + id + "_3DTicket_" + Number + ".pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(document, output);

            //open the document
            document.Open();

            //Set the font
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 8, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            //Create the elements
            var date = new Paragraph(today.ToString("d-M-yyyy", new CultureInfo("nl-NL")));
            date.Alignment = Element.ALIGN_RIGHT;
            var message = new Paragraph("Met dit kaartje heeft u recht op één 3D bril!", titleFont);
            message.Alignment = Element.ALIGN_CENTER;
            var total = new Paragraph(Number + "/" + Amount, endingMessageFont);
            total.Alignment = Element.ALIGN_RIGHT;

            //Add the paragraphs to the document
            document.Add(date);
            document.Add(message);
            document.Add(total);

            //close the document
            document.Close();
        }

        private void CreatePopcornTicket(int Number, int Amount, int id, DateTime today) {
            //List<StringBuilder> Tickets = new List<StringBuilder>();
            //Create the document
            var document = new Document(PageSize.A7.Rotate());

            //set the output and the writer
            string path = HttpContext.Current.Server.MapPath("~/PDFs/");
            var output = new FileStream(path + id + "_PopcornTicket_" + Number + ".pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(document, output);

            //open the document
            document.Open();

            //Set the font
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 8, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            //Create the elements
            var date = new Paragraph(today.ToString("d-M-yyyy", new CultureInfo("nl-NL")));
            date.Alignment = Element.ALIGN_RIGHT;
            var message = new Paragraph("Met dit kaartje heeft u recht op één popcorn!", titleFont);
            message.Alignment = Element.ALIGN_CENTER;
            var total = new Paragraph(Number + "/" + Amount, endingMessageFont);
            total.Alignment = Element.ALIGN_RIGHT;

            //Add the paragraphs to the document
            document.Add(date);
            document.Add(message);
            document.Add(total);

            //close the document
            document.Close();
        }

        private void CreatePopTicket(int Number, int Amount, int id, DateTime today) {
            //List<StringBuilder> Tickets = new List<StringBuilder>();
            //Create the document
            var document = new Document(PageSize.A7.Rotate());

            //set the output and the writer
            string path = HttpContext.Current.Server.MapPath("~/PDFs/");
            var output = new FileStream(path + id + "_PopTicket_" + Number + ".pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(document, output);

            //open the document
            document.Open();

            //Set the font
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 8, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            //Create the elements
            var date = new Paragraph(today.ToString("d-M-yyyy", new CultureInfo("nl-NL")));
            date.Alignment = Element.ALIGN_RIGHT;
            var message = new Paragraph("Met dit kaartje heeft u recht op één frisdrank!", titleFont);
            message.Alignment = Element.ALIGN_CENTER;
            var total = new Paragraph(Number + "/" + Amount, endingMessageFont);
            message.Alignment = Element.ALIGN_RIGHT;

            //Add the paragraphs to the document
            document.Add(date);
            document.Add(message);
            document.Add(total);

            //close the document
            document.Close();
        }
    }
}
