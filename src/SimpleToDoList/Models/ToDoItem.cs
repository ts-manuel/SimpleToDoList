
namespace SimpleToDoList.Models;

/// <summary>
/// This is uor Model for a simple ToDoItem
/// </summary>
public class ToDoItem
{
    /// <summary>
    /// Gets or sets the checked state of the ToDoItem
    /// </summary>
    public bool IsChecked { get; set; }

    /// <summary>
    /// Gets or sets the content of the ToDoItem
    /// </summary>
    public string? Content { get; set; }
}