namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class Automobilis
{
	[DisplayName("Markė")]
	[Required]
	public string Marke { get; set; }

	[DisplayName("Modelis")]
	[Required]
	public string Modelis { get; set; }

	[DisplayName("VIN kodas")]
	[Required]
	public string Vin_kodas { get; set; }

	[DisplayName("Pagaminimo data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	[Required]
	public DateTime PagaminimoData { get; set; }

	[DisplayName("Masė")]
	[Required]
	public float Mase { get; set; }

	[DisplayName("Pavarų dėžė")]
	[Required]
	public int PavaruDeze { get; set; }
}
