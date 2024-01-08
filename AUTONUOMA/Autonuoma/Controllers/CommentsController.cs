using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class CommentsController : Controller
{
    [HttpGet]
    public ActionResult GetComments(int postId)
    {
        var comments = CommentsRepo.GetCommentsForPost(postId);
        return View(comments);
    }

    [HttpPost]
    public ActionResult AddComment(Comment comment)
    {
        CommentsRepo.AddComment(comment);
        return RedirectToAction("GetComments", new { postId = comment.PostId });
    }
}