using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVH7_Cinema.Domain.Entities;

namespace IVH7_Cinema.WebUI.Website.Models {
    public class ShowsListViewModel {

        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Show> Shows { get; set; }
        public IEnumerable<Movie> AllMovies { get; set; }
        //public PagingInfo PagingInfo { get; set; }
    }
}