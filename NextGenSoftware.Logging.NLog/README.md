# NextGenSoftware.Logging.NLog

**Drop-in NLog integration for [`NextGenSoftware.Logging`](https://www.nuget.org/packages/NextGenSoftware.Logging)** — route all OASIS/HoloNET log calls through NLog's full target, rule, and layout engine with a single registration call.

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.Logging.NLog.svg)](https://www.nuget.org/packages/NextGenSoftware.Logging.NLog)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot**. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Features

- Implements `ILogProvider` — one line to add NLog to any OASIS or HoloNET project
- All NLog targets: file, database, Seq, Splunk, console, and 100+ community targets
- Full NLog rules, layouts, filters, and structured logging
- Runs alongside DefaultLogger and FileLogger simultaneously
- Multi-target: **.NET 8, .NET 9, .NET 10**

---

## Installation

```bash
dotnet add package NextGenSoftware.Logging.NLog
```

---

## Quick Start

```csharp
using NextGenSoftware.Logging;
using NextGenSoftware.Logging.NLog;

var logger = new Logger();
logger.RegisterProvider(new DefaultLogger());  // keep console output
logger.RegisterProvider(new NLogProvider());   // add full NLog routing

logger.Log("Starting up...", LogType.Info);
logger.Log("Something failed!", LogType.Error);
```

Configure NLog via `NLog.config` or programmatically as normal.

---

## NLog.config Example

```xml
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd">
  <targets>
    <target name="file" xsi:type="File" fileName="logs/${shortdate}.log"
            layout="${longdate} ${level} ${message}" />
  </targets>
  <rules>
    <logger name="*" minlevel="Info" writeTo="file" />
  </rules>
</nlog>
```

---

- [oasisomniverse.one](https://oasisomniverse.one) | [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

MIT — Copyright (c) NextGen Software Ltd 2019 - 2026