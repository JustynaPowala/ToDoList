namespace ToDoList.WebApi.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityTypeName, Guid id) : base($"Cannot find entity of type {entityTypeName} for given id {id}.") { }
    }
}
