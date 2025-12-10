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
        BaseUrl = "http://10.0.2.2:8080/api/userTask";
#else
        BaseUrl = "http://localhost:8080/api/userTask";
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

    public async Task<bool> CreateTaskAsync(UserTask newTask)
    {
        var url = $"{BaseUrl}";
        try
        {
            var jsonContent = JsonSerializer.Serialize(newTask);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var createdTask = JsonSerializer.Deserialize<UserTask>(content, options);

                if (createdTask != null)
                {
                    newTask.TaskId = createdTask.TaskId;
                }
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error creating task: {ex.ToString()}");
            return false;
        }
    }


    public async Task<bool> UpdateTaskAsync(UserTask updatedTask)
    {
        var url = $"{BaseUrl}/{updatedTask.TaskId}";
        try
        {
            var jsonContent = JsonSerializer.Serialize(updatedTask);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, httpContent);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating task with ID {updatedTask.TaskId}: {ex.ToString()}");
            return false;
        }
    }


    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        var url = $"{BaseUrl}/{taskId}";
        try
        {
            var response = await _httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting task with ID {taskId}: {ex.ToString()}");
            return false;
        }
    }

    public async Task<bool> ArchiveTaskAsync(int taskId)
    {
        var url = $"{BaseUrl}/archive/{taskId}";
        try
        {
            var response = await _httpClient.PostAsync(url, null);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error archiving task with ID {taskId}: {ex.ToString()}");
            return false;
        }
    }
}