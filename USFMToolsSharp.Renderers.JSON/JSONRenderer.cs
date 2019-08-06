using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Renderers.JSON
{
    public class JSONRenderer
    {
        public List<string> UnrenderableMarkers;
        public JArray jsonUSFM;
        public JSONConfig jsonConfig;
        public JSONRenderer()
        {
            UnrenderableMarkers = new List<string>();
            jsonUSFM = new JArray();
            jsonConfig = new JSONConfig();
        }
        public JSONRenderer(JSONConfig config)
        {
            UnrenderableMarkers = new List<string>();
            jsonUSFM = new JArray();
            jsonConfig = config;
        }
        public string Render(USFMDocument input)
        {
            foreach(Marker marker in input.Contents)
            {
                jsonUSFM.Add(RenderMarker(marker));
            }
            if (jsonConfig.isMinified)
            {
                return jsonUSFM.ToString(Formatting.None);
            }
            return jsonUSFM.ToString();
        }
        public JObject RenderMarker(Marker input)
        {
            JObject output = new JObject();
            switch (input)
            {
                case PMarker pMarker:
                    output.Add("Type", "PMarker");
                    output.Add("Identifier",pMarker.Identifier);
                    output.Add("Contents", RenderContents(pMarker));
                    break;
                case CMarker cMarker:
                    output.Add("Type", "CMarker");
                    output.Add("Identifier", cMarker.Identifier);
                    output.Add("Number", cMarker.Number.ToString());
                    output.Add("Contents", RenderContents(cMarker));
                    break;
                case VMarker vMarker:
                    output.Add("Type", "VMarker");
                    output.Add("Identifier", vMarker.Identifier);
                    output.Add("Number", vMarker.VerseNumber.ToString());
                    output.Add("Contents", RenderContents(vMarker));
                    break;
                case QMarker qMarker:
                    output.Add("Type", "QMarker");
                    output.Add("Identifier", qMarker.Identifier);
                    output.Add("Indentation", qMarker.Depth.ToString());
                    output.Add("Contents", RenderContents(qMarker));
                    break;
                case MMarker mMarker:
                    output.Add("Type", "MMarker");
                    output.Add("Identifier", mMarker.Identifier);
                    break;
                case TextBlock textBlock:
                    output.Add("Type", "TextBlock");
                    output.Add("Text", textBlock.Text);
                    break;
                case BDMarker bdMarker:
                    output.Add("Type", "BDMarker");
                    output.Add("Identifier", bdMarker.Identifier);
                    output.Add("Contents", RenderContents(bdMarker));
                    break;
                case HMarker hMarker:
                    output.Add("Type", "HMarker");
                    output.Add("Identifier", hMarker.Identifier);
                    output.Add("Header", hMarker.HeaderText);
                    break;
                case MTMarker mTMarker:
                    output.Add("Type", "MTMarker");
                    output.Add("Identifier", mTMarker.Identifier);
                    output.Add("Emphasis", mTMarker.Weight.ToString());
                    output.Add("Title", mTMarker.Title);
                    break;
                case FMarker fMarker:
                    output.Add("Type", "FMarker");
                    output.Add("Identifier", fMarker.Identifier);
                    output.Add("Caller", fMarker.FootNoteCaller);
                    output.Add("Contents", RenderContents(fMarker));
                    break;
                case FTMarker fTMarker:
                    output.Add("Type", "FTMarker");
                    output.Add("Identifier", fTMarker.Identifier);
                    output.Add("Contents", RenderContents(fTMarker));
                    break;
                case FQAMarker fQAMarker:
                    output.Add("Type", "FQAMarker");
                    output.Add("Identifier", fQAMarker.Identifier);
                    output.Add("Contents", RenderContents(fQAMarker));
                    break;
                case IDEMarker ideMarker:
                    output.Add("Type", "IDEMarker");
                    output.Add("Identifier", ideMarker.Identifier);
                    output.Add("Encoding", ideMarker.Encoding);
                    break;
                case IDMarker iDMarker:
                    output.Add("Type", "IDMarker");
                    output.Add("Identifier", iDMarker.Identifier);
                    output.Add("Identification", iDMarker.TextIdentifier);
                    break;
                case VPMarker vPMarker:
                    output.Add("Type", "VPMarker");
                    output.Add("Identifier", vPMarker.Identifier);
                    output.Add("Character", vPMarker.VerseCharacter);
                    break;
                case VPEndMarker vPEndMarker:
                    output.Add("Type", "VPEndMarker");
                    output.Add("Identifier", vPEndMarker.Identifier);
                    break;
                case FQAEndMarker fQAEndMarker:
                    output.Add("Type", "FQAEndMarker");
                    output.Add("Identifier", fQAEndMarker.Identifier);
                    break;
                case FEndMarker fEndMarker:
                    output.Add("Type", "FEndMarker");
                    output.Add("Identifier", fEndMarker.Identifier);
                    break;
                default:
                    output.Add("Type", "Unknown");
                    output.Add("Identifier", input.Identifier);
                    UnrenderableMarkers.Add(input.Identifier);
                    break;
            }

            return output;
        }
        public JArray RenderContents(Marker input)
        {
            JArray contents = new JArray();
            foreach (Marker marker in input.Contents)
            {
                contents.Add(RenderMarker(marker));
            }
            return contents;
        } 
        public void clearJSONElements()
        {
            jsonUSFM.Clear();
        }

    }
}
