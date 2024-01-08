namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
public class AccountLoginController : Controller
    {
    /// <summary>
    /// This is invoked when either 'Index' action is requested or no action is provided.
    /// </summary>
    /// <returns>Entity list view.</returns>
    [HttpGet]
    public ActionResult Index()
    {
        var account = new AccountLogin();
        return View(account);
    }

    [HttpPost]
    public ActionResult Main(Account temp)
    {
        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// This is invoked when Login button is pressed opened in browser.
    /// </summary>
    /// <param name="id">ID of the entity to edit.</param>
    /// <returns>Editing form view.</returns>
    [HttpPost]
    public ActionResult Login(AccountLogin login)
    {
        Console.WriteLine("BeforeFind");
        var account = AccountRepo.Find(login.username);

        if (account == null)
        {
            ModelState.AddModelError("account", "This user does not exist");
        }
        Console.WriteLine("AfterFind");
        var encPassword = Encrypt(login.password, 3);

        if (ModelState.IsValid)
        {
			if (account.username == login.username && account.password == encPassword)
			{
                string userJson = JsonConvert.SerializeObject(account);
                HttpContext.Session.SetString("User", userJson);
				return RedirectToAction("Index", "Home");
			}
		}
        

        return View(account);
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
