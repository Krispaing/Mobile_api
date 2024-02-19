using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_point_system.Data
{
    public class TransactionDetail
    {
        [Key]
        [Column("detail_id")]
        public string? DetailId { get; set; }

        [Column("id")]
        public string? TransactionId { get; set; }
        public Transaction? Transaction { get; set; }

        public int? Qty { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Point { get; set; }

        [Column("item_id")]
        public string? ItemId { get; set; }
        public Item? Item { get; set; }
    }
}
