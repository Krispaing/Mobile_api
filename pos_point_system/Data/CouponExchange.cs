using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_point_system.Data
{
    public class CouponExchange
    {
        [Key]
        public string? Id { get; set; }

        [Column("member_id")]
        public string? MemberId { get; set; }
        public Member? Member { get; set; }


        [Column("exchanged_points")]
        public int ExchangedPoints { get; set; }
    }
}
