using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SimpleToDoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    /// <summary>
    /// Gets a collection of <see cref="ToDoItem"/> which allows adding and removing items
    /// </summary>
    public ObservableCollection<ToDoItemViewModel> ToDoItems { get; } = new ObservableCollection<ToDoItemViewModel>();

    /// <summary>
    /// Gets or sets the content for new Items to add. If this is not empty,
    /// the AddItemCommand will be enabled automatically.
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))] // This attribute will invalidate the command each time this property changes
    private string? newItemContent;

    /// <summary>
    /// Returns if a new Item can be added. We require to have the NewItem some Text
    /// </summary>
    private bool CanAddItem()
    {
        return !string.IsNullOrWhiteSpace(NewItemContent);
    }

    /// <summary>
    /// This command is used to add a new Item to the List
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        // Add the new item to the collection
        ToDoItems.Add(new ToDoItemViewModel()
        {
            Content = NewItemContent
        });

        // Clear the NewItemContent to reset the input field
        NewItemContent = null;
    }

    /// <summary>
    /// Removes the given Item from the list
    /// </summary>
    /// <param name="item">The item to remove</param>
    [RelayCommand]
    private void RemoveItem(ToDoItemViewModel item)
    {
        // remove the given Item from the list
        ToDoItems.Remove(item);
    }
}
