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
    public class HotelPhoneNumbers
    {
        [Key]
        [Required]
        public string ContactName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a valid Canadian phone number.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Hotel_Hotel_ID { get; set; }

        public virtual Hotels Hotel { get; set; }
    }

}

