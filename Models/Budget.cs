using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySimpleApi.Models
{
    [Table("budgets")]
    public class Budget
    {
        [Key]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("category_id")]
        public long CategoryId { get; set; }

        [Column("amount_limit", TypeName = "decimal(12,2)")]
        public decimal AmountLimit { get; set; }

        [Required]
        public string Period { get; set; } = "monthly"; // monthly or weekly

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
    }
}
