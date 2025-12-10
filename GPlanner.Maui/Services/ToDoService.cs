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
    private readonly IMapper _mapper;

    public ToDoService(IMapper mapper)
    {
        _httpClient = new HttpClient();
        _mapper = mapper;
    }

    public async Task<List<DailyPlanItem>> GetDailyPlansAsync()
    {

        var url = "http://localhost:8080/api/Dailyplan";
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var dtos = JsonSerializer.Deserialize<List<DaillyPlanDto>>(content, options) ?? new List<DaillyPlanDto>();
            return _mapper.Map<List<DailyPlanItem>>(dtos);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching daily plans: {ex.Message}");
        }
        return new List<DailyPlanItem>();

    }
}