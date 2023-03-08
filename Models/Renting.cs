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
    public class Renting
    {
        [Key]
        [Required]
        public int RentingID { get; set; }

        [Required]
        public int Customer_SSN_SIN { get; set; }

        [ForeignKey("Customer_SSN_SIN")]
        public string Customer { get; set; }

        [Required]
        public int? RoomNumber { get; set; }

        [ForeignKey("RoomNumber")]
        public Room Room { get; set; }

        [Required]
        public int? Employee_Renting { get; set; }

        [ForeignKey("Employee_Renting")]
        public string Employee { get; set; }
    }
}

