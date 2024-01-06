namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Controller for working with 'Pavaru_deze' entity.
/// </summary>
public class Pavaru_DezeController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(Pavaru_DezeRepo.List());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var file = new Pavaru_deze();
		return View(file);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="file">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(Pavaru_deze file)
	{
		//do not allow creation of entity with 'asmensKodas' field matching existing one
		var match = Pavaru_DezeRepo.Find(file.Kodas);

		if (match != null)
			ModelState.AddModelError("id_Pavaru_deze", "Field value already exists in database.");

		//form field validation passed?
		if (ModelState.IsValid)
		{
			Pavaru_DezeRepo.Insert(file);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		return View(file);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		return View(Pavaru_DezeRepo.Find(id));
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="file">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(string id, Pavaru_deze file)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			Pavaru_DezeRepo.Update(file);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		return View(file);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var deze = Pavaru_DezeRepo.Find(id);
		return View(deze);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try
		{
			Pavaru_DezeRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch (MySql.Data.MySqlClient.MySqlException)
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var file = Pavaru_DezeRepo.Find(id);
			return View("Delete", file);
		}
	}
}

