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
    public class Booking
    {
        [Key]
        [Required]
        public int BookingID { get; set; }

        [Required]
        public int Customer_SSN_SIN { get; set; }

        [ForeignKey("Customer_SSN_SIN")]
        public string Customer { get; set; }

        public int? RoomNumber { get; set; }

        [ForeignKey("RoomNumber")]
        public Room Room { get; set; }

        public int? Employee_Booking { get; set; }

        [ForeignKey("Employee_Booking")]
        public string Employee { get; set; }
    }

}

