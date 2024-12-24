using Nitaj.Application.Interfaces;

namespace Nitaj.Tests
{
    public class ToDoServiceTests
    {
        private readonly IToDoService _toDoService;
        public ToDoServiceTests(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public async void CompleteToDo_When_Id_1()
        {
            // Arrange
            int id = 1;
            // Act
            var result = await _toDoService.CompleteToDo(id);
            var expectedResult = true;
            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        public async void CompleteToDo_When_Id_0()
        {
            // Arrange
            int id = 0;
            // Act
            var result = await _toDoService.CompleteToDo(id);
            var expectedResult = true;
            // Assert
            Assert.That(expectedResult, Is.Not.EqualTo(result));
        }

    }
}
