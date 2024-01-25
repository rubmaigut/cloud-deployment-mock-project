using System.ComponentModel.DataAnnotations;

namespace AzureFullstackPractice.Models;

public class Person
{
    [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Name { get; set; }
    [Required]
    public string Age { get; set; }
}