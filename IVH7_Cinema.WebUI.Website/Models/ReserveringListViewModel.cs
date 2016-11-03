using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVH7_Cinema.Domain.Entities;

namespace IVH7_Cinema.WebUI.Website.Models {
    public class ReserveringListViewModel {

        public Payment payment { get; set; }

        // Reservering
        public Order order { get; set; }

    }
}