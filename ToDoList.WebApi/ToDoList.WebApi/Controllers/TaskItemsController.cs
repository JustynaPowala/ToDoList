using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using ToDoList.WebApi.EF.Repositories;
using ToDoList.WebApi.Exceptions;
using ToDoList.WebApi.Models;

using ToDoListContracts;

namespace ToDoList.WebApi.Controllers
{


	[ApiController]
	[Route("[controller]")]
	public class TaskItemsController
	{
		private readonly ITaskItemRepository _taskItemRepository;

		public TaskItemsController(ITaskItemRepository taskItemRepository)
		{
			_taskItemRepository = taskItemRepository;
		}

		[HttpPost("")]
		public async Task<Guid> AddTaskItemAsync([FromBody] AddTaskItemDto taskDto)
		{
			
			var id = Guid.NewGuid();
			var newTaskItem = TaskItem.Create(id, taskDto.Date.Date, taskDto.Content);

			await _taskItemRepository.AddAsync(newTaskItem);

			return id;
		}

		[HttpGet("{taskId}")]
		public async Task<TaskItemDto> GetTaskItemAsync([FromRoute] Guid taskId)
		{
			var taskItem = await _taskItemRepository.GetByIdAsync(taskId);

			var ti = Convert(taskItem);
		
			return ti;
		}

		[HttpGet("")]
		public async Task<IEnumerable<TaskItemDto>> GetTaskItemsForDate([FromQuery] DateTime date)
		{
			var tasks = await _taskItemRepository.GetAllForDateAsync(date.Date);

			var tasksDto= tasks.Select(x=> Convert(x));
			
			return tasksDto;
		}

		[HttpPut("{taskId}/content")]   
		public async Task UpdateContentAsync([FromRoute] Guid taskId, [FromBody] UpdateTaskItemContentDto taskItem)
		{
			var task = await _taskItemRepository.GetByIdAsync(taskId);

			task.DefineContent(taskItem.Content);

			await _taskItemRepository.UpdateAsync(task);

		}
		
		[HttpPut("{taskId}/status")] 
        public async Task UpdateStatusAsync([FromRoute] Guid taskId, [FromBody] UpdateTaskItemStatusDto taskItem)
        {
            if (Enum.TryParse<TaskItemStatus>(taskItem.Status, true, out var s) == false)
            {
				throw new ArgumentException($"Status {taskItem.Status} is not valid.",nameof(taskItem));
            }

            var task = await _taskItemRepository.GetByIdAsync(taskId);
            switch (s)
			{
				case TaskItemStatus.Done:
					task.Complete();
					break;
				case TaskItemStatus.ToDo:
					task.MoveBackToToDo();
				break;
				default:
					throw new ArgumentOutOfRangeException("Only 'Done' or 'ToDo' statuses are valid.");

			}

            await _taskItemRepository.UpdateAsync(task);

        }

        [HttpDelete("{taskId}")]
		public async Task DeleteTaskAsync([FromRoute] Guid taskId)
		{

			var task = await _taskItemRepository.GetByIdAsync(taskId);
			

			task.Delete();

			await _taskItemRepository.UpdateAsync(task);

		}

		public static TaskItemDto Convert(TaskItem taskItem)
		{
            var ti = new TaskItemDto
            {
                Id = taskItem.Id,
                Date = taskItem.Date,
                Content = taskItem.Content,
                Status = taskItem.Status.ToString(),
                CreatedDate = taskItem.CreatedDate.Date,
            };

			return ti;

        }
    }
}
