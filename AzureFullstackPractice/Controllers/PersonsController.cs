using AzureFullstackPractice.Data;
using AzureFullstackPractice.Models;
using AzureFullstackPractice.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureFullstackPractice.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly PersonDbContext _context;
    private readonly BlobStorageService _blobStorageService;

    public PersonsController(PersonDbContext context, BlobStorageService blobStorageService)
    {
        _context = context;
        _blobStorageService = blobStorageService;
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
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }
        var tempFilePath = Path.GetTempFileName();
        
        using (var stream = System.IO.File.Create(tempFilePath))
        {
            await file.CopyToAsync(stream);
        }

        await _blobStorageService.UploadFileAsync("personfullstackblob", tempFilePath);
        return Ok("File uploaded successfully.");
    }
}
