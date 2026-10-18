using System.ComponentModel.DataAnnotations;
namespace firstASPnet.Models;
public class DemoRequest {
    [Required(ErrorMessage = "Enter a sample project name."), StringLength(80)]
    [Display(Name = "Sample project name")]
    public string? ProjectName { get; set; }
    [Required(ErrorMessage = "Enter a number of sample records.")]
    [Range(1, 100, ErrorMessage = "Enter a number from 1 to 100.")]
    [Display(Name = "Number of sample records")]
    public int? RecordCount { get; set; }
}
