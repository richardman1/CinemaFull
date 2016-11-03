using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
namespace IVH7_Cinema.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Genre
    {
        [Key]
        public Int64 GenreID { get; set; }

        public String Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

    }
}
