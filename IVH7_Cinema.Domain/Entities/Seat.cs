using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IVH7_Cinema.Domain.Entities {
    public class Seat {

        [Key]
        public Int64 SeatID { get; set; }

        [Required(ErrorMessage = "Please enter the seatnumber")]
        public Int64 SeatNumber { get; set; }

        [Required(ErrorMessage = "Please enter the rownumber")]
        public Int64 RowNumber { get; set; }

        [Required(ErrorMessage = "Please enter the Screennummer")]
        
        public Int64 ScreenID { get; set; }
        
        public virtual Screen Screen { get; set; }

        public Boolean Vacated { get; set; }

    }
}
