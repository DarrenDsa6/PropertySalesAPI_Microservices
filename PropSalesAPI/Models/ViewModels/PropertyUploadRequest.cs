using Microsoft.AspNetCore.Http; // Importing for handling HTTP requests and file uploads
using PropertySales.Models.Domain; // Importing domain models including PropertyType and PropertyStatus
using System.Collections.Generic; // Importing for collections
using System.ComponentModel.DataAnnotations; // Importing for data annotations (validation attributes)

public class PropertyUploadRequest // Class representing a request to upload a new property
{
    public int AddedBy { get; set; } // Identifier for the user adding the property

    public PropertyType PropertyType { get; set; } // Type of property (e.g., Sale or Rent)
    public string Location { get; set; } // Location of the property
    public string Pincode { get; set; } // Postal code for the property's location
    public decimal Price { get; set; } // Price of the property
    public string Description { get; set; } // Description of the property
    public string Amenities { get; set; } // Amenities available with the property
    public PropertyStatus Status { get; set; } // Current status of the property (e.g., Active, Sold)

    // List to hold image files for the property; initialized as an empty list
    public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
}
