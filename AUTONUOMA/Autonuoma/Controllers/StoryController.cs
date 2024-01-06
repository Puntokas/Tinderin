namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;



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
		var Storys = StoryRepo.List();
		return View(Storys);
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
    public ActionResult Create(Story story, IFormFile ImageFile)
    {
        if (ModelState.IsValid)
        {
            byte[] imageData = null;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    ImageFile.CopyTo(stream);
                    imageData = stream.ToArray();
                }
            }

            StoryRepo.Insert(story, imageData, ImageFile?.FileName);

            return RedirectToAction("Index");
        }

        return View(story);
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
	public ActionResult Edit(string id, Story Story)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			StoryRepo.Update(Story);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		return View(Story);
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
