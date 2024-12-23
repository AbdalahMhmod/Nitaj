using AutoMapper;
using Microsoft.Extensions.Logging;
using Nitaj.Application.DTOs.ToDoItems;
using Nitaj.Application.Interfaces;
using Nitaj.Domain.Entities;
using Nitaj.Infrastructure.UnitOfWorks;
using System.Linq.Expressions;

namespace Nitaj.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoService> _logger;
        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ToDoService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CompleteToDo(int id)
        {
            try
            {
                var toDoItemDto = GetById(id);
                toDoItemDto.IsCompleted = true;
                var toDoItem = _mapper.Map<TodoItem>(toDoItemDto);
                toDoItem.Id = toDoItemDto.Id;
                await _unitOfWork.RollbackAsync();
                _unitOfWork.Repository<TodoItem>().Update(toDoItem);
                return toDoItem.IsCompleted;
            }
            catch (Exception)
            {
                _logger.LogWarning("some thing went wrong");
                return false;
            }
        }

        public bool Create(ToDoItemDto toDoItemDto)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    Title = toDoItemDto.Title,
                    Description = toDoItemDto.Description
                };
                var toDoItem = _mapper.Map<TodoItem>(toDoItemDto);
                _unitOfWork.Repository<TodoItem>().Add(toDoItem);
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError("Something went wrong while creating a Todo item.", ex.Message);
                return false;
            }
        }

        public IEnumerable<ToDoItemDto> GetAll()
        {
            try
            {
                var toDoItems = _unitOfWork.Repository<TodoItem>().GetAll();
                var toDoItemDtos = _mapper.Map<IEnumerable<ToDoItemDto>>(toDoItems);
                return toDoItemDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating a Todo item.", ex.Message);
                return Enumerable.Empty<ToDoItemDto>();
            }
        }

        public ToDoItemDto GetById(int id)
        {
            try
            {
                Expression<Func<TodoItem, bool>> expression = td => td.Id == id;
                var toDoItem = _unitOfWork.Repository<TodoItem>().Find(expression);
                return _mapper.Map<ToDoItemDto>(toDoItem);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating a Todo item.", ex.Message);
                return new ToDoItemDto();
            }
        }

        public IEnumerable<ToDoItemDto> GetPendingToDos()
        {
            try
            {
                Expression<Func<TodoItem, bool>> expression = td => td.IsCompleted == false;
                var toDoItems = _unitOfWork.Repository<TodoItem>().FindAll(expression);
                var toDoItemDtos = _mapper.Map<IEnumerable<ToDoItemDto>>(toDoItems);
                return toDoItemDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating a Todo item.", ex.Message);
                return Enumerable.Empty<ToDoItemDto>();
            }
        }

    }
}
