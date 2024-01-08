namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using System.Text.Json;
using System.Text.Json.Serialization;



/// <summary>
/// Controller for working with 'Message' entity.
/// </summary>
public class MessageController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		//var currentUserId = User.Identity.GetUserId();
		int id = 1;
		var Messages = MessageRepo.List(id);
		return View(Messages);
	}

	[HttpGet]
	public JsonResult IndexChat(int senderid, int partnerid)
	{
		//var currentUserId = User.Identity.GetUserId();
		var messages = MessageRepo.ListChat(senderid, partnerid);
		return Json(messages);
	}

	[HttpGet]
	public JsonResult Find(int senderid, int receiverid, string message)
	{
		//var currentUserId = User.Identity.GetUserId();
		var messages = MessageRepo.FindMessages(senderid, receiverid, message);
		return Json(messages);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="Message">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public int Create(int userid, int partnerid, string message)
	{
		MessageRepo.Insert(userid, partnerid, message);
		return MessageRepo.GetLastId();
	}

	[HttpPost]
	public void Edit(int messageid, string message)
	{
		MessageRepo.Update(messageid, message);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpPost]
	public void Delete(string messageid)
	{
		MessageRepo.Delete(messageid);
	}
}
