using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using ToDoList.WebApi.EF.Repositories;
using ToDoList.WebApi.Models;
using ToDoList.WebApi.Validators;
using ToDoListContracts;

namespace ToDoList.WebApi.Controllers
{


	[ApiController]
	[Route("taskitems")]
	public class TaskItemsController
	{
		private readonly ITaskItemRepository _taskItemRepository;
		private TaskItemDtoValidator _taskItemValidator = new TaskItemDtoValidator();

		public TaskItemsController(ITaskItemRepository taskItemRepository)
		{
			_taskItemRepository = taskItemRepository;
		}

		[HttpPost("")]
		public async Task<Guid> AddTaskItemAsync([FromBody] AddTaskItemDto taskDto)
		{
			_taskItemValidator.Validate(taskDto.Content);

			var id = Guid.NewGuid();

			var newTaskItem = new TaskItem
			{
				Id = id,
				Date = taskDto.Date.Date,
				Content = taskDto.Content,
				Status = TaskItemStatus.ToDo,
				CreatedDate = DateTime.UtcNow,
			};


			await _taskItemRepository.AddAsync(newTaskItem);

			return id;
		}

		[HttpGet("")]
		public async Task<Guid> AddTaskItemAsync([FromBody] AddTaskItemDto taskDto)
		{
			_taskItemValidator.Validate(taskDto.Content);

			var id = Guid.NewGuid();

			var newTaskItem = new TaskItem
			{
				Id = id,
				Date = taskDto.Date.Date,
				Content = taskDto.Content,
				Status = TaskItemStatus.ToDo,
				CreatedDate = DateTime.UtcNow,
			};


			await _taskItemRepository.AddAsync(newTaskItem);

			return id;
		}



	}
}
