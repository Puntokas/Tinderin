namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
public class Pavaru_deze
{
    [DisplayName("Kodas")]
    public int Kodas { get; set; }

    [DisplayName("Pavarų skaičius")]
    [Required]
    public int PavaruSk { get; set; }

    [DisplayName("Leistina galia")]
    [Required]
    public int LeistinaGalia { get; set; }

    [DisplayName("Tipas")]
    [Required]
    public string Tipas { get; set; }

}
