# UnicontaCli

## Installation

- Download and install .NET Runtime 10, e.g. [.NET Runtime 10.0.2 for Windows X64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-10.0.2-windows-x64-installer).
- Download and unzip the latest release from [Releases](https://github.com/larsiverpp/UnicontaCli/releases).

## Usage

Start a command prompt in the folder where you unzipped the release and run:

```
Liversen.UnicontaCli.exe
```

For example, to get the inventory stock status as of December 31, 2025, run:

```
Liversen.UnicontaCli.exe --loginId Alice --password My#Pass42@word --accessIdentity 40781a63-c968-458f-bd32-c5baf536a59f --companyId 142857 inventory-stock-status get 2025-12-31
```