using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
    public class PostsRepo
    {
        public static List<Post> List()
        {
            string query = $@"SELECT * FROM `posts` ORDER BY post_id ASC";
            var drc = Sql.Query(query);

            var result = Sql.MapAll<Post>(drc, (dre, t) =>
            {
                t.Content = dre.From<string>("content");
                t.PostId = dre.From<int>("post_id");
                t.UserId = dre.From<int>("user_id");
                t.ImageId = dre.From<int?>("image_id");
                t.PostName = dre.From<string>("post_name");
                t.CreatedAt = dre.From<DateTimeOffset>("created_at");
            });

            return result;
        }

        public static Post Find(int id)
        {
            var query = $@"SELECT * FROM `posts` WHERE post_id=?post_id";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?post_id", id);
            });

            var result = Sql.MapOne<Post>(drc, (dre, t) =>
            {
                t.PostId = dre.From<int>("post_id");
                t.Content = dre.From<string>("content");
                t.UserId = dre.From<int>("user_id");
                t.ImageId = dre.From<int?>("image_id");
                t.PostName = dre.From<string>("post_name");
                t.CreatedAt = dre.From<DateTimeOffset>("created_at");
            });

            return result;
        }

        public static void Update(Post post)
        {
            var query = $@"UPDATE `posts` 
                           SET 
                               content=?content, 
                               user_id=?user_id, 
                               image_id=?image_id, 
                               post_name=?post_name,
                               created_at=?created_at
                           WHERE 
                               post_id=?post_id";

            Sql.Update(query, args =>
            {
                args.Add("?content", post.Content);
                args.Add("?user_id", post.UserId);
                args.Add("?image_id", post.ImageId);
                args.Add("?post_name", post.PostName);
                args.Add("?created_at", post.CreatedAt);
                args.Add("?post_id", post.PostId);
            });
        }

        public static void Insert(Post post)
        {
            var query = $@"INSERT INTO `posts` (content, user_id, image_id, post_name, created_at) 
                           VALUES (?content, ?user_id, ?image_id, ?post_name, ?created_at)";

            Sql.Insert(query, args =>
            {
                args.Add("?content", post.Content);
                args.Add("?user_id", post.UserId);
                args.Add("?image_id", post.ImageId);
                args.Add("?post_name", post.PostName);
                 args.Add("?created_at", DateTimeOffset.Now); // Set to current DateTimeOffset
            });
        }
        public static void InsertWithImage(Post post, IFormFile fileInput)
        {
            // Process image upload (save to database or file system)
            byte[] imageData = null;

            if (fileInput != null && fileInput.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    fileInput.CopyTo(stream);
                    imageData = stream.ToArray();
                }
            }

            // Insert the post with image information
            var query = $@"INSERT INTO `posts`
            (
              user_id,
              content,
              image_id,
              post_name,
              created_at
            )
        VALUES
            (
              ?user_id,
              ?content,
              ?image_id,
              ?post_name,
              ?created_at
            )";

            // Execute the query
            Sql.Insert(query, args =>
            {
                args.Add("?user_id", post.UserId);
                args.Add("?content", post.Content);
                args.Add("?image_id", SaveImage(imageData, fileInput.FileName));
                args.Add("?post_name", post.PostName);
 
            });
        }


        public static object SaveImage(byte[] imageData, string imageName)
        {
            var query = @"INSERT INTO `Images` (image_data, image_name) VALUES (?image_data, ?image_name)";

            return (int)Sql.Insert(query, args =>
            {
                args.Add("?image_data", imageData);
                args.Add("?image_name", imageName);
            });
        }
        public static byte[] GetImageData(int? imageId)
        {
            if (imageId.HasValue)
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
            }

            return null; // Return null if image data is not found or ImageId is null
        }
        public static void Delete(int id)
        {
            var query = $@"DELETE FROM `posts` WHERE post_id=?post_id";
            Sql.Delete(query, args =>
            {
                args.Add("?post_id", id);
            });
        }
        
    }
}
