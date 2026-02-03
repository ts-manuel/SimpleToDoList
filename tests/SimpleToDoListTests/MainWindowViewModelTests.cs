namespace SimpleToDoListTests;

public class MainWindowViewModelTests
{
    [Fact]
    public void AddItemCommand_CanExecute_ReturnsFalse_WhenNewItemContentIsNullOrWhitespace()
    {
        // Arrange
        var viewModel = new SimpleToDoList.ViewModels.MainWindowViewModel();

        // Act & Assert
        viewModel.NewItemContent = null;
        Assert.False(viewModel.AddItemCommand.CanExecute(null));

        viewModel.NewItemContent = "";
        Assert.False(viewModel.AddItemCommand.CanExecute(null));

        viewModel.NewItemContent = "   ";
        Assert.False(viewModel.AddItemCommand.CanExecute(null));
    }

    [Fact]
    public void AddItemCommand_CanExecute_ReturnsTrue_WhenNewItemContentIsValid()
    {
        // Arrange
        var viewModel = new SimpleToDoList.ViewModels.MainWindowViewModel();

        // Act
        viewModel.NewItemContent = "New Task";

        // Assert
        Assert.True(viewModel.AddItemCommand.CanExecute(null));
    }

    [Fact]
    public void AddItemCommand_Executes_AddsNewItemToToDoItems()
    {
        // Arrange
        var viewModel = new SimpleToDoList.ViewModels.MainWindowViewModel();
        viewModel.NewItemContent = "New Task";

        // Act
        viewModel.AddItemCommand.Execute(null);

        // Assert
        Assert.Single(viewModel.ToDoItems);
        Assert.Equal("New Task", viewModel.ToDoItems[0].Content);
        Assert.Null(viewModel.NewItemContent); // NewItemContent should be cleared after adding
    }
}