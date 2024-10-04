using System.ComponentModel.DataAnnotations;

namespace ApiManeroColor.Models;

public class ColorRegistration
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string colorTitle { get; set; } = null!;
}
