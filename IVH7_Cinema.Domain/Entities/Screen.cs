using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVH7_Cinema.Domain.Entities
{
    public class Screen
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order=0)]
        public Int64 ScreenID { get; set; }

        public Int64 ScreenNumber { get; set; }
        
        //amount of people in a screen
        public Int64 Size { get; set; }
        
        //Seats in a screen, a seat has a row and a chairnumber.
        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }

     }
}
