# NextGenSoftware.ErrorHandling

**Structured error handling and the `OASISResult` pattern** — the error propagation backbone of the [OASIS Omniverse](https://oasisomniverse.one) ecosystem (HoloNET, the OASIS API, STAR ODK, OurWorld).

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.ErrorHandling.svg)](https://www.nuget.org/packages/NextGenSoftware.ErrorHandling)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot**. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Features

- **`OASISResult<T>`** — null-safe async result type; return success or failure without throwing across API boundaries
- **`OASISResult`** (non-generic) for void operations
- Structured exception wrappers with rich context metadata
- Consistent error propagation across all 85+ OASIS API packages and HoloNET
- Integrates with `NextGenSoftware.Logging`
- Multi-target: **.NET 8, .NET 9, .NET 10**

---

## Installation

```bash
dotnet add package NextGenSoftware.ErrorHandling
```

---

## Quick Start

```csharp
using NextGenSoftware.ErrorHandling;

public async Task<OASISResult<MyData>> LoadDataAsync(string id)
{
    var result = new OASISResult<MyData>();
    try
    {
        result.Result = await _db.GetAsync(id);
    }
    catch (Exception ex)
    {
        result.Exception = ex;
        result.Message   = $"Failed to load {id}.";
        result.IsError   = true;
    }
    return result;
}

// Call site
var result = await myService.LoadDataAsync("abc123");
if (result.IsError)
    Console.WriteLine($"Error: {result.Message}");
else
    Process(result.Result);
```

---

## OASISResult Properties

| Property | Type | Description |
|---|---|---|
| `Result` | `T` | The returned value on success |
| `IsError` | `bool` | `true` if the operation failed |
| `IsWarning` | `bool` | `true` if a non-fatal warning occurred |
| `Message` | `string` | Human-readable error/success message |
| `Exception` | `Exception` | Underlying exception if any |
| `InnerMessages` | `List<string>` | Accumulated sub-messages from nested calls |

---

- [oasisomniverse.one](https://oasisomniverse.one) | [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

MIT — Copyright (c) NextGen Software Ltd 2019 - 2026