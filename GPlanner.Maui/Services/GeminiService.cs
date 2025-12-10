using System.Net.Http;
using System.Threading.Tasks;
using System;
using GPlanner.Maui.Interfaces;
using System.Text;

public class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl;

    public GeminiService()
    {
        _httpClient = new HttpClient();
#if ANDROID
        BaseUrl = "http://10.0.2.2:8080/api/Planning/generate";
#else
        BaseUrl = "http://localhost:8080/api/Planning/generate";
#endif
    }

    public async Task<bool> GeneratePlanningAsync(int userId)
    {
        var url = $"{BaseUrl}/{userId}";
        try
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error generating planning: {ex.Message}");
            return false;
        }
    }

}