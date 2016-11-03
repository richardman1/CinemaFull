using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Language
    {

        [Key]
        public Int64 LanguageID { get; set; }

        public String LanguageCode { get; set; }

        public String LanguageName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}