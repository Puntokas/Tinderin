namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class Story
{
    [DisplayName("ID")]
    
    public string Id { get; set; }

    [DisplayName("User")]
    [Required]
    public string User_Id { get; set; }

	[DisplayName("Public")]
	[Required]
	public bool Public { get; set; }

    [DisplayName("Image")]
    public int? ImageId { get; set; } 

}

