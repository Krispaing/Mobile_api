using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_point_system.DALs;
using pos_point_system.Data;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace pos_point_system.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]    

    public class MemberController : ControllerBase
    {
        private readonly IMemberDAL member_dal;
        private readonly ApplicationDbContext _dbContext;

        public MemberController(IMemberDAL _member_dal, ApplicationDbContext dbContext)
        {
            member_dal = _member_dal; 
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public async Task<MemberResponse> register([FromBody] MemberRequest model)
        {
            var response = new MemberResponse();
            try
            {
                var member = new Data.Member
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    CreatedAt = DateTime.Now
                };
                await _dbContext.Members.AddAsync(member);
                await _dbContext.SaveChangesAsync();

                response.Id = member.Id;
                response.Name = member.Name;
                response.Phone = member.Phone;
                response.Email = member.Email;
                response.CreatedAt = member.CreatedAt;
            }
            catch (Exception)
            {

                throw;
            }


            return response;
        }

        [HttpGet("purchaseList")]
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public async Task<IActionResult> PurchaseList(string memberid)
        {
            //var response = new List<MemberPurchaseResponse>();

            var transactions = await _dbContext.Transactions
                    .Where(t => t.MemberId == memberid)
                    .Include(t => t.Member)
                    .Include(t => t.TransactionDetailList).ThenInclude(td => td.Item)
                    .AsNoTracking()
                    .ToListAsync();

            var response = transactions.Select(t => new MemberPurchaseResponse
            {
                MemberId = t.Member?.Id,
                MemberName = t.Member?.Name,
                TransactionDate = t.TransactionDate,
                TransactionDetails = t.TransactionDetailList?
                        .Select(td => new TransactionDetailDto
                        {
                            ItemName = td.Item?.Name,
                            Qty = td.Qty,
                            Amount = td.Amount,
                            Point = td.Point
                        }).ToList()
            }).ToList();

            return Ok(response);
        }

        [HttpGet("totalPoint")]
        [HttpGet("RefreshPoint")]
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public async Task<IActionResult> TotalPoint(string memberid)
        {
            var transactions = await member_dal.GetTransactions(memberid);
            var totalPoints = transactions.Sum(t => t.TransactionDetailList?.Sum(td => td.Point));

            var response = new PointResponse
            {
                MemberId = transactions.FirstOrDefault()?.MemberId,
                MemberName = transactions.FirstOrDefault()?.Member?.Name,
                TotalPoint = totalPoints.ToString()
            };

            return Ok(response);
        }        

        [HttpPost("redeemCoupon")]
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public async Task<IActionResult> RedeemCoupon([FromBody] Coupon_Request model)
        {
            try
            {
                var member = await _dbContext.Members.Where(c => c.Id == model.MemberId).FirstOrDefaultAsync();
                if (member == null)
                {
                    return NotFound("Member not found");
                }

                decimal totalPoints = await member_dal.GetPoints(model.MemberId);

                if (totalPoints < 100 || totalPoints < Convert.ToInt64(model.CouponRequest) * 500)
                {
                    return BadRequest($"Minimum points is {500}");
                }
                else
                {
                    var coupon = await _dbContext.CouponExchanges.Where(c => c.MemberId == model.MemberId).FirstOrDefaultAsync();
                    if (model.CouponRequest == "All")
                    {
                        for (int i = 500; totalPoints >= 500; i += 500)
                        {
                            totalPoints -= 500;
                            if (coupon == null)
                            {
                                var c = new CouponExchange
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    MemberId = model.MemberId,
                                    ExchangedPoints = 1
                                };
                                await _dbContext.CouponExchanges.AddAsync(c);
                                await _dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                coupon.ExchangedPoints = 1;
                                await _dbContext.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        totalPoints -= Convert.ToInt64(model.CouponRequest) * 500;
                        if (coupon == null)
                        {
                            var c = new CouponExchange
                            {
                                Id = Guid.NewGuid().ToString(),
                                MemberId = model.MemberId,
                                ExchangedPoints = 1
                            };
                            await _dbContext.CouponExchanges.AddAsync(c);
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            coupon.ExchangedPoints = 1;
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    var response = new CouponResponse
                    {
                        Message = $"{coupon?.ExchangedPoints} Coupon exchanged successfully",
                        MemberId = coupon?.MemberId,
                        TotalCoupon = coupon?.ExchangedPoints.ToString(),
                        RemainingPoints = totalPoints - (coupon?.ExchangedPoints * 100)
                    };

                    return Ok(response);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }                            
        }

        [HttpPost("requestOTP")]
        [Authorize(AuthenticationSchemes = "JwtAuthentication")]
        public async Task<OTPResponse> RequestOTP([FromBody] OTPRequest request)
        {
            var member = _dbContext.Members.FirstOrDefault(m => m.Phone == request.PhoneNumber);
            var response = new OTPResponse();

            if (member == null)
            {
                response.Message = "Please try again later.";

                return response;
            }

            // Generate and save OTP code for the member
            string otpCode = member_dal.GenerateRandomOTP();
            member.OTPcode = otpCode;
            member.OTPExpirationTime = DateTime.UtcNow.AddMinutes(5);
            await _dbContext.SaveChangesAsync();

            // need to implement to Send OTP code to the member's phone number 
            //smsService.SendOTPCode(member.Phone, otpCode);

            response.Message = "OTP code sent successfully";
            response.PhoneNumber = request.PhoneNumber;
            response.OTPcode = otpCode;

            return response;
        }

        [HttpPost("verifyOTP")]
        [Authorize(AuthenticationSchemes = "JwtAuthentication")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPRequest request)
        {
            var member = _dbContext.Members.FirstOrDefault(m => m.Phone == request.PhoneNumber);

            if (member == null)
            {
                return BadRequest("Member not found");
            }

            if (member.OTPcode != request.OTPcode || member.OTPExpirationTime < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired OTP code");
            }

            // Clear the OTP code and expiration time after successful verification
            member.OTPcode = null;
            member.OTPExpirationTime = null;
            await _dbContext.SaveChangesAsync();

            return Ok("OTP code verified successfully");
        }

        [HttpPost("refreshOTP")]
        [Authorize(AuthenticationSchemes = "JwtAuthentication")]
        public async Task<OTPResponse> RefreshOTP([FromBody] OTPRequest request)
        {
            var member = _dbContext.Members.FirstOrDefault(m => m.Phone == request.PhoneNumber);
            var response = new OTPResponse();

            if (member == null)
            {
                response.Message = "Your login session has expired.";

                return response;
            }

            string newOtpCode = member_dal.GenerateRandomOTP();
            member.OTPcode = newOtpCode;
            member.OTPExpirationTime = DateTime.UtcNow.AddMinutes(5);
            await _dbContext.SaveChangesAsync();

            response.Message = "New OTP code sent successfully";
            response.PhoneNumber = request.PhoneNumber;
            response.OTPcode = newOtpCode;

            return response;
        }
    }
}
