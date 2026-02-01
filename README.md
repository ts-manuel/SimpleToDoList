# Simple TODO List

[![develop-tests](https://github.com/ts-manuel/SimpleToDoList/actions/workflows/run-tests.yml/badge.svg?branch=develop)](https://github.com/ts-manuel/SimpleToDoList/actions/workflows/run-tests.yml)

This project is based on the Avalonia SimpleToDoList app, it's main purpose is to lear how to develop graphical applications under Linux with .NET Core.
On top of the example I've added testing and GitHub actions.


## Requirements
This project uses .NET Core 10.0 https://dotnet.microsoft.com/en-us/download

### Manual Linux Setup
- Download the SDK from the link above
- Create a new directory for example /opt/dotnet
- extract downloaded file in that directory
- Add following lines to ~/.bashrc
```bash
export DOTNET_ROOT="/opt/dotnet"
export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"
```

## Notes
This are the steps to recreate the project from scratch

```bash
# Install Avalonia templates
dotnet new install Avalonia.Templates

# Create new Avalonia example project
dotnet new avalonia.mvvm -f net10.0 -o src/SimpleToDoList

# Install xUnit templates
dotnet new install xunit.v3.templates

# Create new xUnit project
dotnet new xunit3 --language C# -f net10.0 -o tests/SimpleToDoListTests -n SimpleToDoListTests

# Add reference to the SimpleToDoList project
dotnet reference add src/SimpleToDoList/SimpleToDoList.csproj --project tests/SimpleToDoListTests/SimpleToDoListTests.csproj

# Add solution file
dotnet new sln --name SimpleToDoList

# Add projects to solution
dotnet sln add src/SimpleToDoList
dotnet sln add tests/SimpleToDolIstTests
```
