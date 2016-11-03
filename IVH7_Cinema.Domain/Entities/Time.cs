using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IVH7_Cinema.Domain.Entities {
    public class Time {

        [Key]public int TimeNr { get; set; }

        [Required(ErrorMessage = "Please enter the amount of hours")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Please enter the amount of minutes")]
        public int Minutes { get; set; }

    }
}
