using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IVH7_Cinema.Domain.Entities;

namespace IVH7_Cinema.WebUI.Website.Models {
    public class IDealListViewModel {

        public Payment payment { get; set; }

        //ForIdeal
        [Required]
        [RegularExpression("[A-Z]{2}[0-9]{2}[A-Z]{4}[0-9]{10}", ErrorMessage = "Please enter a valid IBAN")]
        public string AccountNumber { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}", ErrorMessage = "please enter a valid passnumber")]
        public int CardNumber { get; set; }

        [Required]
        [RegularExpression("[0-9]{7}", ErrorMessage = "please enter a valid identification code")]
        public int Validation { get; set; }

        // Reservering
        public Order order { get; set; }
    }
}