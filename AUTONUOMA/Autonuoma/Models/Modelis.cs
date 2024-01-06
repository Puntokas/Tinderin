namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis.ModelisCE;


/// <summary>
/// 'Modelis' in list form.
/// </summary>
public class ModelisL
{
	[DisplayName("Pavadinimas")]
	[Required]
	public string Pavadinimas { get; set; }

	[DisplayName("Išleidimo metai")]
	[Required]
	public int IsleidimoMetai { get; set; }
}


/// <summary>
/// 'Modelis' in create and edit forms.
/// </summary>
public class ModelisCE
{
	/// <summary>
	/// Entity data.
	/// </summary>
	public class ModelisM
	{
		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }

		[DisplayName("Išleidimo metai")]
		[Required]
		public int IsleidimoMetai { get; set; }

		[DisplayName("Markė")]
		[Required]
		public string Marke { get; set; }

	}

	/// <summary>
	/// Representation of 'Automobilis' entity in 'Modelis' edit form.
	/// </summary>
	public class AutomobilisM
	{
		public int InListId { get; set; }

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

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{

		public IList<SelectListItem> Marke { get; set; }
		public IList<SelectListItem> PavaruDeze { get; set; }
	}


	/// <summary>
	/// Product.
	/// </summary>
	public ModelisM Modelis { get; set; } = new ModelisM();


	/// <summary>
	/// Related 'Review' records.
	/// </summary>
	public IList<AutomobilisM> Automobiliai { get; set; } = new List<AutomobilisM>();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}
