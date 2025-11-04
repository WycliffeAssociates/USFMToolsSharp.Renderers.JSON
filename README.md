![GitHub](https://img.shields.io/github/license/WycliffeAssociates/USFMToolsSharp.Renderers.JSON?color=blue)
![Travis (.com) branch](https://img.shields.io/travis/com/WycliffeAssociates/USFMToolsSharp.Renderers.JSON/master)
![Nuget](https://img.shields.io/nuget/v/USFMToolsSharp.Renderers.JSON?color=blue)
![Nuget](https://img.shields.io/nuget/dt/USFMToolsSharp.Renderers.JSON?color=blue)

# USFMToolsSharp.Renderers.JSON

A .NET library for rendering USFM (Unified Standard Format Markers) documents into structured JSON format.

## Overview

USFMToolsSharp.Renderers.JSON is a high-performance JSON renderer built on top of [USFMToolsSharp](https://github.com/WycliffeAssociates/USFMToolsSharp). It converts parsed USFM documents into a hierarchical JSON structure that preserves the semantic meaning of the original USFM markup. This library is ideal for Bible translation projects, scripture processing pipelines, and any application that needs to work with USFM content in a JSON format.

### Key Features

- **Native .NET 8**: Built on modern .NET for optimal performance and compatibility
- **System.Text.Json**: Uses the fast, built-in JSON serialization library
- **Hierarchical Output**: Maintains the nested structure of USFM documents
- **Configurable**: Supports both minified and formatted JSON output
- **Extensible**: Easy to extend with support for additional USFM markers

## Installation

Install the package from NuGet:

```bash
dotnet add package USFMToolsSharp.Renderers.JSON
```

Or via the Package Manager Console:

```powershell
Install-Package USFMToolsSharp.Renderers.JSON
```

View on NuGet: https://www.nuget.org/packages/USFMToolsSharp.Renderers.JSON/

## Requirements

- **.NET 8.0** or higher
- **USFMToolsSharp** (automatically installed as a dependency)

## Building

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- (Optional) Visual Studio 2022 or JetBrains Rider for IDE support

### Build from Command Line

1. Clone the repository:
   ```bash
   git clone https://github.com/WycliffeAssociates/USFMToolsSharp.Renderers.JSON.git
   cd USFMToolsSharp.Renderers.JSON
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build --configuration Release
   ```

4. (Optional) Run tests:
   ```bash
   dotnet test
   ```

### Build with Visual Studio

1. Open `USFMToolsSharp.Renderers.JSON.sln` in Visual Studio 2022 or later
2. Build the solution (Ctrl+Shift+B)
3. Run tests from Test Explorer (Ctrl+E, T)

## Usage

### Basic Usage

```csharp
using USFMToolsSharp;
using USFMToolsSharp.Renderers.JSON;

// Parse a USFM file
var parser = new USFMParser();
var contents = File.ReadAllText("01-GEN.usfm");
USFMDocument usfmDoc = parser.ParseFromString(contents);

// Render to JSON
var renderer = new JSONRenderer();
string jsonOutput = renderer.Render(usfmDoc);

// Save to file
File.WriteAllText("output.json", jsonOutput);
```

### Configuration Options

The `JSONConfig` class allows you to customize the JSON output:

```csharp
// Create minified JSON (no whitespace)
var config = new JSONConfig(isMinified: true);
var renderer = new JSONRenderer(config);
string minifiedJson = renderer.Render(usfmDoc);

// Create formatted JSON (default, with indentation)
var formattedRenderer = new JSONRenderer();
string formattedJson = formattedRenderer.Render(usfmDoc);
```

### Configuration Properties

- **isMinified** (bool, default: false): When `true`, outputs compact JSON without whitespace. When `false`, outputs formatted JSON with indentation for readability.

## JSON Output Structure

The renderer produces a hierarchical JSON structure where each USFM marker becomes a JSON object with:

- **Type**: The marker type (e.g., "CMarker", "VMarker", "TextBlock")
- **Identifier**: The USFM marker identifier (e.g., "c", "v", "p")
- **Contents**: Array of child elements (for markers that contain other markers)
- **Additional properties**: Marker-specific data (e.g., chapter numbers, verse numbers, text content)

### Example Output

Input USFM:
```
\id GEN Unlocked Literal Bible
\mt1 Genesis
\c 1
\p
\v 1 In the beginning, God created the heavens and the earth.
```

Output JSON:
```json
{
  "USFMDocument": [
    {
      "Type": "IDMarker",
      "Identifier": "id",
      "Identification": "GEN Unlocked Literal Bible"
    },
    {
      "Type": "MTMarker",
      "Identifier": "mt",
      "Emphasis": "1",
      "Title": "Genesis"
    },
    {
      "Type": "CMarker",
      "Identifier": "c",
      "Number": "1",
      "Contents": [
        {
          "Type": "PMarker",
          "Identifier": "p",
          "Contents": [
            {
              "Type": "VMarker",
              "Identifier": "v",
              "Number": "1",
              "Contents": [
                {
                  "Type": "TextBlock",
                  "Text": "In the beginning, God created the heavens and the earth."
                }
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

## Supported USFM Markers

The renderer currently supports the following USFM markers:

- **Identification**: `\id`, `\ide`
- **Titles**: `\mt`, `\h`
- **Chapters & Verses**: `\c`, `\v`
- **Paragraphs**: `\p`, `\q`, `\m`
- **Character Formatting**: `\bd`
- **Footnotes**: `\f`, `\ft`, `\fqa`
- **Special Text**: `\vp`

Unsupported markers are rendered as "Unknown" type and tracked in the `UnrenderableMarkers` list for debugging purposes.

## Contributing

Contributions are welcome! Here are some ways you can help:

- **Testing**: Test with various USFM documents and report issues
- **Marker Support**: Add support for additional USFM markers in the renderer
- **Bug Fixes**: Submit pull requests for bug fixes
- **Documentation**: Improve documentation and examples

### Development Workflow

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-new-feature`)
3. Make your changes and add tests
4. Run tests to ensure they pass (`dotnet test`)
5. Commit your changes (`git commit -am 'Add new feature'`)
6. Push to the branch (`git push origin feature/my-new-feature`)
7. Create a Pull Request

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Related Projects

- [USFMToolsSharp](https://github.com/WycliffeAssociates/USFMToolsSharp) - The core USFM parsing library

## Support

For questions, issues, or feature requests, please [open an issue](https://github.com/WycliffeAssociates/USFMToolsSharp.Renderers.JSON/issues) on GitHub.