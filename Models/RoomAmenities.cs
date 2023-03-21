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
    public class RoomAmenities
    {
        [Key]
        [Column(Order = 0)]
        public int RoomNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Amenity { get; set; }

        public string Description { get; set; }

        public virtual Rooms Room { get; set; }

    }

}

