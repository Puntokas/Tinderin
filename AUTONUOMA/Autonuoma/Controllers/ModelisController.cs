namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis;
using System.Security.Policy;


/// <summary>
/// Controller for working with 'Modelis' entity. Implementation of F2 version.
/// </summary>
public class ModelisController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(ModelisRepo.ListModeliai());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var modelCE = new ModelisCE();

		//modelCE.Parduotuve.fk_Fileid_File = 1;

		PopulateLists(modelCE);

		return View(modelCE);
	}


	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="modelCE">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirets back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(int? save, int? add, int? remove, ModelisCE modelCE)
	{
		
		//addition of new 'UzsakytosPaslaugos' record was requested?
		if (add != null)
		{
			//add entry for the new record
			var up =
			  new ModelisCE.AutomobilisM
			  {
				  InListId =
				  modelCE.Automobiliai.Count > 0 ? modelCE.Automobiliai.Max(it => it.InListId) + 1 : 0,

				  Vin_kodas = "",
				  PagaminimoData = DateTime.Parse("01/01/2000"),
				  Mase = 0,
				  Marke = "",
				  Modelis = "",
				  PavaruDeze = 1
			  };


			modelCE.Automobiliai.Add(up);
			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(modelCE);
			return View(modelCE);
		}

		if (remove != null)
		{
			//if(modelCE.Automobiliai.Count <= 1) {
			//	ViewData["0people"] = true;
			//	return View("Delete", modelCE);
			//}
			//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
			modelCE.Automobiliai =
			  modelCE
				.Automobiliai
				.Where(it => it.InListId != remove.Value)
				.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(modelCE);
			return View(modelCE);
		}

		//save of the form data was requested?
		if (save != null)
		{
			for (var i = 0; i < modelCE.Automobiliai.Count - 1; i++)
			{
				var refUp = modelCE.Automobiliai[i];

				for (var j = i + 1; j < modelCE.Automobiliai.Count; j++)
				{
					var testUp = modelCE.Automobiliai[j];

					if (testUp.Vin_kodas == refUp.Vin_kodas)
					{
						ViewData["sameID"] = true;
						ModelState.AddModelError($"Automobiliai[{j}].Vin_kodas", "Duplicate of another added service.");
					}
				}
			}


			if (modelCE.Automobiliai.Count == 0)
			{
				ViewData["emptyStore"] = true;
				ModelState.AddModelError($"Automobiliai", "Turi turėti bent vieną įrašą");
			}

			//form field validation passed?
			//if (ModelState.IsValid)
			//{
				//create new 'Sutartis'
				modelCE.Modelis.Pavadinimas = ModelisRepo.InsertModelis(modelCE);

				//create new 'UzsakytosPaslaugos' records
				foreach (var upVm in modelCE.Automobiliai) 
				{
					upVm.Marke = modelCE.Modelis.Marke;
					ModelisRepo.InsertAutomobiliai(modelCE.Modelis.Pavadinimas, upVm);

				}

			//save success, go back to the entity list
			return RedirectToAction("Index");
			//}
			//form field validation failed, go back to the form
			//else
			//{

			//	PopulateLists(modelCE);
			//	return View(modelCE);
			//}
		}

		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(string id)
	{
		var modelCE = ModelisRepo.FindModeliaiCE(id);

		modelCE.Automobiliai = ModelisRepo.ListAutomobiliai(id);
		PopulateLists(modelCE);

		return View(modelCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="modelCE">Entity view model filled with latest data.</param>
	/// <returns>Returns editing from view or redired back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(string id, int? save, int? add, int? remove, ModelisCE modelCE)
	{
		//addition of new 'UzsakytosPaslaugos' record was requested?
		if (add != null)
		{
			//add entry for the new record
			var up =
			  new ModelisCE.AutomobilisM
			  {
				  InListId =
				  modelCE.Automobiliai.Count > 0 ?
				  modelCE.Automobiliai.Max(it => it.InListId) + 1 :
				  0,

				  Vin_kodas = "",
				  PagaminimoData = DateTime.Parse("01/01/2000"),
				  Mase = 0,
				  Marke = "",
				  Modelis = "",
				  PavaruDeze = 1
			  };

			modelCE.Automobiliai.Add(up);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();
			//go back to the form
			PopulateLists(modelCE);
			return View(modelCE);
		}

		//removal of existing 'UzsakytosPaslaugos' record was requested?
		if (remove != null)
		{
			if (modelCE.Automobiliai.Count <= 1)
			{
				ViewData["0people"] = true;
				return View("Delete", modelCE);
			}
			//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
			modelCE.Automobiliai =
			  modelCE
				.Automobiliai
				.Where(it => it.InListId != remove.Value)
				.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(modelCE);
			return View(modelCE);
		}

		//save of the form data was requested?
		if (save != null)
		{
			for (var i = 0; i < modelCE.Automobiliai.Count - 1; i++)
			{
				var refUp = modelCE.Automobiliai[i];

				for (var j = i + 1; j < modelCE.Automobiliai.Count; j++)
				{
					var testUp = modelCE.Automobiliai[j];

					if (testUp.Vin_kodas == refUp.Vin_kodas)
					{
						ViewData["sameID"] = true;
						ModelState.AddModelError($"Automobiliai[{j}].Vin_kodas", "Duplicate of another added service.");
					}
				}

			}
			//if (ModelState.IsValid)
			//{
				ModelisRepo.UpdateModelis(modelCE);
				ModelisRepo.DeleteAutomobiliaiModelio(modelCE.Modelis.Pavadinimas);
				foreach (var upVm in modelCE.Automobiliai)
				{
					upVm.Marke = modelCE.Modelis.Marke;
					ModelisRepo.InsertAutomobiliai(modelCE.Modelis.Pavadinimas, upVm);
				}

				return RedirectToAction("Index");
			//}
			//else
			//{
			//	PopulateLists(modelCE);
			//	return View(modelCE);
			//}
		}
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(string id)
	{
		var modelCE = ModelisRepo.FindModeliaiCE(id);
		return View(modelCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(string id)
	{
		//load 'Sutartis'
		var sutCE = ModelisRepo.FindModeliaiCE(id);

		//'Sutartis' is in the state where deletion is permitted?
		//if (true) //sutCE.Parduotuve.FkBusena == 1 || sutCE.Sutartis.FkBusena == 3) !!!
		{
			//delete the entity
			ModelisRepo.DeleteAutomobiliaiModelio(id);
			ModelisRepo.DeleteModelis(id);

			//redired to list form
			return RedirectToAction("Index");
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="modelCE">'Parduotuve' view model to append to.</param>
	private void PopulateLists(ModelisCE modelCE)
	{
		//load entities for the select lists
		var marke = MarkeRepo.List();
		var pavarudeze = Pavaru_DezeRepo.List();

		//build select lists
		modelCE.Lists.Marke =
		  marke
			.Select(it =>
			{
				return
			  new SelectListItem
				{
					Value = Convert.ToString(it.Pavadinimas),
					Text = $"{it.Pavadinimas}"
				};
			})
			.ToList();

		modelCE.Lists.PavaruDeze =
		  pavarudeze
			.Select(it =>
			{
				return
			  new SelectListItem
				{
					Value = Convert.ToString(it.Kodas),
					Text = $"{it.Tipas}, {it.PavaruSk} pavaros"
				};
			})
			.ToList();
	}
}

