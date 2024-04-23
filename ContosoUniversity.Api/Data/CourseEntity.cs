using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data;

public class CourseEntity 
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("CourseID")]
    public int Id { get; set; }
    public int UniversityId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Credits { get; set; }
}