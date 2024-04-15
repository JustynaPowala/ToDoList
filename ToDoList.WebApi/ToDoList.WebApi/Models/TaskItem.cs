namespace ToDoList.WebApi.Models
{
	public class TaskItem
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public string Content { get; set; }
		public TaskItemStatus Status { get; set; }
		public DateTime CreatedDate { get; set; } // to arrange the list in the order of addition
	}
}
