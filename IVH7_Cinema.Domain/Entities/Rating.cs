using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVH7_Cinema.Domain.Entities {
    public class Rating {

        [Key]
        public Int64 RatingID { get; set; }

        public String Name { get; set; }

        public String ImageUrl { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
