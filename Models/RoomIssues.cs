using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace eHotels.Models
{
    public class RoomIssues
    {
        [Key]
        [Column(Order = 0)]
        public string RoomNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Problem { get; set; }

        public string Description { get; set; }

        [BindNever]
        public virtual Rooms Room { get; set; }
    }

}

