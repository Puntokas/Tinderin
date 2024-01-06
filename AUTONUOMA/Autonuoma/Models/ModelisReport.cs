namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.ModelisReport;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// View model for single contract in a report.
/// </summary>
public class Modelis
{
	[DisplayName("Modelis")]
	public string ModelioPav { get; set; }

	[DisplayName("Markė")]
	public string MarkesPav { get; set; }


	[DisplayName("Išleidimo metai")]
	public int ModelisMetai { get; set; }

	[DisplayName("VIN")]
	public string VinKodas { get; set; }

	[DisplayName("Dėžės tipas")]
	public string DezesTipas { get; set; }

	[DisplayName("Pagaminimo data")]
	public string PagaminimoData { get; set; }

	public int AutomobiliuKiekis { get; set; }
	
}

/// <summary>
/// View model for whole report.
/// </summary>
public class Report
{
	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateFrom { get; set; }

	public string MarkeFiltr { get; set; }

	public string DezesTipasFiltr { get; set; }	

	public List<Modelis> Modeliai { get; set; }

	public List<SelectListItem> Dezes { get; set; }

	public int ModeliuKiekis { get; set; }

	public int VisoAutomobiliuKiekis { get; set; }
}

public class Deze
{
	public int Id { get; set; }
	public string Tipas { get; set; }
}
