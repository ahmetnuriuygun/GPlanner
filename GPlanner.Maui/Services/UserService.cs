using GPlanner.Core.Model;
using System.Text.Json;
using System.Text;
public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl;

    public UserService()
    {
        _httpClient = new HttpClient();
#if ANDROID
        BaseUrl = "http://10.0.2.2:5036/api/User";
#else
        BaseUrl = "http://localhost:5036/api/User";
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
                return JsonSerializer.Deserialize<User>(content, options) ?? new User();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching user: {ex.Message}");
        }
        return new User();
    }
}