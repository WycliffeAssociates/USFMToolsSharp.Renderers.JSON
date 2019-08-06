using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Renderers.JSON.Tests
{
    [TestClass]
    public class JSONRendererTests
    {
        private USFMParser parser;
        private JSONRenderer render;

        [TestInitialize]
        public void SetUpTestCase()
        {
            parser = new USFMParser();
            render = new JSONRenderer(new JSONConfig(isMinified:true));

        }

        [TestMethod]
        public void TestHeaderRender()
        {
            Assert.AreEqual("[{\"Type\":\"HMarker\",\"Identifier\":\"h\",\"Header\":\"Genesis\"}]", buildJSON("\\h Genesis"));
            Assert.AreEqual("[{\"Type\":\"HMarker\",\"Identifier\":\"h\",\"Header\":\"1 John\"}]", buildJSON("\\h 1 John"));
            Assert.AreEqual("[{\"Type\":\"HMarker\",\"Identifier\":\"h\",\"Header\":\"\"}]", buildJSON("\\h"));
            Assert.AreEqual("[{\"Type\":\"HMarker\",\"Identifier\":\"h\",\"Header\":\"\"}]", buildJSON("\\h     "));

        }
        [TestMethod]
        public void TestChapterRender()
        {
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"5\",\"Contents\":[]}]", buildJSON("\\c 5"));
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[]}]", buildJSON("\\c 1"));
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"-1\",\"Contents\":[]}]", buildJSON("\\c -1"));
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"0\",\"Contents\":[]}]", buildJSON("\\c 0"));
        }
        [TestMethod]
        public void TestVerseRender()
        {
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"This is a simple verse.\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse."));
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"This is a simple verse.\"}]},{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"2\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"verse 2\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\v 2 verse 2"));
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"This is a simple verse.\"}]}]},{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"2\",\"Contents\":[{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"2\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"verse 2\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\c 2 \\v 2 verse 2"));
        }
        [TestMethod]
        public void TestFootnoteRender()
        {
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"This is a simple verse.\"},{\"Type\":\"FMarker\",\"Identifier\":\"f\",\"Caller\":\"+\",\"Contents\":[{\"Type\":\"FTMarker\",\"Identifier\":\"ft\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"Hello Friend\"}]}]},{\"Type\":\"FEndMarker\",\"Identifier\":\"f*\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\f + \\ft Hello Friend \\f*"));
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"This is a simple verse.\"},{\"Type\":\"FMarker\",\"Identifier\":\"f\",\"Caller\":\"+\",\"Contents\":[{\"Type\":\"FTMarker\",\"Identifier\":\"ft\",\"Contents\":[{\"Type\":\"FQAMarker\",\"Identifier\":\"fqa\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"Hello Fried Friend\"}]}]}]},{\"Type\":\"FEndMarker\",\"Identifier\":\"f*\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\f + \\ft \\fqa Hello Fried Friend \\f*"));
            Assert.AreEqual("[{\"Type\":\"CMarker\",\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"VMarker\",\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"This is a simple verse.\"},{\"Type\":\"FMarker\",\"Identifier\":\"f\",\"Caller\":\"+\",\"Contents\":[{\"Type\":\"FTMarker\",\"Identifier\":\"ft\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"I say\"},{\"Type\":\"FQAMarker\",\"Identifier\":\"fqa\",\"Contents\":[{\"Type\":\"TextBlock\",\"Text\":\"Hello Fried Friend\"}]}]}]},{\"Type\":\"FEndMarker\",\"Identifier\":\"f*\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\f + \\ft I say \\fqa Hello Fried Friend \\f*"));

        }
        public string buildJSON(string usfm)
        {
            render.clearJSONElements();
            return render.Render(parser.ParseFromString(usfm));
        }

    }
}
