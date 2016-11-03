using IVH7_Cinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVH7_Cinema.WebUI.Website.Models
{
    public class HomepageListViewModel
    {
        public List<Movie> SpotLightMovies { get; set; }
        public List<Movie> TopMovies { get; set; }
        public List<Movie> ComingSoonMovies { get; set; }
    }
}