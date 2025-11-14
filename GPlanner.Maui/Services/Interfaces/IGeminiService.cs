public interface IGeminiService
{
    public Task<bool> GeneratePlanningAsync(int userId);
}