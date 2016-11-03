using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVH7_Cinema.Domain.Entities {
    public class Ticket {
        
        [Key]public Int64 TicketID { get; set; }

        public Int64 ShowID { get; set; }

        public virtual Show Show { get; set; }

        public Int64 TariffID { get; set; }

        public virtual Tariff Tariff { get; set; }

        public Int64 SeatID { get; set; }

        public virtual Seat Seat { get; set; }

        public Int64 OrderID { get; set; }

        public virtual Order Order { get; set; }
    }
}