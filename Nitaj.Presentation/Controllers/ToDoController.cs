using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nitaj.Application.DTOs.ToDoItems;
using Nitaj.Application.Interfaces;
using Nitaj.Infrastructure.UnitOfWorks;

namespace Nitaj.Presentation.Controllers
{
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoService _toDoService;
        public ToDoController(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<ToDoController> logger, IToDoService toDoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _toDoService = toDoService;

        }
        [HttpPost("api/todo")]
        public async Task<IActionResult> CreateToDo([FromBody] ToDoItemDto todoItemDto)
        {
            try
            {
                if (todoItemDto == null || string.IsNullOrEmpty(todoItemDto.Title))
                    return BadRequest("Title is required.");
                var isAdded = _toDoService.Create(todoItemDto);
                if (!isAdded)
                {
                    _logger.LogError("can not create new to do item");
                    return BadRequest("can not create new to do item");
                }
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("ToDo Created Successfully");
                return Ok("ToDo Created Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("some thing went wrong");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("api/todo")]
        public IActionResult GetAllToDos()
        {
            try
            {
                var toDos = _toDoService.GetAll();
                if (!toDos.Any())
                {
                    _logger.LogInformation("no Todo items.");
                    return NotFound("no Todo items.");
                }
                _logger.LogInformation("Fetching all Todo items.");
                return Ok(new { message = "Fetching all Todo items.", toDos });
            }
            catch (Exception ex)
            {
                _logger.LogWarning("some thing went wrong");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("api/todo/pending")]
        public IActionResult GetPendingToDos()
        {
            try
            {
                var pendingToDos = _toDoService.GetPendingToDos();
                if (!pendingToDos.Any())
                {
                    _logger.LogInformation("no Todo items.");
                    return NotFound("no Todo items.");
                }
                _logger.LogInformation("Fetching all Pending Todo items.");
                return Ok(new { message = "Fetching all Todo items.", pendingToDos });
            }
            catch (Exception ex)
            {
                _logger.LogWarning("some thing went wrong");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpPut("api/todo/{id}/complete")]
        public async Task<IActionResult> CompleteToDoItem(int id)
        {
            try
            {
                var isCompleted = await _toDoService.CompleteToDo(id);
                if (!isCompleted)
                {
                    _logger.LogWarning("can not complete this to do.");
                    return BadRequest(new { isCompleted, message = "can not complete this to do." });
                }
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Complete ToDo Item.");
                return Ok($"todo with id {id} is completed.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("some thing went wrong");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
