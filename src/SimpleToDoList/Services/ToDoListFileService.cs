using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using SimpleToDoList.Models;

public static class ToDoListFileService
{
    // This is a hard coded path to the file.
    // It may not be available on every platform.
    // In your real world App yuo may want to make this configurable.
    private static readonly string jsonFileName = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Avalonia.SimpleToDoList", "todolost.txt");

    public static async Task SaveToFileAsync(IEnumerable<ToDoItem> items)
    {
        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(jsonFileName)!);

        // We use a FileStream to wite all items to disc
        using (var fs = File.Create(jsonFileName))
        {
            await JsonSerializer.SerializeAsync(fs, items);
        }
    }

    /// <summary>
    /// Loads the file from disc and returns the items stored inside
    /// </summary>
    /// <returns>An IEnumerable of items loaded or null in case the file was not found</returns>
    public static async Task<IEnumerable<ToDoItem>?> LoadFromFileAsync()
    {
        try
        {
            // We try to read the saved file and return the ToDoItemsList if successful
            using (var fs = File.OpenRead(jsonFileName))
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<ToDoItem>>(fs);
            }
        }
        catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
        {
            // In case the file was not found, we simply return null
            return null;
        }
    }
}