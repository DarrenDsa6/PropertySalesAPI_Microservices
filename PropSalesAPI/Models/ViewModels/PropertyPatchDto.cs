using Microsoft.AspNetCore.Http; // Importing for handling HTTP requests and file uploads
using PropertySales.Models.Domain; // Importing domain models including PropertyType and PropertyStatus
using System.Collections.Generic; // Importing for collections
using System.ComponentModel.DataAnnotations; // Importing for data annotations (validation attributes)

public class PropertyPatchDto // Data Transfer Object (DTO) for updating property details
{
    public PropertyType? PropertyType { get; set; } // Nullable for optional updates to property type
    public string? Location { get; set; } // Nullable for optional updates to the location
    public string? Pincode { get; set; } // Nullable for optional updates to the postal code
    public decimal? Price { get; set; } // Nullable for optional updates to the price
    public string? Description { get; set; } // Nullable for optional updates to the description
    public string? Amenities { get; set; } // Nullable for optional updates to the amenities
    public PropertyStatus? Status { get; set; } // Nullable for optional updates to the property status

    // List to hold image files for the property; can be empty if no images are provided
    public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
}
