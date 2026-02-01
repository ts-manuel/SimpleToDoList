using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleToDoList.ViewModels;

/// <summary>
/// This is our ViewModel which represents a <see cref="Models.ToDoItem"/> 
/// </summary>
public partial class ToDoItemViewModel : ViewModelBase
{
    /// <summary>
    /// Gets or sets the checked state of the ToDoItem
    /// </summary>
    [ObservableProperty]
    private bool isChecked;

    /// <summary>
    /// Gets or sets the content of the ToDoItem
    /// </summary>
    [ObservableProperty]
    private string? content;

    /// <summary>
    /// Creates a new blank ToDoItemViewModel
    /// </summary>
    public ToDoItemViewModel()
    {
        // Empty
    }

    /// <summary>
    /// Creates a new ToDoItemViewModel for the given <see cref="Models.ToDoItem"/>
    /// </summary>
    /// <param name="item">The item to load</param>
    public ToDoItemViewModel(Models.ToDoItem item)
    {
        // Init the properties with the given values
        IsChecked = item.IsChecked;
        Content = item.Content;
    }

    /// <summary>
    /// Gets a ToDoItem of this Viewmodel
    /// </summary>
    public Models.ToDoItem GetToDoItem()
    {
        return new Models.ToDoItem()
        {
            IsChecked = this.IsChecked,
            Content = this.Content
        };
    }
}