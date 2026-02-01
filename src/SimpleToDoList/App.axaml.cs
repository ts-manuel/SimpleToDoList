using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using SimpleToDoList.ViewModels;
using SimpleToDoList.Views;
using System.Runtime.CompilerServices;
using System;
using System.Threading.Tasks;

namespace SimpleToDoList;

public partial class App : Application
{
    private readonly MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
    private bool canClose;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            // Listen to the ShutdownRequested-event
            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }

        base.OnFrameworkInitializationCompleted();

        // Init the MainViewModel
        await InitMainViewModelAsync();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        e.Cancel = !canClose; // cancel closing event first time

        if (!canClose)
        {
            // To save the items, we map them to the ToDoItem-Model which is better suited for I/O operations
            var itemsToSave = mainWindowViewModel.ToDoItems.Select(item => item.GetToDoItem());
            await ToDoListFileService.SaveToFileAsync(itemsToSave);

            // Set _canClose to true and Close this Window again
            canClose = true;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }

    // Optional: Load data from disc
    private async Task InitMainViewModelAsync()
    {
        // get the items to load
        var itemsLoaded = await ToDoListFileService.LoadFromFileAsync();

        if (itemsLoaded is not null)
        {
            foreach (var item in itemsLoaded)
            {
                mainWindowViewModel.ToDoItems.Add(new ToDoItemViewModel(item));
            }
        }
    }
}