using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenThanhLong_SE18C.NET_A01.Models
{
    public class NewsTag
    {
        [Key, Column(Order = 0)]
        [StringLength(20)]
        public string NewsArticleID { get; set; } = null!;

        [Key, Column(Order = 1)]
        public int TagID { get; set; }

        // Navigation properties
        [ForeignKey("NewsArticleID")]
        public virtual NewsArticle NewsArticle { get; set; } = null!;

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; } = null!;
    }
}
