using Nitaj.Application.DTOs.ToDoItems;

namespace Nitaj.Application.Interfaces
{
    public interface IToDoService
    {
        bool Create(ToDoItemDto toDoItemDto);
        IEnumerable<ToDoItemDto> GetAll();
        IEnumerable<ToDoItemDto> GetPendingToDos();
        Task<bool> CompleteToDo(int id);
        ToDoItemDto GetById(int id);
    }
}
