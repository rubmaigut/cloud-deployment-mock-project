using System.ComponentModel.DataAnnotations;

namespace AzureFullstackPractice.Models;

public class Person
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Age { get; set; }
}