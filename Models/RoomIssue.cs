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
    public class RoomIssue
    {
        [Key]
        [Column(Order = 0)]
        public int RoomNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Problem { get; set; }

        [ForeignKey("RoomNumber")]
        public Room Room { get; set; }
    }

}

