using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVH7_Cinema.Domain.Entities {
    public class Show {

        [Key]
        public Int64 ShowID { get; set; }

        public Int64 MovieID { get; set; }

        public virtual Movie Movie { get; set; }

        public Int64 ScreenID { get; set; }

        public virtual Screen Screen { get; set; }

        public Int64 CinemaID { get; set; }

        public virtual Cinema Cinema { get; set; }

        public DateTime DateTime { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Int64 LanguageID { get; set; }

        public virtual Language Language { get; set; }

        public String Subtitles { get; set; }

        public Boolean Is3D { get; set; }

        public int AvailableSeats()
        {
            int amount = Screen.Seats.Count();
            if (Tickets != null)
            {
                amount = amount - Tickets.Count();
            }
            return amount;
        }
        
        public IEnumerable<Seat> AssignSeats(int requiredSeats)
        {
            if (AvailableSeats() == requiredSeats)
            {
                List<Seat> allSeats = Screen.Seats.ToList();
                if (Tickets != null)
                {
                    foreach (Ticket t in Tickets)
                    {
                        allSeats.Remove(t.Seat);
                    }
                }
                return allSeats;
            }

            else if (AvailableSeats() >= requiredSeats && requiredSeats > 0)
            {
                //if there are enough "good" spots available on a row
                if (requiredSeats < Screen.Seats.Where(x => x.RowNumber == 1).Count()-6 && GetIdealSeats(requiredSeats) != null)
                {
                    System.Diagnostics.Debug.WriteLine("Show - AssignSeats - GetIdealSeats is niet null");
                    System.Diagnostics.Debug.WriteLine("Show - AssignSeats - Return Seats");
                    System.Diagnostics.Debug.WriteLine("Show - if");
                    return GetIdealSeats(requiredSeats);
                }
                else if (requiredSeats < Screen.Seats.Where(x => x.RowNumber == 1).Count() && GetOtherSeats(requiredSeats, null) != null)
                {   //see if there are enough seats to seat all people on the same (best available) row.
                        return GetOtherSeats(requiredSeats, null);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Show - else");
                        List<Seat> Seats = new List<Seat>();
                        List<Seat> PartialSeats = new List<Seat>();
                        int ReqSeatsLeft = requiredSeats;
                        while (ReqSeatsLeft > 0)
                        {
                            if (GetOtherSeats(6, Seats) != null && ReqSeatsLeft >= 6)
                            {
                                IEnumerable<Seat> TMPSeats = GetOtherSeats(6, Seats);
                                foreach (Seat s in TMPSeats)
                                {
                                    PartialSeats.Add(s);
                                }
                                ReqSeatsLeft = ReqSeatsLeft - 6;
                            }

                            else if (GetOtherSeats(5, Seats) != null && ReqSeatsLeft >= 5)
                            {
                                IEnumerable<Seat> TMPSeats = GetOtherSeats(5, Seats);
                                foreach (Seat s in TMPSeats)
                                {
                                    PartialSeats.Add(s);
                                }
                                ReqSeatsLeft = ReqSeatsLeft - 5;
                            }

                            else if (GetOtherSeats(4, Seats) != null && ReqSeatsLeft >= 4)
                            {
                                IEnumerable<Seat> TMPSeats = GetOtherSeats(4, Seats);
                                foreach (Seat s in TMPSeats)
                                {
                                    PartialSeats.Add(s);
                                }
                                ReqSeatsLeft = ReqSeatsLeft - 4;
                            }
                            else if (GetOtherSeats(3, Seats) != null && ReqSeatsLeft >= 3)
                            {
                                IEnumerable<Seat> TMPSeats = GetOtherSeats(3, Seats);
                                foreach (Seat s in TMPSeats)
                                {
                                    PartialSeats.Add(s);
                                }
                                ReqSeatsLeft = ReqSeatsLeft - 3;
                            }
                            else if (GetOtherSeats(2, Seats) != null && ReqSeatsLeft >= 2)
                            {
                                IEnumerable<Seat> TMPSeats = GetOtherSeats(2, Seats);
                                foreach (Seat s in TMPSeats)
                                {
                                    PartialSeats.Add(s);
                                }
                                ReqSeatsLeft = ReqSeatsLeft - 2;
                            }
                            else
                            {
                                IEnumerable<Seat> TMPSeats = GetOtherSeats(1, Seats);
                                foreach (Seat s in TMPSeats)
                                {
                                    PartialSeats.Add(s);
                                }
                                ReqSeatsLeft--;
                            }
                            System.Diagnostics.Debug.WriteLine("Show - reqseatsleft = " + ReqSeatsLeft);
                            System.Diagnostics.Debug.WriteLine("Show - partialseats = " + PartialSeats.Count());
                            System.Diagnostics.Debug.WriteLine("Show - Seats = " + Seats.Count());
                            //voeg partialseats toe aan seats
                            foreach (Seat s in PartialSeats) { Seats.Add(s); }
                            PartialSeats.Clear();
                        }
                        return Seats;
                    }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Show - AssignSeats - Er zijn geen seats terug gevonden");
                return null;
            }
        }

        private IEnumerable<Seat> GetIdealSeats(int requiredSeats)
        {
            //calculate middle row and highest row.
            int MiddleRow = Convert.ToInt32(Screen.Seats.Average(r => r.RowNumber));
            //int MiddleRow = (int) Screen.Seats.Average(r => r.RowNumber);
            Int64 HighestRow = Screen.Seats.OrderByDescending(s => s.RowNumber).ElementAt(0).RowNumber;
            Int64 IdealRow = MiddleRow;

            System.Diagnostics.Debug.WriteLine("Show - GetIdealSeats - MiddleRow = " + MiddleRow);
            System.Diagnostics.Debug.WriteLine("Show - GetIdealSeats - HighestRow = " + HighestRow);
            System.Diagnostics.Debug.WriteLine("Show - GetIdealSeats - IdealRow = " + IdealRow);
            System.Diagnostics.Debug.WriteLine("Show - GetIdealSeats - RequiredSeats = " + requiredSeats);
                        
            while(GetAvailableGoodSeats(IdealRow, requiredSeats) == null){
                //if the idealrow has no seats available anymore, go up one row.
                if(IdealRow < HighestRow){
                    IdealRow++;
                }
                //if the idealrow is equal to the highest row go back to the middlerow and go down from there
                else if(IdealRow == HighestRow){
                    IdealRow = MiddleRow;
                    IdealRow--;
                }
                //go down from the middlerow as long as the row is not below 0. (that's impossible).
                else if(IdealRow < MiddleRow && IdealRow > 0){
                    IdealRow--;
                } 
                //if there is no idealrow then return null
                else {
                    System.Diagnostics.Debug.WriteLine("Show - GetIdealSeats - Er is geen ideale row");
                    return null;
                }                
            }
            //return the availablegoodseats at the ideal row.
            return GetAvailableGoodSeats(IdealRow, requiredSeats);
        }

        private IEnumerable<Seat> GetOtherSeats(int requiredSeats, List<Seat> seatsBezet)
        {
            //calculate middle row and highest row.
            int MiddleRow = (int)Math.Floor(Screen.Seats.Average(r => r.RowNumber));
            Int64 HighestRow = Screen.Seats.OrderByDescending(s => s.RowNumber).ElementAt(0).RowNumber;
            Int64 IdealRow = MiddleRow;

            while (GetAvailableOtherSeats(IdealRow, requiredSeats, seatsBezet) == null)
                {
                    //if the idealrow has no seats available anymore, go up one row.
                    if (IdealRow < HighestRow)
                    {
                        IdealRow++;
                    }
                    //if the idealrow is equal to the highest row go back to the middlerow and go down from there
                    else if (IdealRow == HighestRow)
                    {
                        IdealRow = MiddleRow;
                        IdealRow--;
                    }
                    //go down from the middlerow as long as the row is not below 0. (that's impossible).
                    else if (IdealRow < MiddleRow && IdealRow > 0)
                    {
                        IdealRow--;
                    }
                    //if there is no idealrow then return null
                    else
                    {
                        return null;
                    }
                }
                //return the availablegoodseats at the ideal row.
                return GetAvailableOtherSeats(IdealRow, requiredSeats, seatsBezet);
        }
        
        private IEnumerable<Seat> GetAvailableGoodSeats(Int64 rowNumber, int requiredSeats)
        {
            //get the total amount of seats on a row and take out the outter seats, which leaves us with the ideal seats.
            IEnumerable<Seat> TotalSeats = Screen.Seats.Where(s => s.RowNumber == rowNumber);
            //make a list of seats that are already ticketed and substract them from the totalseats
            IEnumerable<Seat> TotalOpenSeats = TotalSeats;


            if (Tickets != null)
            {
                System.Diagnostics.Debug.WriteLine("Tickets niet null");
                List<Seat> TicketedSeats = new List<Seat>();
                foreach (Ticket T in Tickets)
                {
                  TicketedSeats.Add(T.Seat);
                }
                TotalOpenSeats = TotalSeats.Except(TicketedSeats);
            }

            System.Diagnostics.Debug.WriteLine("Show - GetAvailableGoodSeats - TotalOpenSeats = " + TotalOpenSeats.Count());

            IEnumerable<Seat> HighestSeats = TotalSeats.OrderByDescending(s => s.SeatNumber).Take(3);
            IEnumerable<Seat> LowestSeats = TotalSeats.OrderBy(s => s.SeatNumber).Take(3);
            //WARNING; possible mistake, not sure if the use of .Except twice is allowed!
            IEnumerable<Seat> TotalAvailableSeats = TotalOpenSeats.Except(HighestSeats).Except(LowestSeats).OrderBy(s => s.SeatNumber);

            System.Diagnostics.Debug.WriteLine("Show - GetAvailableGoodSeats - TotalAvailableSeats = " + TotalAvailableSeats.Count());

            //for each seat check if there are enough seats available next to the seat in order to see if people can sit next to eachother.
            foreach (Seat s in TotalAvailableSeats)
            {
                Int64 HypotheticalMaxReqSeatNumber = s.SeatNumber + requiredSeats - 1;
                
                if(HypotheticalMaxReqSeatNumber <= HighestSeats.ElementAt(0).SeatNumber){
                    IEnumerable<Seat> AvailableSuitableSeats = TotalAvailableSeats.Where(x => x.SeatNumber >= s.SeatNumber && x.SeatNumber <= HypotheticalMaxReqSeatNumber && x.Vacated == false);

                    System.Diagnostics.Debug.WriteLine("Show - GetAvailableGoodSeats - AvailableSuitableSeats = " + AvailableSuitableSeats.Count());

                    if (AvailableSuitableSeats.Count() == requiredSeats)
                    {
                        return AvailableSuitableSeats;
                    }
                }
            }
            //if there is no space for the people to sit next to eachother within the ideal seats, return null.
            System.Diagnostics.Debug.WriteLine("Show - GetAvailableGoodSeats - Er is geen plek voor mensen om naast elkaar te zitten");
            return null;
        }

        private IEnumerable<Seat> GetAvailableOtherSeats(Int64 rowNumber, int requiredSeats, List<Seat> seatsBezet)
        {
            //get the total amount of seats on a row and take out the outter seats, which leaves us with the ideal seats.
            IEnumerable<Seat> TotalSeats = Screen.Seats.Where(s => s.RowNumber == rowNumber);
            //make a list of seats that are already ticketed and substract them from the totalseats
            IEnumerable<Seat> TotalOpenSeats = TotalSeats;
            if (seatsBezet != null)
            {
                TotalOpenSeats = TotalSeats.Except(seatsBezet);
            }
            if (Tickets != null)
            {
                System.Diagnostics.Debug.WriteLine("Tickets niet null");
                List<Seat> TicketedSeats = new List<Seat>();
                foreach (Ticket T in Tickets)
                {
                    TicketedSeats.Add(T.Seat);
                }
                TotalOpenSeats = TotalSeats.Except(TicketedSeats);
            }

            //for each seat check if there are enough seats available next to the seat in order to see if people can sit next to eachother.
            foreach (Seat s in TotalOpenSeats)
            {
                Int64 HypotheticalMaxReqSeatNumber = s.SeatNumber + requiredSeats - 1;
                if (HypotheticalMaxReqSeatNumber <= Screen.Seats.Where(S => S.RowNumber == rowNumber).OrderByDescending(x => x.SeatNumber).ElementAt(0).SeatNumber)
                {
                    IEnumerable<Seat> AvailableSuitableSeats = TotalOpenSeats.Where(x => x.SeatNumber >= s.SeatNumber && x.SeatNumber <= HypotheticalMaxReqSeatNumber && x.Vacated == false);
                    if (AvailableSuitableSeats.Count() == requiredSeats)
                    {
                        return AvailableSuitableSeats;
                    }
                }
            }
            //if there is no space for the people to sit next to eachother, return null.
            return null;
        }
    }
}
