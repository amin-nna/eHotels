using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace eHotels.Models
{


    public class Rooms
    {
        [Key]
        [Required]
        public string RoomID { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string Hotel_ID { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public bool Extendable { get; set; }

        public string? View { get; set; }

        [BindNever]
        public virtual Hotels Hotel { get; set; }

        [BindNever]
        public virtual ICollection<RoomIssues> RoomIssues { get; set; }

        [BindNever]
        public virtual ICollection<RoomAmenities> RoomAmenities { get; set; }

        [BindNever]
        public virtual ICollection<Bookings>? Bookings { get; set; }

        [BindNever]
        public virtual ICollection<Rentings>? Rentings { get; set; }
    }
     
}

