using System.Text.Json;
using System.Text;
using GPlanner.Core.Model;
using GPlanner.Maui.Interfaces;
public class UserTaskService : IUserTaskService
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl;

    public UserTaskService()
    {
        _httpClient = new HttpClient();


#if ANDROID
        BaseUrl = "http://10.0.2.2:8080/api/usertask";
#else
        BaseUrl = "http://localhost:8080/api/usertask";
#endif
    }


    public async Task<List<UserTask>> GetTasksByUserIdAsync(int userId)
    {
        var url = $"{BaseUrl}/{userId}";
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
                return JsonSerializer.Deserialize<List<UserTask>>(content, options) ?? new List<UserTask>();
            }

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching tasks: {ex.Message}");
        }
        return new List<UserTask>();
    }

    public Task<UserTask> UpdateTaskAsync(UserTask updatedTask)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> CreateTaskAsync(UserTask newTask)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        var url = $"{BaseUrl}/{taskId}";
        try
        {
            // Call the API endpoint
            var response = await _httpClient.DeleteAsync(url);

            // Check for success (200 OK or 204 No Content)
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting task: {ex.Message}");
            return false;
        }
    }
}