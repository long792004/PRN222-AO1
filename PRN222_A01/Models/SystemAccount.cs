using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenThanhLong_SE18C.NET_A01.Models
{
    public class SystemAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short AccountID { get; set; }

        [StringLength(100)]
        [Display(Name = "Account Name")]
        public string? AccountName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(70)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string? AccountEmail { get; set; }

        [Display(Name = "Role")]
        public int? AccountRole { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(70)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? AccountPassword { get; set; }

        // Navigation properties
        public virtual ICollection<NewsArticle> CreatedNewsArticles { get; set; } = new List<NewsArticle>();
    }
}
