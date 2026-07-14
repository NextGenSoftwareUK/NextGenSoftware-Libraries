# NextGenSoftware.WebSocket

**A lightweight, high-performance WebSocket client for .NET** — the real-time communication backbone of [HoloNET](https://holonet.oasisomniverse.one) and the [OASIS Omniverse](https://oasisomniverse.one) ecosystem.

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.WebSocket.svg)](https://www.nuget.org/packages/NextGenSoftware.WebSocket)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot** — the biggest NextGen Software release in years. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Features

- Clean **event-driven API** — `OnConnected`, `OnDisconnected`, `OnDataReceived`, `OnError`, `OnDataSent`
- **Async/await first** with proper cancellation support
- **Automatic reconnection** with configurable retry logic
- Binary and text message support
- Production-hardened — used in HoloNET for all Holochain conductor WebSocket communication
- Multi-target: **.NET 8, .NET 9, .NET 10**

---

## Installation

```bash
dotnet add package NextGenSoftware.WebSocket
```

---

## Quick Start

```csharp
using NextGenSoftware.WebSocket;

var client = new WebSocketClient("wss://localhost:8888");
client.OnConnected    += (s, e) => Console.WriteLine("Connected!");
client.OnDataReceived += (s, e) => Console.WriteLine($"Received {e.RawData.Length} bytes");
client.OnError        += (s, e) => Console.WriteLine($"Error: {e.Reason}");

await client.Connect();
await client.SendRawDataAsync(myBytes);
await client.Disconnect();
```

---

## Events

| Event | Description |
|---|---|
| `OnConnected` | Connection established |
| `OnDisconnected` | Connection closed (cleanly or otherwise) |
| `OnDataReceived` | Raw data arrived from the server |
| `OnDataSent` | Data successfully sent |
| `OnError` | WebSocket error occurred |

---

## Part of the OASIS Omniverse

This package powers **HoloNET** — the world's first .NET and Unity client for Holochain (upgraded to Holochain 0.6.2) — and is used across all 85+ OASIS API NuGet packages, STAR ODK, and OurWorld.

- [oasisomniverse.one](https://oasisomniverse.one)
- [holonet.oasisomniverse.one](https://holonet.oasisomniverse.one)
- [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

---

MIT — Copyright (c) NextGen Software Ltd 2019 - 2026