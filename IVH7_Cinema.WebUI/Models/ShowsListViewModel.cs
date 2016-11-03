using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVH7_Cinema.Domain.Entities;

namespace IVH7_Cinema.WebUI.Models {
    public class ShowsListViewModel {
        public IEnumerable<Show> Shows { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}