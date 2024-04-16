using ToDoListContracts;

namespace ToDoList.WebUi.ApiClients
{
	public interface IToDoListApiClient
	{
		Task<Guid> AddTaskItemAsync(DateTime date, string content);
		Task<IEnumerable<TaskItemDto>> GetQuestionsForDateAsync(DateTime date);
		Task DeleteItemAsync(Guid id);
		Task UpdateItemAsync(Guid id, string content, string status);


	}
}
