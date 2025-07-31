using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySimpleApi.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("category_id")]
        public long CategoryId { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; } = "expense"; // income or expense

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public string? Note { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
    }
}
