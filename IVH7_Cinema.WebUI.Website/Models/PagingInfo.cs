using System;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.WebUI.Website.Models
{
    [ExcludeFromCodeCoverage]
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get
            {
                return
                    (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
}