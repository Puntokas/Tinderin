namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class MessageRepo
{
	public static List<Message> List(int userId)
	{
		string query = $@"
			SELECT m1.* 
			FROM messages m1
			INNER JOIN (
				SELECT id, fk_senderid, fk_receiverid
				FROM messages
			) m2 ON m1.id = m2.id
			WHERE m1.fk_senderid = {userId}
			AND m2.fk_receiverid NOT IN (
				SELECT fk_receiverid
				FROM messages
				WHERE id < m2.id
				AND fk_senderid = {userId}
			)
			ORDER BY m1.id ASC;";

		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Message>(drc, (dre, t) => {
				t.Sender = dre.From<int>("fk_senderid");
				t.Receiver = dre.From<int>("fk_receiverid");
			});

		return result;
	}

	public static List<Message> ListChat(int userId, int partnerId)
	{
		string query = $@"
			SELECT * 
			FROM `messages`
			WHERE (fk_senderid = {userId} and fk_receiverid = {partnerId})
			OR    (fk_senderid = {partnerId} and fk_receiverid = {userId})
			ORDER BY id ASC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Message>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Sender = dre.From<int>("fk_senderid");
				t.Receiver = dre.From<int>("fk_receiverid");
				t.MessageString = dre.From<string>("message");
				//t.Date = dre.From<DateTime>("date");
				//t.Time = dre.From<DateTime>("time");
			});

		return result;
	}

	public static List<Message> FindMessages(int senderid, int receiverid, string message)
	{
		var query = $@"
			SELECT * 
			FROM `messages` 
			WHERE (
				(fk_senderid=?senderid AND fk_receiverid=?receiverid) 
				OR (fk_senderid=?receiverid AND fk_receiverid=?senderid)
			)
			AND `message` LIKE CONCAT('%', ?searchstring, '%')";
		var drc =
			Sql.Query(query, args =>
			{
				args.Add("?senderid", senderid);
				args.Add("?receiverid", receiverid);
				args.Add("?searchstring", message);
			});

		var result =
			Sql.MapAll<Message>(drc, (dre, t) =>
			{
				t.Id = dre.From<int>("id");
				t.Sender = dre.From<int>("fk_senderid");
				t.Receiver = dre.From<int>("fk_receiverid");
				t.MessageString = dre.From<string>("message");
			});

		return result;
	}

	public static void Update(int messageid, string message)
	{
		var query =
			$@"UPDATE `messages` 
			SET
	            message=?message
			WHERE 
				id=?id";

		Sql.Update(query, args =>
		{
			args.Add("?id", messageid);
			args.Add("?message", message);
		});
	}

	public static void Insert(int userid, int partnerid, string message)
	{
		var query = $@"INSERT INTO `messages`
	                   (
	                     fk_senderid,
	                     fk_receiverid,
	                     date,
	                     time,
	                     message
	                   )
	               VALUES
	                   (
	                     ?fk_senderid,
	                     ?fk_receiverid,
	                     ?date,
	                     ?time,
	                     ?message
	                   )";

		// Insert the Message information
		Sql.Insert(query, args =>
		{
			args.Add("?fk_senderid", userid);
			args.Add("?fk_receiverid", partnerid);
			args.Add("?date", DateTime.Now.ToString("yyyy-MM-dd"));
			args.Add("?time", DateTime.Now.ToString("HH:mm:ss"));
			args.Add("?message", message);
		});
	}

	public static int GetLastId()
	{
		var query = $@"SELECT * FROM `messages` WHERE id=(SELECT max(id) FROM `messages`)";
		var drc = Sql.Query(query);

		var result =
			Sql.MapOne<Message>(drc, (dre, t) =>
			{
				t.Id = dre.From<int>("id");
			});

		return result.Id;
	}

	public static void Delete(string id)
	{
		var query = $@"DELETE FROM `messages` where id=?id";
		Sql.Delete(query, args =>
		{
			args.Add("?id", id);
		});
	}
}
