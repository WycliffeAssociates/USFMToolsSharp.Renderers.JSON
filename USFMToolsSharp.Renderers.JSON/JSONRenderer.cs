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
        public JSONRenderer()
        {
            UnrenderableMarkers = new List<string>();
            jsonUSFM = new JArray();
        }
        public string Render(USFMDocument input)
        {
            foreach(Marker marker in input.Contents)
            {
                jsonUSFM.Add(RenderMarker(marker));
            }

            return jsonUSFM.ToString();
        }
        public JObject RenderMarker(Marker input)
        {
            JObject output = new JObject();
            JArray contents = new JArray();
            switch (input)
            {
                case PMarker pMarker:
                    output.Add("Identifier",pMarker.Identifier);

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case CMarker cMarker:
                    output.Add("Identifier", cMarker.Identifier);
                    output.Add("Number", cMarker.Number.ToString());

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case VMarker vMarker:
                    output.Add("Identifier", vMarker.Identifier);
                    output.Add("Number", vMarker.VerseNumber.ToString());

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case QMarker qMarker:
                    output.Add("Identifier", qMarker.Identifier);
                    output.Add("Indentation", qMarker.Depth.ToString());

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case MMarker mMarker:
                    output.Add("Identifier", mMarker.Identifier);
                    break;
                case TextBlock textBlock:
                    output.Add("Text", textBlock.Text);
                    break;
                case BDMarker bdMarker:
                    output.Add("Identifier", bdMarker.Identifier);

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case HMarker hMarker:
                    output.Add("Identifier", hMarker.Identifier);
                    output.Add("Header", hMarker.HeaderText);
                    break;
                case MTMarker mTMarker:
                    output.Add("Identifier", mTMarker.Identifier);
                    output.Add("Emphasis", mTMarker.Weight.ToString());
                    output.Add("Title", mTMarker.Title);
                    break;
                case FMarker fMarker:
                    output.Add("Identifier", fMarker.Identifier);
                    output.Add("Caller", fMarker.FootNoteCaller);

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case FTMarker fTMarker:
                    output.Add("Identifier", fTMarker.Identifier);

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case FQAMarker fQAMarker:
                    output.Add("Identifier", fQAMarker.Identifier);

                    foreach (Marker marker in input.Contents)
                    {
                        contents.Add(RenderMarker(marker));
                    }
                    output.Add("Contents", contents);
                    break;
                case IDEMarker ideMarker:
                    output.Add("Identifier", ideMarker.Identifier);
                    output.Add("Encoding", ideMarker.Encoding);
                    break;
                case IDMarker iDMarker:
                    output.Add("Identifier", iDMarker.Identifier);
                    output.Add("Identification", iDMarker.TextIdentifier);
                    break;
                case VPMarker vPMarker:
                    output.Add("Identifier", vPMarker.Identifier);
                    output.Add("Character", vPMarker.VerseCharacter);
                    break;
                case FQAEndMarker _:
                case FEndMarker _:
                case VPEndMarker _:
                    break;
                default:
                    UnrenderableMarkers.Add(input.Identifier);
                    break;
            }

            return output;
        }

    }
}
