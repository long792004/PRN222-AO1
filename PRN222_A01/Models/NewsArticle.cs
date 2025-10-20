using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenThanhLong_SE18C.NET_A01.Models
{
    public class NewsArticle
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Article ID")]
        public string NewsArticleID { get; set; } = null!;

        [StringLength(400)]
        [Display(Name = "Title")]
        public string? NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(150)]
        [Display(Name = "Headline")]
        public string Headline { get; set; } = null!;

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }

        [StringLength(4000)]
        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        public string? NewsContent { get; set; }

        [StringLength(400)]
        [Display(Name = "Source")]
        public string? NewsSource { get; set; }

        [Display(Name = "Category")]
        public short? CategoryID { get; set; }

        [Display(Name = "Status")]
        public bool? NewsStatus { get; set; }

        [Display(Name = "Created By")]
        public short? CreatedByID { get; set; }

        [Display(Name = "Updated By")]
        public short? UpdatedByID { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }

        [ForeignKey("CreatedByID")]
        public virtual SystemAccount? CreatedBy { get; set; }

        public virtual ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
    }
}
