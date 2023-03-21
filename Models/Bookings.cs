﻿using System;
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

        [ForeignKey("Customer_SSN_SIN")]
        public string Customer { get; set; }

        [ForeignKey("RoomNumber")]
        public int RoomNumber { get; set; }

        [ForeignKey("Employee_Booking")]
        public string Employee { get; set; }
    }

}
