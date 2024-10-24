using PropertySales.Models.Domain;

public class PropertyPatchDto
{
    public PropertyType PropertyType { get; set; }
    public string Location { get; set; }
    public string Pincode { get; set; }
    public decimal? Price { get; set; }
    public string Description { get; set; }
    public string Amenities { get; set; }
    public PropertyStatus Status { get; set; }
    public IFormFile ImageFile { get; set; } // Only one image
}
