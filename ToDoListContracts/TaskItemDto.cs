using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListContracts
{
	internal class TaskItemDto
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public string Content { get; set; }
		public string Status { get; set; }
	}
}
