using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Renderers.JSON
{
    public class JSONRenderer
    {
        public List<string> UnrenderableMarkers;
        public JsonObject jsonUSFM;
        public JSONConfig jsonConfig;
        public JSONRenderer()
        {
            UnrenderableMarkers = new List<string>();
            jsonUSFM = new JsonObject();
            jsonConfig = new JSONConfig();
        }
        public JSONRenderer(JSONConfig config)
        {
            UnrenderableMarkers = new List<string>();
            jsonUSFM = new JsonObject();
            jsonConfig = config;
        }
        public string Render(USFMDocument input)
        {
            JsonArray usfmDocJSON = new JsonArray();
            foreach(Marker marker in input.Contents)
            {
                usfmDocJSON.Add(RenderMarker(marker));
            }
            jsonUSFM["USFMDocument"] = usfmDocJSON;

            if (jsonConfig.isMinified)
            {
                return jsonUSFM.ToJsonString(new JsonSerializerOptions { WriteIndented = false, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
            }
            return jsonUSFM.ToJsonString(new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
        }
        public JsonObject RenderMarker(Marker input)
        {
            JsonObject output = new JsonObject();
            switch (input)
            {
                case PMarker pMarker:
                    output["Type"] = "PMarker";
                    output["Identifier"] = pMarker.Identifier;
                    output["Contents"] = RenderContents(pMarker);
                    break;
                case CMarker cMarker:
                    output["Type"] = "CMarker";
                    output["Identifier"] = cMarker.Identifier;
                    output["Number"] = cMarker.Number.ToString();
                    output["Contents"] = RenderContents(cMarker);
                    break;
                case VMarker vMarker:
                    output["Type"] = "VMarker";
                    output["Identifier"] = vMarker.Identifier;
                    output["Number"] = vMarker.VerseNumber.ToString();
                    output["Contents"] = RenderContents(vMarker);
                    break;
                case QMarker qMarker:
                    output["Type"] = "QMarker";
                    output["Identifier"] = qMarker.Identifier;
                    output["Indentation"] = qMarker.Depth.ToString();
                    output["Contents"] = RenderContents(qMarker);
                    break;
                case MMarker mMarker:
                    output["Type"] = "MMarker";
                    output["Identifier"] = mMarker.Identifier;
                    break;
                case TextBlock textBlock:
                    output["Type"] = "TextBlock";
                    output["Text"] = textBlock.Text;
                    break;
                case BDMarker bdMarker:
                    output["Type"] = "BDMarker";
                    output["Identifier"] = bdMarker.Identifier;
                    output["Contents"] = RenderContents(bdMarker);
                    break;
                case HMarker hMarker:
                    output["Type"] = "HMarker";
                    output["Identifier"] = hMarker.Identifier;
                    output["Header"] = hMarker.HeaderText;
                    break;
                case MTMarker mTMarker:
                    output["Type"] = "MTMarker";
                    output["Identifier"] = mTMarker.Identifier;
                    output["Emphasis"] = mTMarker.Weight.ToString();
                    output["Title"] = mTMarker.Title;
                    break;
                case FMarker fMarker:
                    output["Type"] = "FMarker";
                    output["Identifier"] = fMarker.Identifier;
                    output["Caller"] = fMarker.FootNoteCaller;
                    output["Contents"] = RenderContents(fMarker);
                    break;
                case FTMarker fTMarker:
                    output["Type"] = "FTMarker";
                    output["Identifier"] = fTMarker.Identifier;
                    output["Contents"] = RenderContents(fTMarker);
                    break;
                case FQAMarker fQAMarker:
                    output["Type"] = "FQAMarker";
                    output["Identifier"] = fQAMarker.Identifier;
                    output["Contents"] = RenderContents(fQAMarker);
                    break;
                case IDEMarker ideMarker:
                    output["Type"] = "IDEMarker";
                    output["Identifier"] = ideMarker.Identifier;
                    output["Encoding"] = ideMarker.Encoding;
                    break;
                case IDMarker iDMarker:
                    output["Type"] = "IDMarker";
                    output["Identifier"] = iDMarker.Identifier;
                    output["Identification"] = iDMarker.TextIdentifier;
                    break;
                case VPMarker vPMarker:
                    output["Type"] = "VPMarker";
                    output["Identifier"] = vPMarker.Identifier;
                    output["Character"] = vPMarker.VerseCharacter;
                    break;
                case VPEndMarker vPEndMarker:
                    output["Type"] = "VPEndMarker";
                    output["Identifier"] = vPEndMarker.Identifier;
                    break;
                case FQAEndMarker fQAEndMarker:
                    output["Type"] = "FQAEndMarker";
                    output["Identifier"] = fQAEndMarker.Identifier;
                    break;
                case FEndMarker fEndMarker:
                    output["Type"] = "FEndMarker";
                    output["Identifier"] = fEndMarker.Identifier;
                    break;
                default:
                    output["Type"] = "Unknown";
                    output["Identifier"] = input.Identifier;
                    UnrenderableMarkers.Add(input.Identifier);
                    break;
            }

            return output;
        }
        public JsonArray RenderContents(Marker input)
        {
            JsonArray contents = new JsonArray();
            foreach (Marker marker in input.Contents)
            {
                contents.Add(RenderMarker(marker));
            }
            return contents;
        } 

    }
}
