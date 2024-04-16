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
			var address = "TaskItems?date=" + date;
			var response = await client.GetAsync(address);
			var listOfTasksForDay = await response.Content.ReadFromJsonAsync<IEnumerable<TaskItemDto>>();
			return listOfTasksForDay;
		}

		public async Task UpdateItemAsync(Guid id, string content, string status)
		{
			var client = CreateHttpClient();

		var address = "TaskItems/" + id;

			var updateDto = new UpdateTaskItemDto();

			if (!string.IsNullOrEmpty(content))
				updateDto.Content = content;

			if (!string.IsNullOrEmpty(status))
				updateDto.Status = status;

			await client.PatchAsync(address, JsonContent.Create(updateDto));
		}
	}
}
