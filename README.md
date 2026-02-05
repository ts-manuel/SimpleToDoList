# Simple TODO List

[![stars](https://img.shields.io/github/stars/ts-manuel/SimpleToDoList.svg)](https://github.com/ts-manuel/SimpleToDoList/stargazers)
[![forks](https://img.shields.io/github/forks/ts-manuel/SimpleToDoList.svg)](https://github.com/ts-manuel/SimpleToDoList/forks)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/ts-manuel/SimpleToDoList/ci.yml?logo=github&label=CI)
[![license](https://img.shields.io/github/license/ts-manuel/SimpleToDoList.svg)](LICENSE)
[![latest](https://img.shields.io/github/v/release/ts-manuel/SimpleToDoList.svg)](https://github.com/ts-manuel/SimpleToDoList/releases/latest)
[![downloads](https://img.shields.io/github/downloads/ts-manuel/SimpleToDoList/total)](https://github.com/ts-manuel/SimpleToDoList/releases)


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
