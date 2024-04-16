using System.Globalization;
using System.Net.Http.Json;
using ToDoListContracts;

namespace ToDoList.WebUi.ApiClients

{
    public class HttpToDoListApiClient : IToDoListApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpToDoListApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("ToDoListHttpClient");
            return client;
        }

        public async Task<Guid> AddTaskItemAsync(DateTime date, string content)
        {
            var client = CreateHttpClient();
            var address = "TaskItems";

            var response = await client.PostAsync(address, JsonContent.Create(new AddTaskItemDto()
            {
                Date = date,
                Content = content
            }));
            var body = await response.Content.ReadFromJsonAsync<Guid>();
            return body;

        }

        public async Task DeleteItemAsync(Guid id)
        {
            var client = CreateHttpClient();
            var address = "TaskItems/" + id;
            await client.DeleteAsync(address);
        }

        public async Task<IEnumerable<TaskItemDto>> GetQuestionsForDateAsync(DateTime date)
        {
            var client = CreateHttpClient();
            var address = "TaskItems?date=" + date.ToString(CultureInfo.InvariantCulture);
            var response = await client.GetAsync(address);
            var listOfTasksForDay = await response.Content.ReadFromJsonAsync<IEnumerable<TaskItemDto>>();
            return listOfTasksForDay;
        }

        public async Task UpdateItemContentAsync(Guid id, string content)
        {
            var client = CreateHttpClient();

            var address = "TaskItems/" + id + "/content";
            await client.PutAsync(address, JsonContent.Create(new UpdateTaskItemContentDto()
            {
                Content = content,
            }));
        }

        public async Task UpdateItemStatusAsync(Guid id, string status)
        {
            var client = CreateHttpClient();

            var address = "TaskItems/" + id + "/status";
            await client.PutAsync(address, JsonContent.Create(new UpdateTaskItemStatusDto()
            {
                Status = status,
            }));
        }
    }
}
