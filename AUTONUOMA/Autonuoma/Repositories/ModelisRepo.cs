namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Newtonsoft.Json;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis;


/// <summary>
/// Database operations related to 'Modelis' entity.
/// </summary>
public class ModelisRepo
{
	public static List<ModelisL> ListModeliai()
	{
		var query =
		  $@"SELECT
				p.pavadinimas,
				p.isleidimo_metai,
				p.fk_marke
			FROM
				`modeliai` p

			ORDER BY p.pavadinimas ASC";

		var drc = Sql.Query(query);

		var result =
		  Sql.MapAll<ModelisL>(drc, (dre, t) =>
		  {
			  t.Pavadinimas = dre.From<string>("pavadinimas");
			  t.IsleidimoMetai = dre.From<int>("isleidimo_metai");

		  });

		return result;
	}

	public static ModelisCE FindModeliaiCE(string id)
	{
		var query = $@"SELECT * FROM `modeliai` WHERE pavadinimas=?pavadinimas";
		var drc =
		  Sql.Query(query, args =>
		  {
			  args.Add("?pavadinimas", id);
		  });

		var result =
		  Sql.MapOne<ModelisCE>(drc, (dre, t) =>
		  {
			  //make a shortcut
			  var prod = t.Modelis;

			  prod.Pavadinimas = dre.From<string>("pavadinimas");
			  prod.IsleidimoMetai = dre.From<int>("isleidimo_metai");
			  prod.Marke = dre.From<string>("fk_marke");
			  
		  });

		return result;
	}
	public static Automobilis FindAutomobilis(string id)
	{
		var query = $@"SELECT * FROM `automobiliai` WHERE vin_kodas=?vin_kodas";
		var drc =
		  Sql.Query(query, args =>
		  {
			  args.Add("?vin_kodas", id);
		  });

		var result =
		  Sql.MapOne<Automobilis>(drc, (dre, prod) =>
		  {
			  //make a shortcut
			  prod.Vin_kodas = dre.From<string>("vin_kodas");
			  prod.PagaminimoData = dre.From<DateTime>("pagaminimo_data");
			  prod.Mase = dre.From<float>("mase");
			  prod.PavaruDeze = dre.From<int>("fk_pavaru_deze");


		  });

		return result;
	}

	public static string InsertModelis(ModelisCE modelCE)
	{
		var query =
		  $@"INSERT INTO `modeliai`
			(
				`pavadinimas`,
				`isleidimo_metai`,
				`fk_marke`
			)
			VALUES(
				?pavadinimas,
				?isleidimo_metai,
				?fk_marke
			)";
		
		var id =
		  Sql.Insert(query, args =>
		  {
			  //make a shortcut
			  var prod = modelCE.Modelis;

			  //
			  args.Add("?pavadinimas", prod.Pavadinimas);
			  args.Add("?isleidimo_metai", prod.IsleidimoMetai);
			  args.Add("?fk_marke", prod.Marke);
		  });
		//return (int)id;
		return modelCE.Modelis.Pavadinimas;
	}

	public static void UpdateModelis(ModelisCE modelCE)
	{
		var query =
		  $@"UPDATE `modeliai`
			SET

				`pavadinimas` = ?pavadinimas,
				`isleidimo_metai` = ?isleidimo_metai,
				`fk_marke`=?fk_marke

			WHERE pavadinimas=?pavadinimas";

		Sql.Update(query, args =>
		{
			//make a shortcut
			var prod = modelCE.Modelis;

			args.Add("?pavadinimas", prod.Pavadinimas);
			args.Add("?isleidimo_metai", prod.IsleidimoMetai);
			args.Add("?fk_marke", prod.Marke);
		});
	}

	public static void DeleteModelis(string id)
	{
		var query = $@"DELETE FROM `modeliai` where pavadinimas=?pavadinimas";
		Sql.Delete(query, args =>
		{
			args.Add("?pavadinimas", id);
		});
	}


	public static List<ModelisCE.AutomobilisM> ListAutomobiliai(string modelId)
	{
		var query =
		  $@"SELECT *
			FROM `automobiliai`
			WHERE fk_modelis  = ?fk_modelisId
			ORDER BY vin_kodas ASC";

		var drc =
		  Sql.Query(query, args =>
		  {
			  args.Add("?fk_modelisId", modelId);
		  });

		var result =
		  Sql.MapAll<ModelisCE.AutomobilisM>(drc, (dre, t) =>
		  {
			  t.Vin_kodas = dre.From<string>("vin_kodas");
			  double maseDouble;
			  if (Double.TryParse(dre.From<string>("mase"), out maseDouble))
			  {
				  t.Mase = (float)maseDouble;
			  }
			  else
			  {
				  // handle invalid value
			  }
			  t.PagaminimoData = dre.From<DateTime>("pagaminimo_data");
			  t.PavaruDeze = dre.From<int>("fk_pavaru_deze");
			  t.Marke = dre.From<string>("fk_marke");
		  });

		for (int i = 0; i < result.Count; i++)
			result[i].InListId = i;

		return result;
	}

	public static void InsertAutomobiliai(string modelId, ModelisCE.AutomobilisM up)
	{
		var query =
		  $@"INSERT INTO `automobiliai`
				(
					vin_kodas,
					pagaminimo_data,
					mase,
					fk_marke,
					fk_modelis,
					fk_pavaru_deze 
				)
				VALUES(
					?vin_kodas,
					?pagaminimo_data,
					?mase,
					?fk_marke,
					?fk_modelis,
					?fk_pavaru_deze
				)";

		Sql.Insert(query, args =>
		{
			args.Add("?vin_kodas", up.Vin_kodas);
			args.Add("?pagaminimo_data", up.PagaminimoData);
			args.Add("?mase", up.Mase);
			args.Add("?fk_marke", up.Marke);
			args.Add("?fk_modelis", modelId);
			args.Add("?fk_pavaru_deze", up.PavaruDeze);
		});
	}

	public static void DeleteAutomobiliaiModelio(string product)
	{
		var query =
		  $@"DELETE FROM a
			USING `automobiliai` as a
			WHERE a.fk_modelis =?fk_modelis";

		Sql.Delete(query, args =>
		{
			args.Add("?fk_modelis", product);
		});
	}
}