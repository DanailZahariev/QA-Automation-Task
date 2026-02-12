using QaAutomationTask.Models;
using RestSharp;

namespace QaAutomationTask.Services;

public interface IUserApiService
{
    Task<RestResponse<UserResponseDto>> CreateUserAsync(UserDto user);
}