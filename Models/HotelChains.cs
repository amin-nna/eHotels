using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace eHotels.Models
{
    public class HotelChains
    {
        [Key]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int HotelsCount { get; set; }

        [BindNever]
        public virtual ICollection<Hotels> Hotels { get; set; }

        [BindNever]
        public virtual ICollection<CentralOffices> CentralOffices { get; set; }

        public int? Rating { get; set; }
    }
}

