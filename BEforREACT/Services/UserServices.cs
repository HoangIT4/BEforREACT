using BEforREACT.Data;
using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BEforREACT.Services
{
    public class UserServices
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserServices(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }


        public async Task<User> GetUserById(Guid userId)

        {
            if (userId == Guid.Empty)
            {
                return null;
            }

            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }



        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }




        public Task<bool> AddUser(User model)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
            _context.Users.Add(model);
            _context.SaveChanges();
            return Task.FromResult(true);
        }



        public async Task<bool> Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return false; // Email đã tồn tại

            user.Id = Guid.NewGuid(); // Đảm bảo ID luôn là duy nhất
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Mã hóa mật khẩu
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<bool> UpdateUser(Guid id, User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            // Cập nhật thông tin của người dùng hiện có
            existingUser.Email = updatedUser.Email;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            existingUser.UserName = updatedUser.UserName; // Nếu có thuộc tính này
            _context.Entry(updatedUser).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }




        public async Task<bool> ValidatePassword(string email, string rawPassword)
        {
            var user = await GetUserByEmail(email);
            if (user == null) return false;

            // So sánh mật khẩu nhập vào (đã được mã hóa) với mật khẩu đã lưu
            return VerifyPassword(rawPassword, user.Password);
        }

        private bool VerifyPassword(string rawPassword, string hashedPassword)
        {
            // Ví dụ dùng thư viện mã hóa mật khẩu
            return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword);
        }


        public async Task<string> LoginJwt(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user == null) return (null);

            var isValidPassword = await ValidatePassword(email, password);
            if (!isValidPassword)
            {
                return (null);
            }

            var authClaims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = GetToken(authClaims);
            //var refreshToken = GetRefreshToken(authClaims);


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            //var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            return (tokenString);
        }


        public async Task<User> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user == null) return null;

            var isValidPassword = await ValidatePassword(email, password);
            if (!isValidPassword)
            {
                return null;
            }

            return user;
        }




        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: authClaims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

            );
            return token;
        }


        //private JwtSecurityToken GetRefreshToken(List<Claim> authClaims)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //    var refreshToken = new JwtSecurityToken(
        //        issuer: _configuration["Jwt:Issuer"],
        //        audience: _configuration["Jwt:Issuer"],
        //        claims: authClaims,
        //        expires: DateTime.Now.AddDays(2),
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

        //    );
        //    return refreshToken;
        //}
        public async Task<User> Update(UserUpdateRequest request)
        {
            var existingUser = await _context.Users.FindAsync(request.UserID);
            if (existingUser == null)
            {
                return null; // Người dùng không tồn tại
            }

            // Cập nhật các trường thông tin
            if (!string.IsNullOrEmpty(request.UserName)) existingUser.UserName = request.UserName;
            if (!string.IsNullOrEmpty(request.Email)) existingUser.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Address)) existingUser.Address = request.Address;
            if (!string.IsNullOrEmpty(request.BirthDay) && DateTime.TryParse(request.BirthDay, out var birthday))
                existingUser.Birthday = birthday;
            if (!string.IsNullOrEmpty(request.Gender)) existingUser.Gender = request.Gender;

            existingUser.UpdatedAt = DateTime.UtcNow; // Cập nhật thời gian thay đổi

            // Cập nhật thời gian thay đổi
            existingUser.UpdatedAt = DateTime.UtcNow;

            // Cập nhật vào cơ sở dữ liệu
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser; // Trả về người dùng đã cập nhật
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest request)
        {
            // Tìm người dùng trong cơ sở dữ liệu
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return false; // Người dùng không tồn tại
            }

            // Kiểm tra mật khẩu cũ
            var isValidPassword = await ValidatePassword(user.Email, request.CurrentPassword);
            if (!isValidPassword)
            {
                return false; // Mật khẩu cũ không đúng
            }

            // Kiểm tra độ dài mật khẩu mới hoặc các quy tắc khác nếu cần
            if (string.IsNullOrEmpty(request.NewPassword) || request.NewPassword.Length < 6)
            {
                return false; // Mật khẩu mới không hợp lệ
            }

            // Mã hóa mật khẩu mới
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            // Cập nhật thời gian thay đổi
            user.UpdatedAt = DateTime.UtcNow;

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true; // Đổi mật khẩu thành công
        }


    }

}
