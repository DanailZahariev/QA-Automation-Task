using QaAutomationTask.Config;
using QaAutomationTask.Models;
using RestSharp;

namespace QaAutomationTask.Services;

public class UserApiService : IUserApiService
{
    private readonly RestClient _client;

    public UserApiService()
    {
        var baseUrl = ConfigReader.AppSettings.ApiSettings.BaseUrl;

        var options = new RestClientOptions(baseUrl)
        {
            Timeout = TimeSpan.FromSeconds(ConfigReader.AppSettings.ApiSettings.TimeoutSeconds)
        };
        _client = new RestClient(options);
    }

    public async Task<RestResponse<UserResponseDto>> CreateUserAsync(UserDto user)
    {
        var endpoint = ConfigReader.AppSettings.ApiSettings.Endpoints.AddUser;
        var request = new RestRequest(endpoint, Method.Post);

        request.AddJsonBody(user);
        return await _client.ExecuteAsync<UserResponseDto>(request);
    }
}