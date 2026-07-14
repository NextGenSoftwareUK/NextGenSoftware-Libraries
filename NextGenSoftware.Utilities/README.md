# NextGenSoftware.Utilities

**A broad shared utility library** — string helpers, data conversion, encryption, key generation, validation, path resolution, directory tools, list helpers, and more. Used across every [OASIS Omniverse](https://oasisomniverse.one) project.

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.Utilities.svg)](https://www.nuget.org/packages/NextGenSoftware.Utilities)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot**. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Installation

```bash
dotnet add package NextGenSoftware.Utilities
```

---

## String Extension Methods

`using NextGenSoftware.Utilities.ExtentionMethods;`

```csharp
"myVariableName".ToSnakeCase()   // "my_variable_name"
"my_variable".ToCamelCase()      // "myVariable"
"my_variable_name".ToPascalCase() // "MyVariableName"
```

| Method | Description |
|---|---|
| `ToSnakeCase()` | Converts camelCase / PascalCase string to snake_case |
| `ToCamelCase()` | Converts snake_case / spaced string to camelCase |
| `ToPascalCase()` | Full PascalCase conversion — handles separators, runs of caps, numbers |

---

## StringHelper

```csharp
bool ok = StringHelper.IsValidVersion("1.2.3");   // true
bool bad = StringHelper.IsValidVersion("1.2.3.4.5"); // false (max 4 parts)
```

| Method | Description |
|---|---|
| `IsValidVersion(string)` | Validates a version string (up to 4 dot-separated integer parts) |

---

## DataHelper

```csharp
string csv  = DataHelper.ConvertBinaryDataToString(bytes);          // "72,101,108,108,111"
string text = DataHelper.DecodeBinaryDataAsUTF8(bytes);             // "Hello"
string csv2 = DataHelper.ConvertBinaryDataToString(bytes, false);   // include zero bytes
```

| Method | Description |
|---|---|
| `ConvertBinaryDataToString(byte[], bool)` | Comma-delimited decimal representation of bytes; optionally skip zero bytes |
| `DecodeBinaryDataAsUTF8(byte[])` | UTF-8 decode with null-byte stripping and trimming |

---

## FileEncryption (AES + RSA)

```csharp
// AES file encryption
byte[] key = ...; byte[] iv = ...;
FileEncryption.EncryptFile("input.dat", "encrypted.dat", key, iv);
FileEncryption.DecryptFile("encrypted.dat", "output.dat", key, iv);
string text = FileEncryption.DecryptFile("encrypted.dat", key, iv); // returns string

// RSA string encryption (Windows CryptoServiceProvider)
string cipher = myString.Encrypt("MyKeyContainerName");
string plain  = cipher.Decrypt("MyKeyContainerName");
```

| Method | Description |
|---|---|
| `EncryptFile(in, out, key, iv)` | AES-encrypt a file to disk |
| `DecryptFile(in, out, key, iv)` | AES-decrypt a file to disk |
| `DecryptFile(in, key, iv)` | AES-decrypt a file and return as string |
| `Encrypt(this string, key)` | RSA-encrypt a string using a named key container |
| `Decrypt(this string, key)` | RSA-decrypt a string using a named key container |

---

## KeyHelper — Blockchain Key Generation

```csharp
var keypair = KeyHelper.GenerateKeyValuePairAndWalletAddress();
Console.WriteLine(keypair.PublicKey);            // hex-encoded secp256k1 public key
Console.WriteLine(keypair.PrivateKey);           // hex-encoded private key
Console.WriteLine(keypair.WalletAddressLegacy);  // P2PKH Bitcoin address (hex scriptPubKey)
Console.WriteLine(keypair.WalletAddressSegwitP2SH); // P2SH-SegWit address (hex scriptPubKey)

bool validPub  = KeyHelper.IsValidRsaPublicKey(pemString);
bool validPriv = KeyHelper.IsValidRsaPrivateKey(pemOrDerString);
```

| Method | Description |
|---|---|
| `GenerateKeyValuePairAndWalletAddress()` | Generates a secp256k1 keypair + P2PKH and P2SH-SegWit Bitcoin wallet addresses |
| `IsValidRsaPublicKey(string)` | Validates a PEM RSA public key |
| `IsValidRsaPrivateKey(string)` | Validates a PEM or DER RSA private key (PKCS#8 or PKCS#1) |

### IKeyPairAndWallet

```csharp
public interface IKeyPairAndWallet {
    string PublicKey { get; set; }
    string PrivateKey { get; set; }
    string WalletAddressLegacy { get; set; }
    string WalletAddressSegwitP2SH { get; set; }
}
```

---

## WalletAddressHelper

```csharp
string address = WalletAddressHelper.PrivateKeyToAddress(wifPrivateKey);
string address2 = WalletAddressHelper.PublicKeyToAddress(pubKeyHex);
```

| Method | Description |
|---|---|
| `PrivateKeyToAddress(string)` | Derives a P2PKH address scriptPubKey hex from a WIF private key |
| `PublicKeyToAddress(string)` | Derives a P2PKH address scriptPubKey hex from a compressed public key hex |

---

## ValidationHelper

```csharp
bool valid = ValidationHelper.IsValidEmail("user@example.com"); // true
bool bad   = ValidationHelper.IsValidEmail("not-an-email");     // false
```

| Method | Description |
|---|---|
| `IsValidEmail(string)` | RFC-compliant email validation using MailAddress + regex normalisation |

---

## URIHelper

```csharp
bool exists = await URIHelper.ValidateUrlWithHttpClientAsync("https://oasisomniverse.one");
```

| Method | Description |
|---|---|
| `ValidateUrlWithHttpClientAsync(string)` | HTTP HEAD request to check a URL is reachable; returns false on DNS/connection failure |

---

## ListHelper

```csharp
List<string> list = ListHelper.ConvertToList("apple,banana,cherry");
string csv = ListHelper.ConvertFromList(list); // "apple,banana,cherry"
```

| Method | Description |
|---|---|
| `ConvertToList(string)` | Splits a comma-delimited string into a `List<string>` |
| `ConvertFromList<T>(List<T>)` | Joins a list into a comma-delimited string |

---

## EnumHelper

```csharp
string newline = EnumHelper.GetEnumValues(typeof(ConsoleColor));
// "Black\nDarkBlue\nDarkGreen\n..."

string comma = EnumHelper.GetEnumValues(typeof(ConsoleColor), EnumHelperListType.ItemsSeperatedByComma);
// "Black, DarkBlue, DarkGreen, ... or White"

IEnumerable<EnumValue<ConsoleColor>> wrapped = EnumHelper.ConvertToEnumValueList(colours);
IEnumerable<ConsoleColor> unwrapped = EnumHelper.ConvertFromEnumValueList(wrapped);
```

| Method | Description |
|---|---|
| `GetEnumValues(Type, EnumHelperListType)` | Returns all enum member names formatted as newline or comma-separated list |
| `ConvertToEnumValueList<T>(IEnumerable<T>)` | Wraps enum values in `EnumValue<T>` for data binding |
| `ConvertFromEnumValueList<T>(IEnumerable<EnumValue<T>>)` | Unwraps `EnumValue<T>` back to plain enum values |

---

## ExpandoObjectHelpers

```csharp
using NextGenSoftware.Utilities.ExtentionMethods;

dynamic obj = new ExpandoObject();
ExpandoObjectHelpers.AddProperty(obj, "Name", "OurWorld");
ExpandoObjectHelpers.AddProperty(obj, "Name", "Updated"); // updates existing key
Console.WriteLine(obj.Name); // "Updated"
```

| Method | Description |
|---|---|
| `AddProperty(ExpandoObject, string, object)` | Adds or updates a named property on an ExpandoObject |

---

## DirectoryHelper

```csharp
DirectoryHelper.CopyFilesRecursively("C:/source", "C:/dest");
```

| Method | Description |
|---|---|
| `CopyFilesRecursively(source, target)` | Recursively copies all files and subdirectories from source to target, creating missing directories |

---

## AppPathHelper

Reliable cross-platform app root and user data path resolution.

```csharp
string appRoot  = AppPathHelper.ResolveAppRootDirectory();
string fullPath = AppPathHelper.ResolvePathFromAppRoot("config/settings.json");
string basePath = AppPathHelper.ResolveBasePath(appRoot, configured);
bool writable   = AppPathHelper.IsDirectoryWritable(appRoot);
string userData = AppPathHelper.GetUserDataRoot("my-app");
string subDir   = AppPathHelper.GetUserDataSubDirectory("my-app", "logs");
```

| Method | Description |
|---|---|
| `ResolveAppRootDirectory()` | Returns the directory containing the running DLL; falls back through ProcessPath, Assembly.Location, CurrentDirectory |
| `ResolvePathFromAppRoot(string)` | Combines app root with a relative path; absolute paths pass through unchanged |
| `ResolveBasePath(appRoot, configured)` | Returns configured path if non-empty; falls back to appRoot |
| `IsDirectoryWritable(string)` | Write-tests a directory by creating and deleting a temp file |
| `GetUserDataRoot(string)` | Returns (and creates) `%LocalAppData%/<appFolderName>` (cross-platform) |
| `GetUserDataSubDirectory(string, string)` | Returns (and creates) a named subdirectory under `GetUserDataRoot` |

---

## PathHelper

```csharp
string normalized = PathHelper.NormalizePathFromConfig("some/forward/slash/path");
// On Windows: "some\forward\slash\path"
// On Linux/macOS: "some/forward/slash/path"
```

| Method | Description |
|---|---|
| `NormalizePathFromConfig(string)` | Converts forward/back slashes to the OS directory separator; safe for JSON config paths |

---

- [oasisomniverse.one](https://oasisomniverse.one) | [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

MIT — Copyright © NextGen Software Ltd 2019 - 2026
