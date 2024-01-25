using AzureFullstackPractice.Data;
using AzureFullstackPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureFullstackPractice.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly PersonDbContext _context;

    public PersonsController(PersonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Person>>> GetAll()
    {
        return _context.Persons.ToList();
    }

    [HttpPost("/addPerson")]
    public async Task<ActionResult<Person>> AddPerson(Person person)
    {
        if (person is null) return BadRequest("wrong information.");
        _context.Persons.Add(person);
       await _context.SaveChangesAsync();
        return Ok(person);
    }
}
