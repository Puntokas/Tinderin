using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class PostsController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        var posts = PostsRepo.List();
        return View(posts);
    }

    [HttpGet]
    public ActionResult Create()
    {
        var post = new Post();
        return View(post);
    }

    //[HttpPost]
    //public ActionResult Create(Post post)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        PostsRepo.Insert(post);
    //        return RedirectToAction("Index");
    //    }

    //    return View(post);
    //}
    //[HttpPost]
    //public ActionResult Create(Post post)
    //{
    //    // Set a default or hardcoded user_id for testing
    //    int hardcodedUserId = 1; // Replace with the desired user ID

    //    // Set the user_id property
    //    post.UserId = hardcodedUserId;

    //    if (ModelState.IsValid)
    //    {
    //        PostsRepo.Insert(post);
    //        return RedirectToAction("Index");
    //    }

    //    return View(post);
    //}
    private int? UploadImage(IFormFile fileInput)
    {
        if (fileInput != null && fileInput.Length > 0)
        {
            // Process the image upload
            byte[] imageData;

            using (var stream = new MemoryStream())
            {
                fileInput.CopyTo(stream);
                imageData = stream.ToArray();
            }

            // Save imageData to the database or file system, and get the ImageId
            int? imageId = (int?)PostsRepo.SaveImage(imageData, fileInput.FileName);

            return imageId;
        }

        return null; // No image uploaded
    }

    [HttpPost]
    public ActionResult Create(Post post, IFormFile fileInput)
    {
        // Set a default or hardcoded user_id for testing
        int hardcodedUserId = 1; // Replace with the desired user ID

        // Set the user_id property
        post.UserId = hardcodedUserId;

        if (ModelState.IsValid)
        {
            // Process image upload (save to database or file system)
            int? imageId = UploadImage(fileInput);

            // Set the ImageId property in the post
            post.ImageId = imageId;

            // Set the creation date to the current local time
            post.CreatedAt = DateTime.Now.ToLocalTime();

            // Insert the post
            PostsRepo.Insert(post);

            return RedirectToAction("Index");
        }

        return View(post);
    }

    public FileResult GetImage(int id)
    {
        byte[] imageData = PostsRepo.GetImageData(id);

        if (imageData != null && imageData.Length > 0)
        {
            // Determine the content type based on your image format (e.g., "image/jpeg")
            string contentType = "image/jpeg";

            // Return the image data along with the content type
            return File(imageData, contentType);
        }

        // If image data is not found, return a placeholder image or an empty image
        // You can customize this part based on your application's requirements
        return File(new byte[0], "image/jpeg");
    }
    [HttpGet]
    public ActionResult Edit(int id)
    {
        var post = PostsRepo.Find(id);
        return View(post);
    }

    [HttpPost]
    public ActionResult Edit(int id, Post post)
    {
        // Set a default or hardcoded user_id for testing
        int hardcodedUserId = 1; // Replace with the desired user ID

        // Set the user_id property
        post.UserId = hardcodedUserId;
        if (ModelState.IsValid)
        {
            PostsRepo.Update(post);
            return RedirectToAction("Index");
        }

        return View(post);
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
        var post = PostsRepo.Find(id);
        return View(post);
    }

    [HttpPost]
    public ActionResult DeleteConfirm(int id)
    {
        try
        {
            PostsRepo.Delete(id);
            return RedirectToAction("Index");
        }
        catch (MySql.Data.MySqlClient.MySqlException)
        {
            ViewData["deletionNotPermitted"] = true;
            var post = PostsRepo.Find(id);
            return View("Delete", post);
        }
    }
}
