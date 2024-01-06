namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using ModelisReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.ModelisReport;


/// <summary>
/// Database operations related to reports.
/// </summary>
public class AtaskaitaRepo
{
	
	public static List<ModelisReport.Modelis> GetModeliai(DateTime? dateFrom, string? DezesTipasFiltr, string? MarkeFiltr)
	{
		var query =
			$@"SELECT ALL
					UPPER(modeliai.pavadinimas) AS modeliopavadinimas,
					markes.pavadinimas AS markespavadinimas,
					modeliai.isleidimo_metai AS isleidimometai,
					UPPER(automobiliai.vin_kodas) AS vinkodas,
					pavaru_dezes.tipas AS dezestipas,
					MIN(automobiliai.pagaminimo_data) AS minpagaminimodata
				FROM
					modeliai
					LEFT JOIN markes ON modeliai.fk_marke = markes.pavadinimas
					LEFT JOIN automobiliai ON modeliai.fk_marke = automobiliai.fk_marke AND modeliai.pavadinimas = automobiliai.fk_modelis
					LEFT JOIN pavaru_dezes ON automobiliai.fk_pavaru_deze = pavaru_dezes.kodas
				GROUP BY
					modeliai.pavadinimas,
					modeliai.isleidimo_metai,
					markes.pavadinimas,
					automobiliai.vin_kodas,
					pavaru_dezes.tipas
				HAVING
					pavaru_dezes.tipas = IFNULL(?dezestipas, pavaru_dezes.tipas)
					AND markes.pavadinimas = IFNULL(?marke, markes.pavadinimas)
					AND MIN(automobiliai.pagaminimo_data) >= IFNULL(?nuo, MIN(automobiliai.pagaminimo_data))
				ORDER BY
					markes.pavadinimas ASC, modeliai.pavadinimas ASC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?nuo", dateFrom);
				args.Add("?marke", MarkeFiltr);
				args.Add("?dezestipas", DezesTipasFiltr);
			});

		var result = 
			Sql.MapAll<ModelisReport.Modelis>(drc, (dre, t) => {
				t.ModelioPav = dre.From<string>("modeliopavadinimas");
				t.MarkesPav = dre.From<string>("markespavadinimas");
				t.ModelisMetai = dre.From<int>("isleidimometai");
				t.DezesTipas = dre.From<string>("dezestipas");
				t.VinKodas = dre.From<string>("vinkodas");
				t.PagaminimoData = dre.From<string>("minpagaminimodata");
			});

		return result;
	}
	public static List<ModelisReport.Deze> ListDezes()
	{
		var query = $@"SELECT * FROM `pavaru_dezes`";

		var drc = Sql.Query(query);

		var results = Sql.MapAll<ModelisReport.Deze>(drc, (dre, bus) => {
			bus.Id = dre.From<int>("kodas");
			bus.Tipas = dre.From<string>("tipas");
		});

		return results;
	}
}
