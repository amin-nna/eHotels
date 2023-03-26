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
    public class Bookings
    {
        [Key]
        [Required]
        public int BookingID { get; set; }


        // ? To set OnDelete behavior to SET NULL 
        [ForeignKey("Customer_SSN_SIN")]
        public string? Customer { get; set; }

        [ForeignKey("RoomNumber")]
        public string? RoomNumber { get; set; }

        [ForeignKey("Employee_Booking")]
        public string? Employee { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public Boolean Active { get; set; }
    }

}

