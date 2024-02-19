using System.ComponentModel.DataAnnotations.Schema;

namespace pos_point_system.Data
{
    public class Member
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        [Column("otp_code")] 
        public string? OTPcode { get; set; }

        [Column("otp_expire_time")]
        public DateTime? OTPExpirationTime { get; set; }        

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
