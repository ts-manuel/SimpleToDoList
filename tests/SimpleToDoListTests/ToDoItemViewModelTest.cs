namespace SimpleToDoListTests;

public class ToDoItemViewModelTest
{
    [Fact]
    public void ToDoItemViewModel_Constructor_InitializesPropertiesCorrectly()
    {
        // Arrange
        var toDoItem = new SimpleToDoList.Models.ToDoItem
        {
            IsChecked = true,
            Content = "Test Content"
        };

        // Act
        var viewModel = new SimpleToDoList.ViewModels.ToDoItemViewModel(toDoItem);

        // Assert
        Assert.True(viewModel.IsChecked);
        Assert.Equal("Test Content", viewModel.Content);
    }

    [Theory]
    [InlineData(true, "Sample Task 1")]
    [InlineData(false, "Sample Task 2")]
    public void ToDoItemViewModel_Properties_SetAndGetCorrectly(bool isChecked, string content)
    {
        // Arrange
        var toDoItem = new SimpleToDoList.Models.ToDoItem
        {
            IsChecked = isChecked,
            Content = content
        };

        // Act
        var viewModel = new SimpleToDoList.ViewModels.ToDoItemViewModel(toDoItem);

        // Assert
        Assert.Equal(isChecked, viewModel.IsChecked);
        Assert.Equal(content, viewModel.Content);
    }
}