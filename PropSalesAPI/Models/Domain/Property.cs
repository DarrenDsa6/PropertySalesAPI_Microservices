using System.Collections.Generic; // Importing the namespace for collections
using System.ComponentModel.DataAnnotations; // Importing for data annotations (validation attributes)
using System.ComponentModel.DataAnnotations.Schema; // Importing for database schema attributes

namespace PropertySales.Models.Domain // Defining the namespace for the domain models
{
    public class Property // Class representing a property for sale or rent
    {
        // This is the primary key for the Property entity
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies that the database generates the value for this property
        public int PropertyId { get; set; } // Unique identifier for each property

        [Required] // Indicates that this field is mandatory
        [Column(TypeName = "varchar(10)")] // Specifies the column type in the database
        public PropertyType PropertyType { get; set; } // Type of property (e.g., Sale or Rent)

        [Required] // Indicates that this field is mandatory
        public string Location { get; set; } // The location of the property

        [Required] // Indicates that this field is mandatory
        public string Pincode { get; set; } // Postal code for the property's location

        [Required] // Indicates that this field is mandatory
        [Column(TypeName = "decimal(18,2)")] // Specifies the column type in the database with decimal precision
        public decimal Price { get; set; } // Price of the property

        public string Description { get; set; } // Optional description of the property

        public string Amenities { get; set; } // Optional amenities available with the property

        // Collection of images associated with the property
        public virtual ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

        [Required] // Indicates that this field is mandatory
        [Column(TypeName = "varchar(10)")] // Specifies the column type in the database
        public PropertyStatus Status { get; set; } // Current status of the property (e.g., Active, Pending)

        // Foreign key referencing the user who added the property
        [ForeignKey("User")]
        public int AddedBy { get; set; } // User identifier for the person who added the property
    }

    // Enum representing the types of properties available
    public enum PropertyType
    {
        Sale, // Indicates the property is for sale
        Rent // Indicates the property is for rent
    }

    // Enum representing the various statuses a property can have
    public enum PropertyStatus
    {
        Active, // Property is currently active and available
        Pending, // Property is under consideration or offer
        Sold, // Property has been sold
        Rented // Property has been rented out
    }
}
