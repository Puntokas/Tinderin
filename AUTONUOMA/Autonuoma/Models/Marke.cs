namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class Marke
{
    [DisplayName("Pavadinimas")]
    [Required]
    public string Pavadinimas { get; set; }

    [DisplayName("Kompanija")]
    [Required]
    public string Kompanija { get; set; }

}

