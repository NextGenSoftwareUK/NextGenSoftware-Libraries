# NextGenSoftware.Logging

**A flexible, pluggable logging library** with a clean `ILogger`/`ILogProvider` abstraction — used across the entire [OASIS Omniverse](https://oasisomniverse.one) ecosystem (HoloNET, OASIS API, STAR ODK, OurWorld, and more).

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.Logging.svg)](https://www.nuget.org/packages/NextGenSoftware.Logging)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot**. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Features

- **Pluggable provider model** — register multiple simultaneous providers; all receive every log call
- Built-in **DefaultLogger** with configurable colour output
- **File logger** out of the box
- **NLog integration** via the companion [`NextGenSoftware.Logging.NLog`](https://www.nuget.org/packages/NextGenSoftware.Logging.NLog) package
- Write your own provider by implementing `ILogProvider`
- Configurable **log levels** and async log dispatch
- Multi-target: **.NET 8, .NET 9, .NET 10**

---

## Installation

```bash
dotnet add package NextGenSoftware.Logging
```

---

## Quick Start

```csharp
using NextGenSoftware.Logging;

var logger = new Logger();
logger.RegisterProvider(new DefaultLogger());        // coloured console
logger.RegisterProvider(new FileLogger("app.log"));  // file output

logger.Log("Application started.", LogType.Info);
logger.Log("Something went wrong!", LogType.Error);
```

---

## Custom Provider

```csharp
public class MyProvider : ILogProvider
{
    public void Log(string message, LogType logType)
    {
        MyTelemetrySystem.Send(message, logType.ToString());
    }
}
logger.RegisterProvider(new MyProvider());
```

---

## Log Levels

| Level | Use |
|---|---|
| `LogType.Debug` | Verbose trace for development |
| `LogType.Info` | Normal operational messages |
| `LogType.Warning` | Recoverable unexpected conditions |
| `LogType.Error` | Failures requiring attention |

---

- [oasisomniverse.one](https://oasisomniverse.one) | [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

MIT — Copyright (c) NextGen Software Ltd 2019 - 2026