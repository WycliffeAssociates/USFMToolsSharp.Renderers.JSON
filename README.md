# USFMToolsSharp.Renderers.JSON
A .net JSON rendering tool for USFM.

# Description
USFMToolsSharp.Renderers.JSON is a JSON renderer for USFM. 

# Installation

You can install this package from nuget https://www.nuget.org/packages/USFMToolsSharp.Renderers.JSON/

# Requirements

We targeted .net standard 2.0 so .net core 2.0, .net framework 4.6.1, and mono 5.4 and
higher are the bare minimum.

# Building

With Visual Studio just build the solution. With the .net core tooling use `dotnet build`

# Contributing

Yes please! A couple things would be very helpful

- Testing: Because I can't test every single possible USFM document in existance. If you find something that doesn't look right in the parsing or rendering please submit an issue.
- Adding support for other markers to the parser. There are still plenty of things in the USFM spec that aren't implemented.
- Adding support for other markers to the JSON renderer

# Usage

There a couple useful classes that you'll want to use

## JSONRenderer

This class transforms a USFMDocument into a JSON string

Example:
```csharp
var contents = File.ReadAllText("01-GEN.usfm");
USFMDocument usfm = parser.ParseFromString(contents);
JSONRenderer JSONRenderer = new JSONRenderer();
string JSONOutput = JSONRenderer.Render(usfm);
File.WriteAllText("output.json",JSONOutput);
```

# JSON Output Example
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