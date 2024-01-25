using AzureFullstackPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureFullstackPractice.Data;

public class PersonDbContext: DbContext
{
    public PersonDbContext(DbContextOptions options) : base(options) {}

    public virtual DbSet<Person> Persons { get; set; } = default;
}