using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IVH7_Cinema.Domain.Entities;

namespace IVH7_Cinema.WebUI.Website.Models {
    public class Payment {

        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "NameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
                          ErrorMessageResourceName = "NameLong")]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "EmailRequired")]
        [RegularExpression(".+@.+\\..+", ErrorMessageResourceType = typeof(Resources.Resources),
                                         ErrorMessageResourceName = "EmailInvalid")]
        public string Email { get; set; }

        [Display(Name = "Adress", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "AdressRequired")]
        [RegularExpression("^[a-zA-Z_ ]+$", ErrorMessageResourceType = typeof(Resources.Resources),
                                         ErrorMessageResourceName = "AdressInvalid")]
        public String Adres { get; set; }

        [Display(Name = "AdressNumber", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "NumberRequired")]
        [RegularExpression("^[0-9]+$", ErrorMessageResourceType = typeof(Resources.Resources),
                                         ErrorMessageResourceName = "NumberInvalid")]
        public Int32 Huisnummer { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "ZipCodeRequired")]
        [RegularExpression("[0-9]{4}[A-Z]{2}", ErrorMessageResourceType = typeof(Resources.Resources),
                                         ErrorMessageResourceName = "ZipCodeInvalid")]
        public String Postcode { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "CityRequired")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessageResourceType = typeof(Resources.Resources),
                                         ErrorMessageResourceName = "CityInvalid")]        
        public String Woonplaats { get; set; }

        [Display(Name = "PaymentType", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "PaymentRequired")]
        public String Betaalwijze { get; set; }

        public Order Order { get; set; }
    }
}