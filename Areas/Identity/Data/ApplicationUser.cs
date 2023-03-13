using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace eHotels.Areas.Identity.Data;

public class ApplicationUser : IdentityUser
{

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Please enter a valid Canadian SIN number in the format XXX-XXX-XXX.")]
    public string SIN { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    //[EnumDataType(typeof(CanadianCity), ErrorMessage = "Please enter a valid city in Canada.")]
    public string City { get; set; }

    [Required]
    public string Province { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", ErrorMessage = "Please enter a valid Canadian postal code in the format X1X 1X1.")]
    public string PostalCode { get; set; }

}

public enum CanadianCity
{
    Toronto,
    Montreal,
    Vancouver,
    Calgary,
    Edmonton,
    Ottawa,
    Quebec,
    Winnipeg,
    Hamilton
}

public enum CanadianProvince
{
    //"Ontario","Quebec","British Columbia","Alberta,Manitoba,Saskatchewan,Nova Scotia,New Brunswick,Newfoundland and Labrador,Prince Edward Island,Northwest Territories,Nunavut,Yukon
}
