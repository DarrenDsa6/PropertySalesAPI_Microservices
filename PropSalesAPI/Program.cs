using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PropertySales.Models.Domain;
using PropSalesAPI.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly PropertySalesDbContext _context; // Replace with your actual DbContext
    private readonly string _storagePath;
    private readonly string _imageBasePath;

    public PropertyController(PropertySalesDbContext context, IConfiguration configuration)
    {
        _context = context;
        var uploadsFolder = configuration["ImageStorage:Path"];
        _imageBasePath = uploadsFolder; // Set base path for image URL conversion
        _storagePath = Path.Combine(Directory.GetCurrentDirectory(), uploadsFolder);

        // Ensure the directory exists
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddProperty([FromForm] PropertyUploadRequest request)
    {
        if (request == null)
        {
            return BadRequest("Property data is required.");
        }

        var property = new Property
        {
            PropertyType = request.PropertyType,
            Location = request.Location,
            Pincode = request.Pincode,
            Price = request.Price,
            Description = request.Description,
            Amenities = request.Amenities,
            Status = request.Status,
            AddedBy = request.AddedBy,
        };

        // Check for the image file
        if (request.ImageFile == null)
        {
            return BadRequest("An image file is required.");
        }

        if (request.ImageFile.Length > 0)
        {
            var fileName = Path.GetFileName(request.ImageFile.FileName);
            var filePath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(stream);
            }

            // Log the file path to ensure it's being saved correctly
            Console.WriteLine($"Image saved to: {filePath}");

            // Assign the image to the property
            property.PropertyImage = new PropertyImage { FilePath = filePath };
        }

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        return Ok(new { property.PropertyId });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProperties()
    {
        var properties = await _context.Properties
            .Include(p => p.PropertyImage) // Include the image if needed
            .ToListAsync();

        return Ok(properties);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePropertyPartial(int id, [FromForm] PropertyPatchDto request)
    {
        var existingProperty = await _context.Properties
            .Include(p => p.PropertyImage) // Include the image if needed
            .FirstOrDefaultAsync(p => p.PropertyId == id);

        if (existingProperty == null)
        {
            return NotFound($"Property with ID {id} not found.");
        }

        // Update fields only if they have values in the request
        if (request.PropertyType.HasValue)
        {
            existingProperty.PropertyType = request.PropertyType.Value;
        }

        if (!string.IsNullOrWhiteSpace(request.Location))
        {
            existingProperty.Location = request.Location;
        }

        if (!string.IsNullOrWhiteSpace(request.Pincode))
        {
            existingProperty.Pincode = request.Pincode;
        }

        if (request.Price.HasValue)
        {
            existingProperty.Price = request.Price.Value;
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            existingProperty.Description = request.Description;
        }

        if (!string.IsNullOrWhiteSpace(request.Amenities))
        {
            existingProperty.Amenities = request.Amenities;
        }

        if (request.Status.HasValue)
        {
            existingProperty.Status = request.Status.Value;
        }

        // Handle ImageFile if provided in the request
        if (request.ImageFile != null && request.ImageFile.Length > 0)
        {
            var fileName = Path.GetFileName(request.ImageFile.FileName);
            var filePath = Path.Combine(_storagePath, fileName);

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(stream);
            }

            Console.WriteLine($"Image updated to: {filePath}");
            existingProperty.PropertyImage = new PropertyImage { FilePath = filePath }; // Update the image
        }

        await _context.SaveChangesAsync();

        return NoContent(); // Return a response indicating success
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        var property = await _context.Properties.Include(p => p.PropertyImage).FirstOrDefaultAsync(p => p.PropertyId == id);

        if (property == null)
        {
            return NotFound($"No Entry with id: {id}");
        }

        // Optionally delete the image from the file system
        if (property.PropertyImage != null && System.IO.File.Exists(property.PropertyImage.FilePath))
        {
            System.IO.File.Delete(property.PropertyImage.FilePath);
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
        return Ok(property);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPropertiesByUserId(int id)
    {
        var properties = await _context.Properties
            .Where(p => p.AddedBy == id)
            .Include(p => p.PropertyImage) // Include the image if needed
            .ToListAsync();

        if (properties == null || !properties.Any())
        {
            return NotFound($"No properties added by the user with id: {id}");
        }

        // Handle image path for each property
        foreach (var property in properties)
        {
            if (property.PropertyImage != null)
            {
                property.PropertyImage.FilePath = property.PropertyImage.FilePath
                    .Replace(Path.Combine(Directory.GetCurrentDirectory(), _imageBasePath) + "\\", "/Uploads/");
            }
        }

        return Ok(properties);
    }
}
