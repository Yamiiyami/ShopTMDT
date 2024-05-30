using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using ShopTMDT.Data;
using ShopTMDT.Helpers;
using ShopTMDT.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
namespace ShopTMDT.services
{
    public interface IAccountRepository
    {
        public Task<IEnumerable<UserDetailVM>> GetUsers(); 
        public Task<JsonResult> Login(LoginRepuest login);
        public Task<JsonResult> Register(Register register);
        public Task<UserDetailVM> GetUserDetail(string id);
        public Task<JsonResult> AdminRegister(AdminRegsister account);
        public Task<JsonResult> Delete(string id);
    }
    public class AccountRepository : IAccountRepository
    {
        private readonly StmdtContext _dbcontext;
        private readonly PasswordHash passwordHash;
        private readonly IConfiguration _configuration;
        private readonly ISendEmailService _sendEmailService;
        public AccountRepository(StmdtContext dbcontext, IConfiguration configuration,ISendEmailService sendEmailService)
        {
            _dbcontext = dbcontext;
            passwordHash = new PasswordHash();
            _configuration = configuration;
            _sendEmailService = sendEmailService;
        }
        public async Task<JsonResult> Register(Register register)
        {

            try
            {
                //string barcodeValue = Guid.NewGuid().ToString();

                //var barcodeWriter = new BarcodeWriter<byte[]>
                //{
                //    Format = BarcodeFormat.CODE_128,
                //    Options = new EncodingOptions
                //    {
                //        Width = 300,
                //        Height = 100,
                //        Margin = 10
                //    }
                //};
                //var barcodeImage = barcodeWriter.Write(barcodeValue);

                var Checkuser = await _dbcontext.Users.SingleOrDefaultAsync(u => u.Email == register.EmailAddress);
                if (Checkuser != null)
                {
                    return new JsonResult("Email này đã tồn tại")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                string barcodeValue = Guid.NewGuid().ToString();
                var password = passwordHash.HashPassword(register.Password);

                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = "1",
                    Email = register.EmailAddress,
                    PasswordHash = password,
                    FullName = register.FullName,
                    LockoutEnabled = true,
                    Qrcode = barcodeValue
                };
                await _dbcontext.AddAsync(user);
                await _dbcontext.SaveChangesAsync();

                return new JsonResult("tạo thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch(Exception ex) 
            {
                return new JsonResult("tạo thất bại: "+ex.Message)
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            
        }
        public async Task<JsonResult> Login(LoginRepuest login)
        {

            var User = await (from u in _dbcontext.Users where u.Email == login.Email select u).FirstOrDefaultAsync();
            if (User == null)
            {
                return new JsonResult("không tìm thấy user với Email này")
                {
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            if (!passwordHash.verifyPassword(login.Password, User.PasswordHash!))
            {
                return new JsonResult("Mật khẩu không đúng")
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            string token = GeneraToken(User);
            return new JsonResult(token)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }
        public string GeneraToken(User user)
        {
            var tokenHandle = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT").GetSection("Access_Secret").Value!);
            var role = _dbcontext.Roles.SingleOrDefault(r => r.Id == user.RoleId);

            List<Claim> claims = [
                 new (JwtRegisteredClaimNames.Email, user.Email!),
                 new (JwtRegisteredClaimNames.Name,user.FullName!),
                 new (JwtRegisteredClaimNames.NameId,user.Id),
                 new (JwtRegisteredClaimNames.Aud, _configuration.GetSection("JWT").GetSection("ValidAudience").Value!),
                 new (JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWT").GetSection("ValidIssuer").Value!)
                ];
            claims.Add(new Claim(ClaimTypes.Role, role!.Name!));

            var tokenDecription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(45),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandle.CreateToken(tokenDecription);
            return tokenHandle.WriteToken(token);
        }
        public async Task<IEnumerable<UserDetailVM>> GetUsers()
        {
            var users = await (from u in _dbcontext.Users
                               select new UserDetailVM
                               {
                                   Id = u.Id,
                                   Email = u.Email,
                                   FullName = u.FullName,
                                   Role = u.Role.Name,
                                   AccessFailesCount = u.AccessFailedCount,
                                   PhoneNumber = u.PhoneNumber,
                                   PhoneNumberConfirmed = u.PhoneNumberConfirmed,

                               }).ToListAsync();
            return users;
        }
        public async Task<UserDetailVM> GetUserDetail(string id)
        {
            var user = await (from u in _dbcontext.Users
                              where u.Id == id
                              select new UserDetailVM
                              {
                                  Id = u.Id,
                                  Email = u.Email,
                                  FullName = u.FullName,
                                  Role = u.Role.Name,
                                  AccessFailesCount = u.AccessFailedCount,
                                  PhoneNumber = u.PhoneNumber,
                                  PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                              }).SingleOrDefaultAsync();

            return user;
        }
        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                var user = await _dbcontext.Users.SingleOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return new JsonResult("khong tim thay user")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                _dbcontext.Remove(user);
                await _dbcontext.SaveChangesAsync();

                return new JsonResult("Xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch
            {
                return new JsonResult("Xoá thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }


        }

        public async Task<JsonResult> AdminRegister(AdminRegsister account)
        {
            try
            {
                var acc = await _dbcontext.Users.SingleOrDefaultAsync(u => u.Email == account.EmailAddress);
                if(acc != null)
                {
                    return new JsonResult("tài khoản đã tồn tại")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                var passrandom = passwordHash.GetPassRandom(8);
                var passhash = passwordHash.HashPassword(passrandom);
                string barcodeValue = Guid.NewGuid().ToString();
                User user;
                if(account.role is null)
                {
                    user = new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = "1",
                        Email = account.EmailAddress,
                        PasswordHash = passhash,
                        FullName = account.FullName,
                        Qrcode = barcodeValue,
                        LockoutEnabled = true

                    };
                }
                else
                {
                    user = new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = account.role,
                        Email = account.EmailAddress,
                        PasswordHash = passhash,
                        FullName = account.FullName,
                        LockoutEnabled = true,
                        Qrcode = barcodeValue
                    };
                }

                await _dbcontext.AddAsync(user);
                await _dbcontext.SaveChangesAsync();
                EmaiModel emaiModel = new EmaiModel();
                emaiModel.ToEmail = account.EmailAddress;
                emaiModel.subject = "Chao ban " + user.FullName;
                emaiModel.body = "Mật khẩu của bạn là: " + passrandom;
                _sendEmailService.SendEmail(emaiModel);
                return new JsonResult("tạo thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch
            {
                return new JsonResult("tạo thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
