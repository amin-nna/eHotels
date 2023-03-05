using System;
using Microsoft.AspNetCore.Identity;

namespace eHotels.Areas.Identity.Data;

public class ApplicationUser : IdentityUser
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SIN { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }

    //public string phoneNumber { get; set; }


}

