namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class AutomobilisRepo
{
	public static List<Automobilis> List()
	{
		string query = $@"SELECT * FROM `automobiliai` ORDER BY vin_kodas ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Automobilis>(drc, (dre, t) =>
			{
				t.Modelis = dre.From<string>("fk_modelis");
				t.Marke = dre.From<string>("fk_marke");
				t.Vin_kodas = dre.From<string>("vin_kodas");
				t.PagaminimoData = dre.From<DateTime>("pagaminimo_data");
				t.Mase = dre.From<float>("mase");
				t.PavaruDeze = dre.From<int>("pavaru_deze");
			});

		return result;
	}

	public static Automobilis Find(string id)
	{
		var query = $@"SELECT * FROM `automobiliai` WHERE vin_kodas=?vin_kodas";
		var drc =
			Sql.Query(query, args =>
			{
				args.Add("?vin_kodas", id);
			});

		var result =
			Sql.MapOne<Automobilis>(drc, (dre, t) =>
			{
				t.Modelis = dre.From<string>("fk_modelis");
				t.Marke = dre.From<string>("fk_marke");
				t.Vin_kodas = dre.From<string>("vin_kodas");
				t.PagaminimoData = dre.From<DateTime>("pagaminimo_data");
				t.Mase = dre.From<float>("mase");
				t.PavaruDeze = dre.From<int>("pavaru_deze");
			});

		return result;
	}

	public static void Update(Automobilis automobilis)
	{
		var query =
			$@"UPDATE `automobiliai` 
			SET 
				fk_marke=?fk_marke
				fk_modelis=?fk_modelis
				vin_kodas=?vin_kodas 
				pagaminimo_data=?pagaminimo_data
				mase=?mase
				pavaru_deze=?pavaru_deze
			WHERE 
				vin_kodas=?vin_kodas";

		Sql.Update(query, args =>
		{
			args.Add("?fk_marke", automobilis.Marke);
			args.Add("?fk_modelis", automobilis.Modelis);
			args.Add("?vin_kodas", automobilis.Vin_kodas);
			args.Add("?pagaminimo_data", automobilis.PagaminimoData);
			args.Add("?mase", automobilis.Mase);
			args.Add("?pavaru_deze", automobilis.PavaruDeze);
		});
	}

	public static void Insert(Automobilis automobilis)
	{
		var query = $@"INSERT INTO `automobiliai` ( vin_kodas, pagaminimo_data, mase ) VALUES ( ?vin_kodas, ?pagaminimo_data, ?mase)";
		Sql.Insert(query, args =>
		{
			args.Add("?fk_marke", automobilis.Marke);
			args.Add("?fk_modelis", automobilis.Modelis);
			args.Add("?vin_kodas", automobilis.Vin_kodas);
			args.Add("?pagaminimo_data", automobilis.PagaminimoData);
			args.Add("?mase", automobilis.Mase);
			args.Add("?pavaru_deze", automobilis.PavaruDeze);
		});
	}

	public static void Delete(string id)
	{
		var query = $@"DELETE FROM `automobiliai` where vin_kodas=?vin_kodas";
		Sql.Delete(query, args =>
		{
			args.Add("?vin_kodas", id);
		});
	}
}

