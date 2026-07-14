# NextGen Software Libraries

**The foundational .NET library suite powering the [OASIS Omniverse](https://oasisomniverse.one) ecosystem** — HoloNET, the OASIS API (85+ NuGet packages), STAR ODK, OurWorld, and more.

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.WebSocket.svg?label=WebSocket)](https://www.nuget.org/packages/NextGenSoftware.WebSocket)
[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.Logging.svg?label=Logging)](https://www.nuget.org/packages/NextGenSoftware.Logging)
[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.CLI.Engine.svg?label=CLI.Engine)](https://www.nuget.org/packages/NextGenSoftware.CLI.Engine)
[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.Utilities.svg?label=Utilities)](https://www.nuget.org/packages/NextGenSoftware.Utilities)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-purple.svg)](https://dotnet.microsoft.com)

---

## Overview

NextGen Software Libraries is a collection of lightweight, battle-tested .NET libraries that form the foundation of the entire OASIS Omniverse ecosystem. Every library is production-hardened across years of use in HoloNET (the Holochain .NET client), the OASIS API, STAR ODK (the No/Low Code generator), and OurWorld (the AR geo-location game-of-games).

All packages multi-target **.NET 8, .NET 9, and .NET 10** and are available on NuGet. They are designed to be dropped into any .NET project with minimal friction.

---

## Packages

### [NextGenSoftware.WebSocket](https://www.nuget.org/packages/NextGenSoftware.WebSocket)

A lightweight, high-performance WebSocket client with a clean event-driven API.

- `OnConnected`, `OnDisconnected`, `OnDataReceived`, `OnError`, and more lifecycle events
- Async/await-first design with robust reconnection handling
- Used internally by HoloNET to communicate with the Holochain conductor
- Multi-target: net8.0, net9.0, net10.0

```csharp
var socket = new WebSocketClient("wss://my-server/ws");
socket.OnConnected += (sender, args) => Console.WriteLine("Connected!");
socket.OnDataReceived += (sender, args) => Console.WriteLine($"Data: {args.RawData}");
await socket.Connect();
```

---

### [NextGenSoftware.Logging](https://www.nuget.org/packages/NextGenSoftware.Logging)

A flexible, pluggable logging library with a clean `ILogger`/`ILogProvider` abstraction.

- Register multiple simultaneous log providers (console, file, NLog, custom)
- Configurable log levels, formatting options, and async log dispatch
- The `ILogProvider` interface makes it trivial to plug in any logging backend
- Multi-target: net8.0, net9.0, net10.0

```csharp
var logger = new Logger();
logger.RegisterProvider(new DefaultLogger());           // console
logger.RegisterProvider(new FileLogger("app.log"));    // file

logger.Log("Starting up...", LogType.Info);
logger.Log("Something went wrong!", LogType.Error);
```

---

### [NextGenSoftware.Logging.NLog](https://www.nuget.org/packages/NextGenSoftware.Logging.NLog)

Drop-in NLog implementation of `ILogProvider` for `NextGenSoftware.Logging`.

- Supports all NLog targets, layouts, and rule-based configuration
- Zero-friction: just register and all NLog power is available via the shared logger
- Multi-target: net8.0, net9.0, net10.0

```csharp
logger.RegisterProvider(new NLogProvider());
// All subsequent logger.Log() calls now also route through NLog
```

---

### [NextGenSoftware.CLI.Engine](https://www.nuget.org/packages/NextGenSoftware.CLI.Engine)

A feature-rich CLI engine — the terminal backbone of the OASIS Omniverse ecosystem.

- Rich colour output, ASCII art banners, animated working-message spinners
- Formatted tables, progress bars, multi-column layouts
- `BeginWorkingMessage` / `EndWorkingMessage` for inline DONE feedback on long operations
- `SupressConsoleLogging` property to toggle output on/off at runtime
- Integrates with `NextGenSoftware.ErrorHandling` for structured error reporting
- Multi-target: net8.0, net9.0, net10.0

```csharp
CLIEngine.ShowWorkingMessage("Loading data...");
var data = await LoadDataAsync();
CLIEngine.ShowMessage($"Loaded {data.Count} records.", ConsoleColor.Green);

CLIEngine.BeginWorkingMessage("Saving to Holochain...");
await SaveAsync(data);
CLIEngine.EndWorkingMessage();   // prints "DONE" on the same line

CLIEngine.ShowDivider();
CLIEngine.ShowTable(headers, rows);
```

---

### [NextGenSoftware.ErrorHandling](https://www.nuget.org/packages/NextGenSoftware.ErrorHandling)

Structured error handling primitives and the `OASISResult` pattern used throughout the OASIS ecosystem.

- `OASISResult<T>` — a clean, null-safe async result type: no exceptions leaking across boundaries
- Structured exception wrappers with context metadata
- Consistent error propagation pattern across all OASIS and HoloNET APIs
- Multi-target: net8.0, net9.0, net10.0

```csharp
OASISResult<MyData> result = await myManager.LoadAsync(id);
if (result.IsError)
    Console.WriteLine($"Error: {result.Message}");
else
    Process(result.Result);
```

---

### [NextGenSoftware.Utilities](https://www.nuget.org/packages/NextGenSoftware.Utilities)

A broad shared utility library used across every NextGen Software project.

- String manipulation, date/time helpers, encryption utilities
- `DataHelper`: `ObjectToByteArray`, `ConvertBinaryDataToString`, `DecodeBinaryDataAsUTF8`, and more
- `ExpandoObject` extensions, async patterns, reflection helpers
- Multi-target: net8.0, net9.0, net10.0

```csharp
byte[] bytes = DataHelper.ObjectToByteArray(myObject);
string decoded = DataHelper.DecodeBinaryDataAsUTF8(bytes);
```

---

## Installation

Install any package via NuGet:

```bash
dotnet add package NextGenSoftware.WebSocket
dotnet add package NextGenSoftware.Logging
dotnet add package NextGenSoftware.Logging.NLog
dotnet add package NextGenSoftware.CLI.Engine
dotnet add package NextGenSoftware.ErrorHandling
dotnet add package NextGenSoftware.Utilities
```

Or search for `NextGenSoftware` in the Visual Studio / Rider NuGet UI.

---

## Part of the OASIS Omniverse

These libraries are the foundational layer of the entire OASIS Omniverse ecosystem:

| Project | Description |
|---|---|
| [HoloNET](https://holonet.oasisomniverse.one) | The .NET Holochain client — uses WebSocket, Logging, Utilities |
| [OASIS API](https://oasisomniverse.one) | 85+ NuGet packages for Web3/Web4/Web5 — built on all 6 libraries |
| [STAR ODK](https://oasisomniverse.one) | No/Low Code generator for hApps, Solana, Ethereum, IPFS, and more |
| [OurWorld](https://oasisomniverse.one) | AR geo-location game-of-games — the flagship OASIS application |

Visit **[oasisomniverse.one](https://oasisomniverse.one)** to explore the full ecosystem.

---

## Building from Source

```bash
git clone https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries.git
cd NextGenSoftware-Libraries
dotnet build NextGenSoftware Libraries.sln
```

> **Note:** HoloNET and OASIS projects reference these libraries via `ProjectReference`. Clone this repo as a sibling to `holochain-client-csharp` and `OASIS` for local development to work out of the box.

---

## Licence

MIT — Copyright © NextGen Software Ltd 2019 - 2026

---

## Links

- [OASIS Omniverse](https://oasisomniverse.one)
- [HoloNET](https://holonet.oasisomniverse.one)
- [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)
- [NuGet](https://www.nuget.org/profiles/NextGenSoftware)
- [NextGen Software Ltd](https://oasisomniverse.one)
