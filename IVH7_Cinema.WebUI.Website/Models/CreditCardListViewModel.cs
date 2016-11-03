using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IVH7_Cinema.Domain.Entities;

namespace IVH7_Cinema.WebUI.Website.Models {
    public class CreditCardListViewModel {

        public Payment payment { get; set; }

        //ForCreditCard
        [Required]
        public Int64 CreditCardNumber { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}", ErrorMessage = "please enter a valid CVC")]
        public int CVC { get; set; }

        [Required]
        [RegularExpression("[0-9]{2}", ErrorMessage = "please enter a valid month")]
        public int ExpirationMonth { get; set; }

        [Required]
        [RegularExpression("[0-9]{2}", ErrorMessage = "please enter a valid year")]
        public int ExpirationYear { get; set; }

        // Reservering
        public Order order { get; set; }

    }
}