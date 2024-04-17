using ToDoList.WebApi.Models;

namespace ToDoList.WebApi.EF.Repositories
{
	public interface ITaskItemRepository
	{
		Task<IEnumerable<TaskItem>> GetAllForDateAsync(DateTime date);
		Task<TaskItem> GetByIdAsync(Guid id);
		Task AddAsync(TaskItem taskItem);
		Task UpdateAsync(TaskItem taskItem);
	}
}
