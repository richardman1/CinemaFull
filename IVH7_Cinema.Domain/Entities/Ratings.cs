using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IVH7_Cinema.Domain.Entities {
    public class Rating {

        [Key]
        public Int64 RatingID { get; set; }

        public String Name { get; set; }

        public String ImageUrl { get; set; }
    }
}
