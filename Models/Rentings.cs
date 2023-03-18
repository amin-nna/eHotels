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
    public class Rentings
    {
        [Key]
        [Required]
        public int RentingID { get; set; }

        [Required]
        [ForeignKey("Customer_SSN_SIN")]
        public string Customer { get; set; }

        [ForeignKey("RoomNumber")]
        [Required]
        public int RoomNumber { get; set; }

        [Required]
        [ForeignKey("Employee_Renting")]
        public string Employee { get; set; }
    }
}

