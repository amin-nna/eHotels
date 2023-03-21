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
    public class Hotels
    {
        [Key]
        [Required]
        public string Hotel_ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Hotel_chainName_ID { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z]\s?\d[A-Za-z]\d$", ErrorMessage = "Postal code must be a valid Canadian postal code.")]
        public string PostalCode { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int RoomsCount { get; set; }

        [BindNever]
        public virtual HotelChains HotelChain { get; set; }

        [BindNever]
        public virtual ICollection<Rooms> Rooms { get; set; }

        [BindNever]
        public virtual ICollection<HotelPhoneNumbers> HotelPhoneNumbers { get; set; }
    }



}

