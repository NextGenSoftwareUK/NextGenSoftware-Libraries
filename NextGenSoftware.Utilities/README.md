# NextGenSoftware.Utilities

**A broad shared utility library** — string helpers, data conversion, encryption, async patterns, reflection extensions, and more. Used across every [OASIS Omniverse](https://oasisomniverse.one) project.

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.Utilities.svg)](https://www.nuget.org/packages/NextGenSoftware.Utilities)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot**. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Installation

```bash
dotnet add package NextGenSoftware.Utilities
```

---

## String Extensions

```csharp
"myVariableName".ToSnakeCase()   // "my_variable_name"
"my_variable".ToCamelCase()      // "myVariable"
"my_variable".ToPascalCase()     // "MyVariable"
```

## DataHelper

```csharp
byte[] bytes = DataHelper.ObjectToByteArray(myObject);
string csv   = DataHelper.ConvertBinaryDataToString(bytes);  // comma-delimited, useful for logging
string text  = DataHelper.DecodeBinaryDataAsUTF8(bytes);
```

## ExpandoObject Helpers

```csharp
dynamic obj = new ExpandoObject();
ExpandoObjectHelpers.AddProperty(obj, "MyProp", "hello");
Console.WriteLine(obj.MyProp); // "hello"
```

---

## What''s Included

| Category | Utilities |
|---|---|
| **String** | `ToSnakeCase`, `ToCamelCase`, `ToPascalCase` |
| **Data / Binary** | `ObjectToByteArray`, `ConvertBinaryDataToString`, `DecodeBinaryDataAsUTF8` |
| **ExpandoObject** | `AddProperty` and dynamic object helpers |
| **Encryption** | Encryption/decryption helpers |
| **Async patterns** | Utilities for async workflows |
| **Reflection** | Type inspection and manipulation extensions |
| **Date/Time** | Date formatting and conversion helpers |

---

- [oasisomniverse.one](https://oasisomniverse.one) | [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

MIT — Copyright (c) NextGen Software Ltd 2019 - 2026