namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class Message
{
	[DisplayName("Id")]
	[Required]
	public int Id { get; set; }

	[DisplayName("Sender")]
    [Required]
    public int Sender { get; set; }

	[DisplayName("Receiver")]
	[Required]
	public int Receiver { get; set; }

	[DisplayName("Message")]
    [Required]
    public string MessageString { get; set; }

	[DisplayName("Date")]
	[Required]
	public DateTime Date { get; set; }

	[DisplayName("Time")]
	[Required]
	public DateTime Time { get; set; }
}
