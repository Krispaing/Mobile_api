using System.ComponentModel.DataAnnotations.Schema;

namespace pos_point_system.Data
{
    public class Transaction
    {
        public string? Id { get; set; }

        [Column("member_id")]
        public string? MemberId { get; set; }
        public Member? Member { get; set; }

        [Column("transaction_date")]
        public DateTime? TransactionDate { get; set; }

        public List<TransactionDetail>? TransactionDetailList { get; set; }
    }
}
