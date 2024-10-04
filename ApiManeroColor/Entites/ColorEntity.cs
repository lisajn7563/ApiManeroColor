using System.ComponentModel.DataAnnotations;

namespace ApiManeroColor.Entites;

public class ColorEntity
{
    [Key]
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string colorTitle { get; set; } = null!;
    public string PartitionKey { get; set; } = "Colors";

}
