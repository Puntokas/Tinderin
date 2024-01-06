namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


public class Pavaru_DezeRepo
{
	public static List<Pavaru_deze> List()
	{
		var query = $@"SELECT * FROM `pavaru_dezes` ORDER BY kodas ASC";
		var drc = Sql.Query(query);

		var result =
		  Sql.MapAll<Pavaru_deze>(drc, (dre, t) =>
		  {
			  t.Kodas = dre.From<int>("kodas");
			  t.PavaruSk = dre.From<int>("pavaru_sk");
			  t.LeistinaGalia = dre.From<int>("maks_leistina_galia");
			  t.Tipas = dre.From<string>("tipas");
		  });

		return result;
	}

	public static Pavaru_deze Find(int id)
	{
		var query = $@"SELECT * FROM `pavaru_dezes` WHERE kodas=?kodas";

		var drc =
		  Sql.Query(query, args =>
		  {
			  args.Add("?kodas", id);
		  });

		if (drc.Count > 0)
		{
			var result =
			  Sql.MapOne<Pavaru_deze>(drc, (dre, t) =>
			  {
				  t.Kodas = dre.From<int>("kodas");
				  t.PavaruSk = dre.From<int>("pavaru_sk");
				  t.LeistinaGalia = dre.From<int>("maks_leistina_galia");
				  t.Tipas = dre.From<string>("tipas");
			  });

			return result;
		}

		return null;
	}

	public static void Insert(Pavaru_deze pavarudeze)
	{
		var query =
		  $@"INSERT INTO `pavaru_dezes`
			(
				kodas,
				pavaru_sk,
				maks_leistina_galia,
				tipas
				
			)
			VALUES(
				?kodas,
				?pavaru_sk,
				?maks_leistina_galia,
				?tipas
			)";

		Sql.Insert(query, args =>
		{
			args.Add("?kodas", pavarudeze.Kodas);
			args.Add("?pavaru_sk", pavarudeze.PavaruSk);
			args.Add("?maks_leistina_galia", pavarudeze.LeistinaGalia);
			args.Add("?tipas", pavarudeze.Tipas);
		});
	}

	public static void Update(Pavaru_deze pavarudeze)
	{
		var query =
		  $@"UPDATE `pavaru_dezes`
			SET
				kodas=?kodas,
				pavaru_sk=?pavaru_sk,
				maks_leistina_galia=?maks_leistina_galia,
				tipas=?tipas
			WHERE
				kodas=?kodas";

		Sql.Update(query, args =>
		{
			args.Add("?kodas", pavarudeze.Kodas);
			args.Add("?pavaru_sk", pavarudeze.PavaruSk);
			args.Add("?maks_leistina_galia", pavarudeze.LeistinaGalia);
			args.Add("?tipas", pavarudeze.Tipas);
		});
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `pavaru_dezes` WHERE kodas=?kodas";
		Sql.Delete(query, args =>
		{
			args.Add("?kodas", id);
		});
	}
}
