namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class AccountRepo
{
    public static List<Account> List()
    {
        string query = $@"SELECT * FROM `accounts`";
        var drc = Sql.Query(query);

        var result =
            Sql.MapAll<Account>(drc, (dre, t) =>
            {
                t.name = dre.From<string>("name");
                t.surname = dre.From<string>("surname");
                t.birth_date = dre.From<DateTime>("birth_date");
                t.email = dre.From<string>("email");
                t.address = dre.From<string>("address");
                t.password = dre.From<string>("password");
                t.description = dre.From<string>("description");
                t.last_login = dre.From<DateTime>("last_login");
                t.profile_picture_id = dre.From<int>("picture_id");
                t.username = dre.From<string>("username");
            });

        return result;
    }

    public static Account Find(string username)
    {
        var query = $@"SELECT * FROM `accounts` WHERE username=?username";
        var drc =
            Sql.Query(query, args =>
            {
                args.Add("?username", username);
            });

        Console.WriteLine(drc);

        if (drc.Count == 0)
            return null;

        var result =
            Sql.MapOne<Account>(drc, (dre, t) =>
            {
                t.name = dre.From<string>("name");
                t.surname = dre.From<string>("surname");
                t.birth_date = dre.From<DateTime>("birth_date");
                t.email = dre.From<string>("email");
                t.address = dre.From<string>("address");
                t.password = dre.From<string>("password");
                t.description = dre.From<string>("description");
                t.last_login = dre.From<DateTime>("last_login");
                t.profile_picture_id = dre.From<int>("profile_picture_id");
                t.username = dre.From<string>("username");
            });

        return result;
    }

    public static void Update(Account account)
    {
        var query =
            $@"UPDATE `accounts` 
			SET 
				name=?name,
				surname=?surname,
				birth_date=?birth_date, 
				email=?email,
				address=?address,
                description=?description
			WHERE 
				username=?username";

        Sql.Update(query, args =>
        {
            args.Add("?name", account.name);
            args.Add("?surname", account.surname);
            args.Add("?birth_date", account.birth_date);
            args.Add("?email", account.email);
            args.Add("?address", account.address);
            args.Add("?description", account.description);
            args.Add("?username", account.username);
        });
    }

    public static void Insert(Account account)
    {
        var query = $@"INSERT INTO `accounts` ( name, surname, birth_date, email, address, password, description, last_login, profile_picture_id, username ) VALUES ( ?name, ?surname, ?birth_date, ?email, ?address, ?password, ?description, ?last_login, ?picture_id, ?username)";
        Sql.Insert(query, args =>
        {
            args.Add("?name", account.name);
            args.Add("?surname", account.surname);
            args.Add("?birth_date", account.birth_date);
            args.Add("?email", account.email);
            args.Add("?address", account.address);
            args.Add("?password", account.password);
            args.Add("?description", account.description);
            args.Add("?last_login", account.last_login);
            args.Add("?picture_id", account.profile_picture_id);
            args.Add("?username", account.username);
        });
    }

    public static void Delete(string id)
    {
        var query = $@"DELETE FROM `accounts` where username=?username";
        Sql.Delete(query, args =>
        {
            args.Add("?username", id);
        });
    }
}
