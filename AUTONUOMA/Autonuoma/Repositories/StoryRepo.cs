namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class StoryRepo
{
    public static List<Story> List()
    {
        string query = $@"SELECT * FROM `Stories` ORDER BY id ASC";
        var drc = Sql.Query(query);

        var result = Sql.MapAll<Story>(drc, (dre, t) => {
            t.Id = dre.From<string>("id");
            t.User_Id = dre.From<string>("user_id");
            t.Public = dre.From<bool>("public");
            t.ImageId = dre.From<int?>("image_id"); // Make sure to include ImageId
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
    public static byte[] GetImageData(int imageId)
    {
        var query = "SELECT image_data FROM Images WHERE id = ?id";

        var rows = Sql.Query(query, args =>
        {
            args.Add("?id", imageId);
        });

        if (rows.Count > 0)
        {
            // Assuming the column name for image data is "image_data"
            var imageData = (byte[])rows[0]["image_data"];
            return imageData;
        }

        // Log a message indicating that image data is not found
        Console.WriteLine($"Image data not found for ID: {imageId}");

        return null; // Return null if image data is not found
    }


    public static Story GetStoryByImageId(int imageId)
    {
        var query = "SELECT * FROM Stories WHERE image_id = ?imageId";

        var rows = Sql.Query(query, args =>
        {
            args.Add("?imageId", imageId);
        });

        if (rows.Count > 0)
        {
            var story = new Story
            {
                Id = rows[0]["id"].ToString(),
                User_Id = rows[0]["user_id"].ToString(),
                Public = Convert.ToBoolean(rows[0]["public"]),
                ImageId = Convert.ToInt32(rows[0]["image_id"])
            };

            return story;
        }

        return null;
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

        // Insert the image information and retrieve the last inserted image_id
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

        Console.WriteLine($"Executing StoryRepo.Insert for {imageQuery}");

        // Execute the image insertion query and retrieve the last inserted image_id
        var lastInsertedImageId = Sql.Insert(imageQuery, imageArgs =>
        {
            imageArgs.Add("?image_data", imageData);
            imageArgs.Add("?image_name", imageName);
        });

        // Use the last inserted image_id in the Stories insertion query
        var storyQuery = $@"INSERT INTO `Stories`
                        (
                          user_id,
                          public,
                          image_id
                        )
                    VALUES
                        (
                          ?user_id,
                          ?public,
                          {lastInsertedImageId}
                        )";

        // Insert the story information
        Sql.Insert(storyQuery, args =>
        {
            args.Add("?user_id", Story.User_Id);
            args.Add("?public", Story.Public);
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
