namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats;



/// <summary>
/// Controller for working with 'Story' entity.
/// </summary>
public class StoryController : Controller
{
    /// <summary>
    /// This is invoked when either 'Index' action is requested or no action is provided.
    /// </summary>
    /// <returns>Entity list view.</returns>
    [HttpGet]
    public ActionResult Index()
    {
        var stories = StoryRepo.List();
        return View(stories);
    }

    [HttpGet]
    public IActionResult GetImage(int id)
    {
        Console.WriteLine("Get " + id);
        var imageData = StoryRepo.GetImageData(id);

        if (imageData == null)
        {
            Console.WriteLine($"Image data not found for ID: {id}");
            return File("~/path/to/default-image.jpg", "image/jpeg");
        }

        Console.WriteLine($"Image data retrieved for ID: {id}");
        return File(imageData, "image/jpeg");
    }

    [HttpGet]
    public IActionResult GetStoryByImageId(int id)
    {
        var story = StoryRepo.GetStoryByImageId(id);

        if (story == null)
        {
            return NotFound();
        }

        return Json(story);
    }

    /// <summary>
    /// This is invoked when creation form is first opened in browser.
    /// </summary>
    /// <returns>Creation form view.</returns>
    [HttpGet]
    public ActionResult Create()
    {
        var Story = new Story();
        return View(Story);
    }

    /// <summary>
    /// This is invoked when buttons are pressed in the creation form.
    /// </summary>
    /// <param name="Story">Entity model filled with latest data.</param>
    /// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Story story, IFormFile ImageFile, string textToAdd)
    {
        if (ModelState.IsValid)
        {
            byte[] imageData = null;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Read the uploaded image into a MemoryStream
                using (var imageStream = new MemoryStream())
                {
                    ImageFile.CopyTo(imageStream);

                    // Resize the image using SixLabors.ImageSharp
                    var resizedImageData = ResizeImage(imageStream.ToArray(), 800, 600); // Adjust width and height as needed

                    // Create a new Bitmap from the resized image data
                    using (var editedImage = System.Drawing.Image.FromStream(new MemoryStream(resizedImageData)))
                    using (var graphics = System.Drawing.Graphics.FromImage(editedImage))
                    {
                        // Set text color based on the selected color
                        var textColor = System.Drawing.Color.FromName(story.SelectedTextColor);

                        // Center the text horizontally and vertically
                        var format = new StringFormat();
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;

                        // Add text to the image
                        using (var font = new System.Drawing.Font("Arial", 30))
                        using (var brush = new System.Drawing.SolidBrush(textColor))
                        {
                            graphics.DrawString(textToAdd, font, brush, new System.Drawing.RectangleF(0, 0, editedImage.Width, editedImage.Height), format);
                        }

                        // Save the edited image to a MemoryStream
                        using (var editedStream = new System.IO.MemoryStream())
                        {
                            editedImage.Save(editedStream, System.Drawing.Imaging.ImageFormat.Jpeg); // Adjust the format as needed
                            imageData = editedStream.ToArray();
                        }
                    }
                }
            }

            StoryRepo.Insert(story, imageData, ImageFile?.FileName, textToAdd);

            return RedirectToAction("Index");
        }

        return View(story);
    }


    private byte[] ResizeImage(byte[] imageData, int maxWidth, int maxHeight)
    {
        using (var imageStream = new MemoryStream(imageData))
        {
            using (var image = SixLabors.ImageSharp.Image.Load(imageStream))
            {
                image.Mutate(x => x
                    .Resize(new ResizeOptions
                    {
                        Size = new SixLabors.ImageSharp.Size(maxWidth, maxHeight),
                        Mode = ResizeMode.Max
                    }));

                using (var resizedStream = new MemoryStream())
                {
                    image.Save(resizedStream, new JpegEncoder());

                    return resizedStream.ToArray();
                }
            }
        }
    }


    /// <summary>
    /// This is invoked when editing form is first opened in browser.
    /// </summary>
    /// <param name="id">ID of the entity to edit.</param>
    /// <returns>Editing form view.</returns>
    [HttpGet]
	public ActionResult Edit(string id)
	{
		var Story = StoryRepo.Find(id);
		return View(Story);
	}

    /// <summary>
    /// This is invoked when buttons are pressed in the editing form.
    /// </summary>
    /// <param name="id">ID of the entity being edited</param>		
    /// <param name="Story">Entity model filled with latest data.</param>
    /// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
    [HttpPost]
    public ActionResult Edit(string id, Story updatedStory)
    {
        // Get the existing story from the database
        var existingStory = StoryRepo.Find(id);

        // Check if the existingStory is found
        if (existingStory == null)
        {
            return NotFound();
        }


        existingStory.Public = updatedStory.Public;

        // Update the existing story in the database
        StoryRepo.Update(existingStory);

        // Redirect to the Index action or another appropriate action
        return RedirectToAction("Index");
    }


    /// </summary>
    /// <param name="id">ID of the entity to delete.</param>
    /// <returns>Deletion form view.</returns>
    [HttpGet]
	public ActionResult Delete(string id)
	{
		var Story = StoryRepo.Find(id);
		return View(Story);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(string id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try 
		{
			StoryRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var Story = StoryRepo.Find(id);
			return View("Delete", Story);
		}
	}
}
