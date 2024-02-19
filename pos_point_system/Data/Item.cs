using System.ComponentModel.DataAnnotations.Schema;

namespace pos_point_system.Data
{
    public class Item
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
