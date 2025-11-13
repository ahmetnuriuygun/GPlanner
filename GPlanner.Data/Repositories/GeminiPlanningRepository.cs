using GenerativeAI;
using GPlanner.Core.Model;
using GPlanner.Core.Repositories;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using GenerativeAI.Types;
using System.Globalization;

namespace GPlanner.Data.Repositories
{
    public class GeminiPlanningRepository : IGeminiPlanningRepository
    {
        private readonly GenerativeModel _geminiModel;
        private readonly IScheduledTaskRepository _scheduledTaskRepository;

        public GeminiPlanningRepository(GenerativeModel geminiModel, IScheduledTaskRepository scheduledTaskRepository)
        {
            _geminiModel = geminiModel;
            _scheduledTaskRepository = scheduledTaskRepository;
        }

        public async Task<List<DailyPlanItem>> GenerateAndSavePlanAsync(int userId, List<UserTask> tasks)
        {
            // 1️⃣ Clean the input
            var cleanTasks = tasks.Select(t => new
            {
                t.TaskId,
                t.Title,
                t.Description,
                t.Type,
                Date = t.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                t.Priority
            }).ToList();

            string userTaskJson = JsonSerializer.Serialize(cleanTasks);
            string tomorrowDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime generationTime = DateTime.UtcNow;

            // 2️⃣ System Prompt
            string systemPrompt = $@"
You are an efficient academic planner AI.
Generate a 7-day detailed plan for the student (UserID: {userId}) starting tomorrow ({tomorrowDate}).

Task Types:
0 = GROTETOETS (Major Exam)
1 = HUISWERK (Homework)
2 = AFSPRAAK (Appointment)
3 = KLEINETOETS (Minor Test)
4 = FREETIME (Free Time)

RULES:
1. Exams (Type 0 or 3): Study at least 3 hours the day BEFORE the due date.
2. Appointments/Reservations (Type 2 or 4): Add 30-minute 'Travel/Prep' before and after.
3. Homework (Type 1): Schedule 1–2 hour sessions before the due date.
4. Include 'OriginalTaskTitle' and 'OriginalUserTaskId' in every task.
5. Output valid JSON conforming to this C# structure:
   GeminiPlanWrapper → DailyPlanItems → Tasks.
";

            string fullPrompt = systemPrompt + "\n\nStudent Tasks:\n" + userTaskJson;

            try
            {
                var request = new GenerateContentRequest();
                request.UseJsonMode<GeminiPlanWrapper>();
                request.AddText(fullPrompt);

                var planWrapper = await _geminiModel.GenerateObjectAsync<GeminiPlanWrapper>(request);

                if (planWrapper?.DailyPlanItems == null || !planWrapper.DailyPlanItems.Any())
                {
                    throw new Exception("Gemini generated an empty or malformed plan.");
                }

                var dbPlanItems = planWrapper.DailyPlanItems.Select(p => new DailyPlanItem
                {
                    Date = p.Date,
                    DayOfWeek = p.DayOfWeek,
                    LastGenerated = generationTime,
                    Tasks = p.Tasks.Select(t => new ScheduledTask
                    {
                        Title = t.Title,
                        ActivityType = t.ActivityType,
                        StartTime = t.StartTime.TimeOfDay,
                        EndTime = t.EndTime.TimeOfDay,
                        Description = t.Description,
                        IsCompleted = false,
                        OriginalTaskTitle = t.OriginalTaskTitle,
                        OriginalUserTaskId = t.OriginalUserTaskId,
                        DailyPlanDate = p.Date
                    }).ToList()
                }).ToList();

                // 6️⃣ Save to DB
                await _scheduledTaskRepository.SaveDailyPlanAsync(dbPlanItems);

                return dbPlanItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gemini Planning Error: {ex}");
                throw new InvalidOperationException(
                    "Failed to generate schedule from AI. Check API key and JSON schema.", ex);
            }
        }
    }
}
