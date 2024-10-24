using System.ComponentModel.DataAnnotations.Schema; // Importing for database schema attributes
using System.ComponentModel.DataAnnotations; // Importing for data annotations (validation attributes)

public class PropertyImage // Class representing an image associated with a property
{
    // This is the primary key for the PropertyImage entity
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies that the database generates the value for this property
    public int Id { get; set; } // Unique identifier for each property image

    public string FilePath { get; set; } // File path of the property image
}
