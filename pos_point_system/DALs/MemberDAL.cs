using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_point_system.Data;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static pos_point_system.Controllers.MemberController;

namespace pos_point_system.DALs
{
    public interface IMemberDAL
    {
        string GenerateRandomOTP();
        Task<List<Transaction>> GetTransactions(string memberid);
        Task<decimal> GetPoints(string memberid);
    }
    public class MemberDAL : IMemberDAL
    {
        private readonly ApplicationDbContext _dbContext;
        public MemberDAL(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public string GenerateRandomOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999); // Generate a 6-digit OTP
            return otp.ToString();
        }

        public async Task<List<Transaction>> GetTransactions(string memberid)
        {
            var transactions = await _dbContext.Transactions
                    .Where(t => t.MemberId == memberid)
                    .Include(t => t.Member)
                    .Include(t => t.TransactionDetailList)
                    .AsNoTracking()
                    .ToListAsync();

            return transactions;
        }

        public async Task<decimal> GetPoints(string memberid)
        {
            var transactions = await GetTransactions(memberid);
            var totalAmount = transactions.Sum(t => t.TransactionDetailList?.Sum(td => td.Amount));
            decimal p = Convert.ToDecimal(totalAmount) / 100;

            return p;
        }
    }    


    public class MemberResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    
    public class MemberPurchaseResponse
    {
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public DateTime? TransactionDate { get; set; }
        public List<TransactionDetailDto>? TransactionDetails { get; set; }
    }
    public class TransactionDetailDto
    {
        public string? ItemName { get; set; }
        public int? Qty { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Point { get; set; }

    }

    public class PointResponse
    {
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? TotalPoint { get; set; }
    }

    public class Coupon_Request
    {
        public string? MemberId { get; set; }
        public string? CouponRequest { get; set; }
    }

    public class CouponResponse 
    {
        public string? Message { get; set; }
        public string? MemberId { get; set; }
        public string? TotalCoupon { get; set; }
        public decimal? RemainingPoints { get; set; }
    }

    public class OTPRequest
    {
        public string? PhoneNumber { get; set; }
    }

    public class OTPResponse
    {
        public string? Message { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OTPcode { get; set; }
    }

    public class VerifyOTPRequest
    {
        [MaxLength(6)]
        public string? OTPcode { get; set; }

        public string? PhoneNumber { get; set; }
    }

}
