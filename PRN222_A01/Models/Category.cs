using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenThanhLong_SE18C.NET_A01.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short CategoryID { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(100)]
        public string CategoryName { get; set; } = null!;

        [Required(ErrorMessage = "Category Description is required")]
        [StringLength(250)]
        [Display(Name = "Category Description")]
        public string CategoryDesciption { get; set; } = null!;

        [Display(Name = "Parent Category")]
        public short? ParentCategoryID { get; set; }

        [Display(Name = "Active Status")]
        public bool? IsActive { get; set; }

        // Navigation properties
        [ForeignKey("ParentCategoryID")]
        public virtual Category? ParentCategory { get; set; }
        
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
    }
}
