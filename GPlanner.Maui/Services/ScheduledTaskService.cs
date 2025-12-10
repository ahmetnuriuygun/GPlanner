using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net; // Used for EnsureSuccessStatusCode
using GPlanner.Maui.Interfaces;



public class ScheduledTaskService : IScheduledTaskService
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl;

    public ScheduledTaskService()
    {
        _httpClient = new HttpClient();

        // The API endpoint route is [HttpPut("/update/completed/{taskId}")] in ScheduledTaskController
#if ANDROID
        BaseUrl = "http://10.0.2.2:8080/api/ScheduledTask";
#else
        BaseUrl = "http://localhost:8080/api/ScheduledTask";
#endif
    }

    public async Task<bool> UpdateScheduledTaskAsync(int taskId)
    {
        // The final URL should be: /api/ScheduledTask/update/completed/{taskId}
        var url = $"{BaseUrl}/completed/{taskId}";

        try
        {
            // FIX: Using PutAsync(url, null) is sufficient since the API only reads the ID from the URL.
            var response = await _httpClient.PutAsync(url, null);

            // Throws an exception if the status code is not 2xx
            response.EnsureSuccessStatusCode();
            return true; // FIX: Explicitly return true on success (Fix CS0161)
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating scheduled task: {ex.Message}");
            return false; // FIX: Ensure false is returned on error (Fix CS0161)
        }
    }
}