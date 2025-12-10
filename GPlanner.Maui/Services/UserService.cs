using GPlanner.Core.Model;
using System.Text.Json;
using System.Text;
using AutoMapper;
using GPlanner.Maui.Services.Dtos;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly string BaseUrl;

    public UserService(IMapper mapper)
    {
        _httpClient = new HttpClient();
        _mapper = mapper;
#if ANDROID
        BaseUrl = "http://10.0.2.2:8080/api/User";
#else
        BaseUrl = "http://localhost:8080/api/User";
#endif
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var url = $"{BaseUrl}/{id}";
        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var userDto = JsonSerializer.Deserialize<UserDto>(content, options);
                return _mapper.Map<User>(userDto) ?? new User();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching user: {ex.Message}");
        }
        return new User();
    }
}