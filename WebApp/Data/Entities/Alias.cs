using System.ComponentModel.DataAnnotations;

namespace WebApp.Data.Entities;

public class Alias
{
    public Alias()
    {
        Users = new HashSet<IdentityAppUser>();
        TasksToDo = new HashSet<TaskToDo>();
    }
    [Key] public Guid Id { get; set; }

    [Required]
    [StringLength(48, MinimumLength = 4)]
    public string Name { get; set; }

    [StringLength(48, MinimumLength = 4)]
    public string Domain { get; set; }

    public virtual ICollection<TaskToDo> TasksToDo { get; set; }
    public virtual ICollection<IdentityAppUser> Users { get; set; }

    public bool IsGroup { get; set; }


}