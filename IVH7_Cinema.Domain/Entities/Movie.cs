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
    public class Movie
    {
        [Key]
        public Int64 MovieID { get; set; }

        //[Required(ErrorMessage = "Vul de titel van de film in")]
        public String Title { get; set; }

        public String Description { get; set; }

        public String DescriptionEN { get; set; }

        public String DescriptionFR { get; set; }

        public String Director { get; set; }

        public String ImdbURL { get; set; }

        public String ImdbRating { get; set; }

        public String TrailerURL { get; set; }

        public String ImageURL { get; set; }

        public String BannerURL { get; set; }

        //[Required(ErrorMessage = "Vul de duur van de film in")]
        public Int64 Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        //[Required(ErrorMessage = "Vul het genre van de film in")]
        public virtual ICollection<Genre> Genres { get; set; }

        //[Required(ErrorMessage = "Geef aan of de film in 3D beschikbaar is")]
        public Boolean Is3DAvailable { get; set; }

        //[Required(ErrorMessage = "Geef de beschikbare talen van de film aan")]
        public virtual ICollection<Language> Languages { get; set; }

        //[Required(ErrorMessage = "Geef de bijbehorende kijkwijzer icoontjes van de film aan.")]
        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }
    }
}
