using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
    public class CommentsRepo
    {
        public static List<Comment> GetCommentsForPost(int postId)
        {
            string query = $@"SELECT * FROM `comments` WHERE post_id = ?post_id";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?post_id", postId);
            });

            var result = Sql.MapAll<Comment>(drc, (dre, t) =>
            {
                t.CommentId = dre.From<int>("CommentId");
                t.Content = dre.From<string>("Content");
                t.PostId = dre.From<int>("post_id");
                t.UserId = dre.From<int>("user_id");
                t.CreatedAt = dre.From<DateTimeOffset>("CreatedAt");
            });

            return result;
        }

        public static void AddComment(Comment comment)
        {
            var query = @"INSERT INTO comments (CommentId, post_id, user_id, Content, CreatedAt) 
                  VALUES (?CommentId, ?post_id, 1, ?content, ?created_at)";

            Sql.Insert(query, args =>
            {
                args.Add("?CommentId", comment.CommentId);
                args.Add("?post_id", comment.PostId);
                args.Add("?content", comment.Content);
                args.Add("?created_at", comment.CreatedAt);
            });
        }



    }
}
