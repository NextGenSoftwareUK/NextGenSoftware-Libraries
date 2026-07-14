# NextGenSoftware.CLI.Engine

**A feature-rich CLI engine for .NET** — colour output, animations, spinners, tables, progress bars, ASCII art, interactive prompts, non-interactive / CI mode, and more. The terminal backbone of the [OASIS Omniverse](https://oasisomniverse.one) ecosystem, used in HoloNET, the OASIS API CLI, STAR ODK, and OurWorld.

[![NuGet](https://img.shields.io/nuget/v/NextGenSoftware.CLI.Engine.svg)](https://www.nuget.org/packages/NextGenSoftware.CLI.Engine)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10-purple.svg)](https://dotnet.microsoft.com)

Part of the **OASIS Omniverse v2.0 Reboot**. Visit [oasisomniverse.one](https://oasisomniverse.one).

---

## Installation

```bash
dotnet add package NextGenSoftware.CLI.Engine
```

---

## Global Properties

```csharp
CLIEngine.SuccessMessageColour   = ConsoleColor.Green;     // default
CLIEngine.ErrorMessageColour     = ConsoleColor.Red;       // default
CLIEngine.WarningMessageColour   = ConsoleColor.DarkYellow;// default
CLIEngine.WorkingMessageColour   = ConsoleColor.Yellow;    // default
CLIEngine.SupressConsoleLogging  = false;                  // set true to silence all output
CLIEngine.NonInteractive         = false;                  // set true for CI / shell mode
CLIEngine.AssumeYes              = false;                  // auto-yes for GetConfirmation in NonInteractive
CLIEngine.JsonOutput             = false;                  // flag for JSON-line host apps
CLIEngine.Quiet                  = false;                  // suppress banners/donation messages
CLIEngine.MaxHolonSearchResults  = 0;                      // 0 = unlimited
CLIEngine.ErrorHandlingBehaviour = ErrorHandlingBehaviour.OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent;
```

---

## Events

```csharp
CLIEngine.OnError += (sender, e) => {
    Console.WriteLine($"CLIEngine error: {e.Reason}");
    Console.WriteLine(e.ErrorDetails);
};
```

---

## Output Methods

### ShowMessage

```csharp
CLIEngine.ShowMessage("Hello world");                            // yellow, with leading blank line
CLIEngine.ShowMessage("Done.", ConsoleColor.Cyan);              // custom colour
CLIEngine.ShowMessage("Inline", lineSpace: false, noLineBreaks: true); // no newline appended
CLIEngine.ShowMessage("Indented", intendBy: 4);                // extra indent spaces
CLIEngine.ShowMessage("After gap", addLineBefore: true);       // blank line before message
```

### ShowSuccessMessage / ShowErrorMessage / ShowWarningMessage

```csharp
CLIEngine.ShowSuccessMessage("All packages published!");
CLIEngine.ShowErrorMessage("Connection failed.", addLineBefore: true);
CLIEngine.ShowWarningMessage("Proceeding with defaults.");
```

### ShowDivider

```csharp
CLIEngine.ShowDivider();                         // 119 "*" characters
CLIEngine.ShowDivider("-", 80);                  // 80 dashes
CLIEngine.ShowDivider("=", 60, lineSpace: false);
```

### DisplayProperty

```csharp
CLIEngine.DisplayProperty("Status", "Connected", displayFieldLength: 20);
// Output:  Status:             Connected
CLIEngine.DisplayProperty("Version", "4.0.0", 20, displayColon: false);
```

---

## Working Message Spinner

Displays a message then starts an animated spinner on the same line. `EndWorkingMessage` stops the spinner and prints a result on the same line.

```csharp
CLIEngine.BeginWorkingMessage("Connecting to Holochain conductor...");
await client.ConnectAsync();
CLIEngine.EndWorkingMessage("DONE");           // prints " DONE" on the same line

// Or with a custom colour:
CLIEngine.BeginWorkingMessage("Loading data...", ConsoleColor.Cyan);
var data = await LoadDataAsync();
CLIEngine.EndWorkingMessage("Loaded.", ConsoleColor.Green);
```

### ShowWorkingMessage (lower level)

```csharp
CLIEngine.ShowWorkingMessage("Processing...");
// ... do work ...
// spinner continues until next ShowMessage call or Spinner.Stop()
```

### UpdateWorkingMessage / UpdateWorkingMessageWithPercent

```csharp
CLIEngine.BeginWorkingMessage("Uploading...");
for (int i = 0; i <= 100; i += 10)
{
    await Task.Delay(200);
    CLIEngine.UpdateWorkingMessageWithPercent(i);
}
CLIEngine.EndWorkingMessage("Complete.");

CLIEngine.UpdateWorkingMessage("Still going...", ConsoleColor.Cyan);
```

| Method | Description |
|---|---|
| `BeginWorkingMessage(string)` | Print message + start spinner; cursor stays on same line |
| `BeginWorkingMessage(string, ConsoleColor)` | Same with custom colour |
| `ShowWorkingMessage(string)` | Print yellow working message + start spinner |
| `ShowWorkingMessage(string, ConsoleColor)` | Same with custom colour |
| `UpdateWorkingMessage(string)` | Replace working message in-place without stopping spinner |
| `UpdateWorkingMessageWithPercent(int)` | Write `NN%` inline next to the spinner |
| `EndWorkingMessage(string)` | Stop spinner, print result on same line |
| `EndWorkingMessage(string, ConsoleColor)` | Same with custom colour |

---

## Progress Bar

```csharp
for (double i = 0; i <= 1.0; i += 0.1)
{
    CLIEngine.ShowProgressBar(i);       // 0.0 – 1.0
    await Task.Delay(200);
}
CLIEngine.DisposeProgressBar();         // clears bar from console
CLIEngine.DisposeProgressBar(clearText: false); // leaves final bar text visible
```

| Method | Description |
|---|---|
| `ShowProgressBar(double)` | Update ASCII progress bar (0.0–1.0); creates it on first call |
| `DisposeProgressBar(bool)` | Dispose the bar; `clearText: false` leaves final state visible |

---

## ASCII Art Banner

```csharp
CLIEngine.WriteAsciMessage("OASIS", System.Drawing.Color.Cyan);
```

| Method | Description |
|---|---|
| `WriteAsciMessage(string, Color)` | Renders large ASCII-art text using Colorful.Console |

---

## Colour Helpers

```csharp
CLIEngine.ShowColoursAvailable();           // lists all valid ConsoleColor names in their own colour
CLIEngine.PrintColour("DarkGreen");         // writes "DarkGreen" in dark green
```

---

## Interactive Input Methods

All input methods throw `CLIEngineNonInteractiveInputRequiredException` when `CLIEngine.NonInteractive = true`.

### Text Input

```csharp
string name  = CLIEngine.GetValidInput("Enter your name:");
string title = CLIEngine.GetValidTitle("Enter title (Mr/Mrs/Ms/Miss/Dr):");
string email = CLIEngine.GetValidInputForEmail("Enter email:");
```

### Numeric Input

```csharp
int    count  = CLIEngine.GetValidInputForInt("How many?");
int    ranged = CLIEngine.GetValidInputForInt("Pick 1–10:", checkForLowestOrHighestRange: true, lowest: 1, highest: 10);
long   big    = CLIEngine.GetValidInputForLong("Enter a large number:");
double rate   = CLIEngine.GetValidInputForDouble("Enter rate:");
decimal price = CLIEngine.GetValidInputForDecimal("Enter price:");
Guid   id     = CLIEngine.GetValidInputForGuid("Enter GUID:");
```

### Date Input

```csharp
DateTime date = CLIEngine.GetValidInputForDate("Enter date (yyyy-mm-dd):");
// Type "exit" or "none" to cancel and return DateTime.MinValue
```

### Enum Input

```csharp
object result = CLIEngine.GetValidInputForEnum("Choose log level:", typeof(LogType));
LogType level = (LogType)result;
// Shows all valid enum values automatically; re-prompts on invalid input
```

### Boolean / Confirmation

```csharp
bool yes = CLIEngine.GetConfirmation("Are you sure? (Y/N)");
// Returns AssumeYes when NonInteractive = true
```

### Password Input

```csharp
string password = CLIEngine.GetValidPassword();
// Prompts twice, masks input with *, requires matching entries

string pw = CLIEngine.ReadPassword("Enter master password:");
// Single masked read
```

### File and Folder Input

```csharp
string folder = CLIEngine.GetValidFolder("Enter folder path:", createIfDoesNotExist: true);
string file   = CLIEngine.GetValidFile("Enter file path:");
byte[] bytes  = CLIEngine.GetValidFileAndUpload("Select file to upload:");
```

### URI Input

```csharp
Uri uri = await CLIEngine.GetValidURIAsync("Enter URL:", checkFileExists: true);
// Validates format and optionally does an HTTP HEAD to confirm resource exists
```

### Colour Picker

```csharp
ConsoleColor fav = ConsoleColor.Blue;
ConsoleColor cli = ConsoleColor.Green;
CLIEngine.GetValidColour(ref fav, ref cli);
// Interactive wizard: pick favourite colour + CLI display colour, with confirmation loop
```

---

## Spinner (standalone)

```csharp
var spinner = new Spinner();
spinner.Colour   = ConsoleColor.Cyan;
spinner.Sequence = @"/-\|";    // animation frames
spinner.Delay    = 100;        // ms per frame

spinner.Start();               // auto-positions at current cursor
spinner.Start(left: 20, top: 5, delay: 80); // explicit position
spinner.Stop();
spinner.Dispose();

// CLIEngine.Spinner is a shared singleton:
CLIEngine.Spinner.Colour = ConsoleColor.Magenta;
```

| Property | Description |
|---|---|
| `Sequence` | Animation characters (default `/-\|`) |
| `Delay` | Frame delay in milliseconds (default 100) |
| `Colour` | Spinner character colour (default Green) |
| `IsActive` | Whether the spinner thread is running |
| `Left / Top` | Console cursor position for spinner character |

---

## Animations

```csharp
var anim = new Animations();

// Rotating spinner character at current position:
anim.Turn("Loading");

// Matrix-style grid animation at a fixed console position:
anim.SequencedMatrix(row: 5, column: 10, width: 8, height: 4);

// ASCII loading bar at a fixed position:
anim.LoadingBar("Loading", row: 3, column: 0);

anim.Ready(); // advance frame counter + sleep 75ms

// Simple inline spinner:
var spin = new ConsoleSpiner();
spin.Turn(); // writes one frame character and backtracks cursor
```

---

## ConsoleHelper (Windows — Font Control)

```csharp
var fontInfo = ConsoleHelper.SetCurrentFont("Lucida Console", fontSize: 16);
// Returns FontInfo[] { before, set, after }
```

| Method | Description |
|---|---|
| `SetCurrentFont(string, short)` | Sets console font via Win32 SetCurrentConsoleFontEx; returns before/set/after snapshots |

---

## Exceptions

| Type | Description |
|---|---|
| `CLIEngineException` | Thrown by internal error handler when no `OnError` subscriber exists (or `AlwaysThrowExceptionOnError` is set) |
| `CLIEngineNonInteractiveInputRequiredException` | Thrown by any input method when `CLIEngine.NonInteractive = true` |

---

## ErrorHandlingBehaviour

| Value | Description |
|---|---|
| `AlwaysThrowExceptionOnError` | Always throw `CLIEngineException` on internal error |
| `OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent` | Throw only if `OnError` has no subscribers (default) |
| `NeverThrowExceptions` | Fire `OnError` event only, never throw |

---

- [oasisomniverse.one](https://oasisomniverse.one) | [GitHub](https://github.com/NextGenSoftwareUK/NextGenSoftware-Libraries)

MIT — Copyright © NextGen Software Ltd 2019 - 2026
