namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.ModelisReport;
using ModelisReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.ModelisReport;


/// <summary>
/// Controller for producing reports.
/// </summary>
public class ReportsController : Controller
{

	/// <summary>
	/// Produces contracts report.
	/// </summary>
	/// <param name="dateFrom">Starting date. Can be null.</param>
	/// <param name="dateTo">Ending date. Can be null.</param>
	/// <returns>Report view.</returns>
	[HttpGet]
	public ActionResult Contracts(DateTime? dateFrom, string? DezesTipasFiltr, string? MarkeFiltr )
	{
		var report = new ModelisReport.Report();
		report.DateFrom = dateFrom;
		report.DezesTipasFiltr = DezesTipasFiltr;
		report.MarkeFiltr = MarkeFiltr;

		report.Modeliai = AtaskaitaRepo.GetModeliai(report.DateFrom, report.DezesTipasFiltr, report.MarkeFiltr);

		foreach (var item in report.Modeliai)
		{
			//report.VisoSumaSutartciu += item.Kaina;
			report.VisoAutomobiliuKiekis += 1;
		}
		PopulateSelection(report);

		return View(report);
	}
	public void PopulateSelection(ModelisReport.Report ataskaita)
	{
		var busenos = AtaskaitaRepo.ListDezes();

		ataskaita.Dezes = busenos.Select(it => {
			return new SelectListItem()
			{
				Value = Convert.ToString(it.Id),
				Text = it.Tipas
			};
		}).ToList();
	}
}
