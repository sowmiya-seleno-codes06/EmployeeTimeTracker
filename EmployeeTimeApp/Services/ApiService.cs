using EmployeeTimeApp.Models;
using System.Net.Http;
using System.Text.Json;

namespace EmployeeTimeApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesAsync()
        {
            string url = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            var response = await _httpClient.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var entries = JsonSerializer.Deserialize<List<TimeEntry>>(response, options);
            return entries ?? new List<TimeEntry>();
        }
    }
}
