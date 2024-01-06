namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class StoryRepo
{
	public static List<Story> List()
	{
		string query = $@"SELECT * FROM `Stories` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Story>(drc, (dre, t) => {
				t.Id = dre.From<string>("id");
				t.User_Id = dre.From<string>("user_id");
				t.Public = dre.From<bool>("public");
			});

		return result;
	}

	public static Story Find(string id)
	{
		var query = $@"SELECT * FROM `stories` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<Story>(drc, (dre, t) => {
				t.Id = dre.From<string>("id");
				t.User_Id = dre.From<string>("user_id");
				t.Public = dre.From<bool>("public");
			});

		return result;
	}

	public static void Update(Story Story)
	{			
		var query = 
			$@"UPDATE `Stories` 
			SET
				user_id=?user_id,
				public=?public
			WHERE 
				id=?id";

		Sql.Update(query, args => {
			args.Add("?id", Story.Id);
			args.Add("?user_id", Story.User_Id);
			args.Add("?public", Story.Public);
		});							
	}

    public static void Insert(Story Story, byte[] imageData, string imageName)
    {
        Console.WriteLine($"Insert method called with Story.User_Id: {Story.User_Id}, Story.Public: {Story.Public}");

        var query = $@"INSERT INTO `Stories`
                    (
                      user_id,
                      public,
                      image_id
                    )
                VALUES
                    (
                      ?user_id,
                      ?public,
                      ?image_id
                    )";

        // Insert the story information
        Sql.Insert(query, args =>
        {
            args.Add("?user_id", Story.User_Id);
            args.Add("?public", Story.Public);
            args.Add("?image_id", Story.ImageId); // Assuming you have a property ImageId in your Story model
        });

        // Insert the image information
        var imageQuery = $@"INSERT INTO `Images`
                        (
                          image_data,
                          image_name
                        )
                    VALUES
                        (
                          ?image_data,
                          ?image_name
                        )";

        Sql.Insert(imageQuery, imageArgs =>
        {
            imageArgs.Add("?image_data", imageData);
            imageArgs.Add("?image_name", imageName);
        });
    }


    public static void Delete(string id)
	{			
		var query = $@"DELETE FROM `Stories` where id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});			
	}
}
