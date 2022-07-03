using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Data.Models
{
    public class Credentials
    {
        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string Identity { get; set; }

        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Secret { get; set; }

        [DisplayName("Repeat Password")]
        public string? RepeatSecret { get; set; }

        [MaxLength(48)] public string Alias { get; set; } = "None";
        public bool RememberMe { get; set; }
    }

}
