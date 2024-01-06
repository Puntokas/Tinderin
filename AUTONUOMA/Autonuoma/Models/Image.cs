namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ImageModel
{
    public int Id { get; set; }
    public byte[] ImageData { get; set; }
    public string ImageName { get; set; }
}

