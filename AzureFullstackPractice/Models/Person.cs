using System.ComponentModel.DataAnnotations;

namespace AzureFullstackPractice.Model;

public class Person
{
    [Key]
    public string Id { get; set; } 
    [Required]
    public string Name { get; set; }
    [Required]
    public string Age { get; set; }
}