using ToDoList.WebApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ToDoList.WebApi.Validators
{
	public class TaskItemDtoValidator
	{
		public void Validate(string content)
		{
			if (string.IsNullOrEmpty(content))
			{
				throw new DomainValidationException("Content is required");
			}

		}


	}
}
