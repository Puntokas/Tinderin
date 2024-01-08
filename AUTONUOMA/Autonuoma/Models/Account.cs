namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class Account
{
    [DisplayName("Vardas")]
    [Required]
    public string name { get; set; }

    [DisplayName("Pavarde")]
    [Required]
    public string surname { get; set; }

    [DisplayName("Gimimo data")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [Required]
    public DateTime birth_date { get; set; }

    [DisplayName("El. paštas")]
    [Required]
    public string email { get; set; }

    [DisplayName("Adresas")]
    [Required]
    public string address { get; set; }

    [DisplayName("Slaptažodis")]
    [Required]
    public string password { get; set; }

    [DisplayName("Aprašymas")]
    [Required]
    public string description { get; set; }

    [DisplayName("Paskutinis prisijungimas")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [Required]
    public DateTime last_login { get; set; }

    [DisplayName("Profilio nuotraukos id")]
    [Required]
    public int profile_picture_id { get; set; }

    [DisplayName("Prisijungimo vardas")]
    [Required]
    public string username { get; set; }
}

public class AccountLogin
{
    [DisplayName("Prisijungimo vardas")]
    [Required]
    public string username { get; set; }

    [DisplayName("Slaptažodis")]
    [Required]
    public string password { get; set; }
}
