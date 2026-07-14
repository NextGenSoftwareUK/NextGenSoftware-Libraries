# NextGenSoftware.CLI.Engine

**A feature-rich CLI engine for .NET** — colour output, animations, spinners, tables, progress bars, ASCII art, interactive prompts, and more. The terminal backbone of the [OASIS Omniverse](https://oasisomniverse.one) ecosystem, used in HoloNET, the OASIS API CLI, STAR ODK, and OurWorld.

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.CLI.Engine.svg)](https://www.nuget.org/packages/NextGenSoftware.CLI.Engine)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot**. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Features

- Rich **colour output** — full 16-colour and RGB console support
- **Animated working-message spinners** with inline DONE on the same line
- **Formatted tables** — single and multi-column layouts
- **Progress bars** with percentage and custom messages
- **ASCII art banners** for CLI app headers
- **Interactive prompts** — validated text, password, confirmation, colour pickers
- `SupressConsoleLogging` — toggle all output on/off at runtime
- Cross-platform: Windows, macOS, Linux
- Multi-target: **.NET 8, .NET 9, .NET 10**

---

## Installation

```bash
dotnet add package NextGenSoftware.CLI.Engine
```

---

## Working Message Spinner

```csharp
CLIEngine.BeginWorkingMessage("Connecting to Holochain conductor...");
await client.ConnectAsync();
CLIEngine.EndWorkingMessage(); // prints " DONE" on the same line
```

## Coloured Messages

```csharp
CLIEngine.ShowSuccessMessage("All packages published!");
CLIEngine.ShowErrorMessage("Connection failed.");
CLIEngine.ShowWarningMessage("Proceeding with defaults.");
CLIEngine.ShowMessage("Custom text", ConsoleColor.Cyan);
```

## Tables

```csharp
CLIEngine.ShowDivider();
CLIEngine.ShowTable(
    new[] { "Package", "Version", "Status" },
    new[] {
        new[] { "NextGenSoftware.WebSocket", "2.0.1", "Published" },
        new[] { "NextGenSoftware.Logging",   "2.0.1", "Published" },
    }
);
```

## Interactive Input

```csharp
string name  = CLIEngine.GetValidInput("Enter your name:");
string pass  = CLIEngine.GetValidPassword("Enter password:");
bool confirm = CLIEngine.GetConfirmation("Are you sure? (y/n)");
```

---

## Method Reference

| Method | Description |
|---|---|
| `ShowMessage` | Print text in any console colour |
| `ShowSuccessMessage` | Green success output |
| `ShowErrorMessage` | Red error output with optional exception |
| `ShowWarningMessage` | Yellow warning output |
| `BeginWorkingMessage` | Start animated spinner |
| `EndWorkingMessage` | Stop spinner, print DONE inline |
| `ShowDivider` | Print a horizontal rule |
| `ShowTable` | Render a formatted table |
| `ShowProgressBar` | Progress bar for long operations |
| `WriteAsciMessage` | Print ASCII art banner |
| `GetValidInput` | Prompt for validated text |
| `GetValidPassword` | Prompt for masked password |
| `GetConfirmation` | Prompt for yes/no |
| `GetValidColour` | Prompt for a console colour |

---

- [oasisomniverse.one](https://oasisomniverse.one) | [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

MIT — Copyright (c) NextGen Software Ltd 2019 - 2026