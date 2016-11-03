using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Cinema
    {
        [Key]
        public Int64 CinemaID { get; set; }

        public String Name { get; set; }

        public String Address { get; set; }

        public String ZipCode { get; set; }

        public String City { get; set; }

        public String Phone { get; set; }

        public String Email { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public virtual ICollection<Screen> Screens { get; set; }

        public virtual ICollection<Show> Shows { get; set; }
    }
}
