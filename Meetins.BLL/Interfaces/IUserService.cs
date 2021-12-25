using Meetins.BLL.DTOs;
using Meetins.BLL.DTOs.Requests;
using Meetins.BLL.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<AutheticateResponseDto> AuthenticateUser(AuthenticateRequestDto autheticateRequest);
        Task RegisterUserAsync(UserDto user);
        Task DeleteAllRefreshTokenByUserId(Guid userId);
        Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest);
        Task<bool> CheckUserByEmailOrPhoneNumber(string email, string phoneNumber);
        Task<UserDto> CheckUserByEmail(string email);
        Task<UserDto> CheckUserByPhone(string phone);  
    }
}
