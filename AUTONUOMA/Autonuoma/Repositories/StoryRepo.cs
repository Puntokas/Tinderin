namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class MarkeRepo
{
	public static List<Marke> List()
	{
		string query = $@"SELECT * FROM `markes` ORDER BY pavadinimas ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Marke>(drc, (dre, t) => {
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Kompanija = dre.From<string>("kompanija");
			});

		return result;
	}

	public static Marke Find(string id)
	{
		var query = $@"SELECT * FROM `markes` WHERE pavadinimas=?pavadinimas";
		var drc = 
			Sql.Query(query, args => {
				args.Add("?pavadinimas", id);
			});

		var result = 
			Sql.MapOne<Marke>(drc, (dre, t) => {
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Kompanija = dre.From<string>("kompanija");
			});

		return result;
	}

	public static void Update(Marke marke)
	{			
		var query = 
			$@"UPDATE `markes` 
			SET 
				pavadinimas=?pavadinimas,
				kompanija=?kompanija
			WHERE 
				pavadinimas=?pavadinimas";

		Sql.Update(query, args => {
			args.Add("?pavadinimas", marke.Pavadinimas);
			args.Add("?kompanija", marke.Kompanija);
		});							
	}

	public static void Insert(Marke marke)
	{			
		var query = $@"INSERT INTO `markes`
						( pavadinimas,
						  kompanija 
						)
					VALUES
						( ?pavadinimas,
						  ?kompanija 
						)";
		Sql.Insert(query, args =>
		{
			args.Add("?pavadinimas", marke.Pavadinimas);
			args.Add("?kompanija", marke.Kompanija);
		});
	}

	public static void Delete(string id)
	{			
		var query = $@"DELETE FROM `markes` where pavadinimas=?pavadinimas";
		Sql.Delete(query, args => {
			args.Add("?pavadinimas", id);
		});			
	}
}
