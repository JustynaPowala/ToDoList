using ToDoList.WebApi.Exceptions;

namespace ToDoList.WebApi.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public TaskItemStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } // to arrange the list in the order of addition
        public static TaskItem Create(Guid id, DateTime date, string content)
        {
            var taskItem = new TaskItem
            {
                Id = id,
                Date = date,
                Status = TaskItemStatus.ToDo,
                CreatedDate = DateTime.UtcNow

            };
            taskItem.DefineContent(content);
            return taskItem;
        }
        public void DefineContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new DomainValidationException("Content is required.");
            }
            Content = content;
        }
        public void Complete()
        {
            if (Status == TaskItemStatus.Done)
            {
                throw new DomainValidationException("Status is already done.");
            }
            if (Status == TaskItemStatus.Deleted)
            {
                throw new DomainValidationException("You cannot complete deleted task.");
            }
            Status = TaskItemStatus.Done;
        }
        public void MoveBackToToDo()
        {
            if (Status == TaskItemStatus.ToDo)
            {
                throw new DomainValidationException("Status is already to do.");
            }
            if (Status == TaskItemStatus.Deleted)
            {
                throw new DomainValidationException("You cannot complete deleted task.");
            }
            Status = TaskItemStatus.ToDo;
        }
        public void Delete()
        {
            if (Status == TaskItemStatus.Deleted)
            {
                throw new DomainValidationException("Task is already deleted.");
            }
            Status = TaskItemStatus.Deleted;
        }
    }
}
