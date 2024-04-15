using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Models;

namespace ToDoList.WebApi.EF.Repositories
{
	public class TaskItemRepository : ITaskItemRepository
	{
		private readonly ToDoListDbContext _dbContext;
		private readonly DbSet<TaskItem> _tasks;


		public TaskItemRepository(ToDoListDbContext dbContext)
		{
			_dbContext = dbContext;
			_tasks = _dbContext.TaskItems;
		}

		public async Task AddAsync(TaskItem taskItem)
		{
			await _tasks.AddAsync(taskItem);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<TaskItem>> GetAllForDateAsync(DateTime date)
		{
			return await _tasks.Where(x=>x.Date.Date == date.Date && x.Status != TaskItemStatus.Deleted).OrderBy(x => x.CreatedDate).ToListAsync();

		}

		public async Task<TaskItem> GetByIdAsync(Guid id)
		{
			return await _tasks.SingleOrDefaultAsync(x => x.Id == id);
		}

		public async Task UpdateAsync(TaskItem taskItem)
		{
			_dbContext.TaskItems.Update(taskItem);
			await _dbContext.SaveChangesAsync();
		}
	}
}



