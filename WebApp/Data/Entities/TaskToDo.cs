using System.ComponentModel.DataAnnotations;
using WebApp.Data.Enums;

namespace WebApp.Data.Entities;

public class TaskToDo
{
    [Key] public Guid Id { get; set; }

    [Required]
    [StringLength(48, MinimumLength = 3)]
    public string Title { get; set; }

    [MaxLength(256)]
    public string Description { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime? Deadline { get; set; }
    [EnumDataType(typeof(Priority))]
    public Priority Priority { get; set; }

    public bool IsCompleted { get; set; }
}