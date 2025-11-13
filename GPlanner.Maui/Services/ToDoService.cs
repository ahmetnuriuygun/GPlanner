using GPlanner.Core.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using GPlanner.Maui.Services.Dtos;
using System.Linq;



public class ToDoService : IToDoService
{
    private readonly HttpClient _httpClient;

    public ToDoService()
    {
        _httpClient = new HttpClient();


    }

    public async Task<List<DailyPlanItem>> GetDailyPlansAsync()
    {

        var url = "http://localhost:5036/api/Dailyplan";
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<List<DailyPlanItem>>(content, options) ?? new List<DailyPlanItem>();

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching daily plans: {ex.Message}");
        }
        return new List<DailyPlanItem>();

    }
}