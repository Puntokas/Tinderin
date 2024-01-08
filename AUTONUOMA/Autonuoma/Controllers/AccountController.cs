namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Autonuoma;
using Newtonsoft.Json;

public class AccountController : Controller
    {
    /// <summary>
    /// This is invoked when either 'Index' action is requested or no action is provided.
    /// </summary>
    /// <returns>Entity list view.</returns>
    [HttpGet]
    public ActionResult Index()
    {
        Console.WriteLine("Indexpage");
        return Create();
    }

    /// <summary>
    /// This is invoked when creation form is first opened in browser.
    /// </summary>
    /// <returns>Creation form view.</returns>
    [HttpGet]
    public ActionResult Create()
    {
        var account = new Account();
        return View(account);
    }

    /// <summary>
    /// This is invoked when buttons are pressed in the creation form.
    /// </summary>
    /// <param name="automobilis">Entity model filled with latest data.</param>
    /// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
    [HttpPost]
    public ActionResult Create(Account account)
    {
        //do not allow creation of entity with 'asmensKodas' field matching existing one
        var match = AccountRepo.Find(account.username);

        Console.WriteLine(match);


        if (match != null)
            ModelState.AddModelError("Account", "Field value already exists in database.");

        //form field validation passed?
        if (ModelState.IsValid)
        {
            account.last_login = DateTime.Now;
            account.profile_picture_id = 1;

            account.password = Encrypt(account.password, 3);
            AccountRepo.Insert(account);
            Console.WriteLine("Daejo");

            //save success, go back to the entity list
            return RedirectToAction("Index", "AccountLogin");
        }

        //form field validation failed, go back to the form
        
        return RedirectToAction("Index");
    }

    /// <summary>
    /// This is invoked when editing form is first opened in browser.
    /// </summary>
    /// <param name="id">ID of the entity to edit.</param>
    /// <returns>Editing form view.</returns>
    [HttpGet]
    public ActionResult Edit()
    {
        if (HttpContext.Session.GetString("User") == null)
            return View();

        string userJson = HttpContext.Session.GetString("User");

        Account account = JsonConvert.DeserializeObject<Account>(userJson);
        return View(AccountRepo.Find(account.username));
    }

    /// <summary>
    /// This is invoked when editing form is first opened in browser.
    /// </summary>
    /// <param name="id">ID of the entity to edit.</param>
    /// <returns>Editing form view.</returns>
    [HttpPost]
    public ActionResult Edit(Account temp)
    {
        if (HttpContext.Session.Get("User") == null)
            return View();

        string userJson = HttpContext.Session.GetString("User");

        Account account = JsonConvert.DeserializeObject<Account>(userJson);
        
        account.name = temp.name;
        account.surname = temp.surname;
        account.email = temp.email;
        account.birth_date = temp.birth_date;
        account.description = temp.description;
        account.address = temp.address;

        AccountRepo.Update(account);

        HttpContext.Session.SetString("User", JsonConvert.SerializeObject(account));

        return RedirectToAction("Details", "Account");
    }

    /// <summary>
    /// This is invoked when editing form is first opened in browser.
    /// </summary>
    /// <param name="id">ID of the entity to edit.</param>
    /// <returns>Editing form view.</returns>
    [HttpPost]
    public ActionResult Main(Account temp)
    {
        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// This is invoked when editing form is first opened in browser.
    /// </summary>
    /// <param name="id">ID of the entity to edit.</param>
    /// <returns>Editing form view.</returns>
    [HttpGet]
	public ActionResult Details()
	{
		if (HttpContext.Session.Get("User") == null)
			return View();

		string userJson = HttpContext.Session.GetString("User");

		Account account = JsonConvert.DeserializeObject<Account>(userJson);
		return View(AccountRepo.Find(account.username));
	}

    /// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpPost]
    public ActionResult Details(Account temp)
    {
        return RedirectToAction("Edit", "Account");
    }

    /// <summary>
    /// This is invoked when buttons are pressed in the editing form.
    /// </summary>
    /// <param name="id">ID of the entity being edited</param>		
    /// <param name="automobilis">Entity model filled with latest data.</param>
    /// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
    /*[HttpPost]
    public ActionResult Edit(string id, Account account)
    {
        //form field validation passed?
        if (ModelState.IsValid)
        {
			account.password = Encrypt(account.password, 3);
			AccountRepo.Update(account);

            //save success, go back to the entity list
            return RedirectToAction("Index");
        }

        //form field validation failed, go back to the form
        return View(account);
    }*/

    /// </summary>
    /// <param name="id">ID of the entity to delete.</param>
    /// <returns>Deletion form view.</returns>
    [HttpPost]
    public ActionResult Delete(Account temp)
    {
        string accQuerry = HttpContext.Session.GetString("User");

        Account account = JsonConvert.DeserializeObject<Account>(accQuerry);

        AccountRepo.Delete(account.username);
        HttpContext.Session.Remove("User");
        return RedirectToAction("Index", "Home");
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
            AccountRepo.Delete(id);

            //deletion success, redired to list form
            return RedirectToAction("Index");
        }
        //entity in use, deletion not permitted
        catch (MySql.Data.MySqlClient.MySqlException)
        {
            //enable explanatory message and show delete form
            ViewData["deletionNotPermitted"] = true;

            var account = AccountRepo.Find(id);
            return View("Delete", account);
        }
    }

	static string Encrypt(string input, int key)
	{
		char[] chars = input.ToCharArray();

		for (int i = 0; i < chars.Length; i++)
		{
			if (char.IsLetter(chars[i]))
			{
				char baseChar = char.IsUpper(chars[i]) ? 'A' : 'a';
				chars[i] = (char)((chars[i] + key - baseChar) % 26 + baseChar);
			}
		}

		return new string(chars);
	}

	static string Decrypt(string input, int key)
	{
		return Encrypt(input, -key);
	}
}
