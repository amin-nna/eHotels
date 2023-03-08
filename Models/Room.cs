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

    public class Room
    {
        [Key]
        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int Hotel_Hotel_ID { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        [Required]
        public int Capacity { get; set; }

        public string View { get; set; }

        [Required]
        public bool Extendable { get; set; }

        public Hotel Hotel { get; set; }
    }

}

