namespace InfloUserManagerUI.Models;

public class UserDTO
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string Forename { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Date of Birth")]
    public DateTime? DateOfBirth { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
    [Display(Name = "Status")]
    public int StatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;
}
