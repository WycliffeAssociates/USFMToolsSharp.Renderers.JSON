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
            Assert.AreEqual("[{\"Identifier\":\"h\",\"Header\":\"Genesis\"}]", buildJSON("\\h Genesis"));
            Assert.AreEqual("[{\"Identifier\":\"h\",\"Header\":\"1 John\"}]", buildJSON("\\h 1 John"));
            Assert.AreEqual("[{\"Identifier\":\"h\",\"Header\":\"\"}]", buildJSON("\\h"));
            Assert.AreEqual("[{\"Identifier\":\"h\",\"Header\":\"\"}]", buildJSON("\\h     "));

        }
        [TestMethod]
        public void TestChapterRender()
        {
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"5\",\"Contents\":[]}]", buildJSON("\\c 5"));
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[]}]", buildJSON("\\c 1"));
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"-1\",\"Contents\":[]}]", buildJSON("\\c -1"));
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"0\",\"Contents\":[]}]", buildJSON("\\c 0"));
        }
        [TestMethod]
        public void TestVerseRender()
        {
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Text\":\"This is a simple verse.\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse."));
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Text\":\"This is a simple verse.\"}]},{\"Identifier\":\"v\",\"Number\":\"2\",\"Contents\":[{\"Text\":\"verse 2\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\v 2 verse 2"));
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Text\":\"This is a simple verse.\"}]}]},{\"Identifier\":\"c\",\"Number\":\"2\",\"Contents\":[{\"Identifier\":\"v\",\"Number\":\"2\",\"Contents\":[{\"Text\":\"verse 2\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\c 2 \\v 2 verse 2"));
        }
        [TestMethod]
        public void TestFootnoteRender()
        {
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Text\":\"This is a simple verse.\"},{\"Identifier\":\"f\",\"Caller\":\"+\",\"Contents\":[{\"Identifier\":\"ft\",\"Contents\":[{\"Text\":\"Hello Friend\"}]}]},{\"Identifier\":\"f*\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\f + \\ft Hello Friend \\f*"));
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Text\":\"This is a simple verse.\"},{\"Identifier\":\"f\",\"Caller\":\"+\",\"Contents\":[{\"Identifier\":\"ft\",\"Contents\":[{\"Identifier\":\"fqa\",\"Contents\":[{\"Text\":\"Hello Fried Friend\"}]}]}]},{\"Identifier\":\"f*\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\f + \\ft \\fqa Hello Fried Friend \\f*"));
            Assert.AreEqual("[{\"Identifier\":\"c\",\"Number\":\"1\",\"Contents\":[{\"Identifier\":\"v\",\"Number\":\"1\",\"Contents\":[{\"Text\":\"This is a simple verse.\"},{\"Identifier\":\"f\",\"Caller\":\"+\",\"Contents\":[{\"Identifier\":\"ft\",\"Contents\":[{\"Text\":\"I say\"},{\"Identifier\":\"fqa\",\"Contents\":[{\"Text\":\"Hello Fried Friend\"}]}]}]},{\"Identifier\":\"f*\"}]}]}]", buildJSON("\\c 1 \\v 1 This is a simple verse. \\f + \\ft I say \\fqa Hello Fried Friend \\f*"));

        }
        public string buildJSON(string usfm)
        {
            render.clearJSONElements();
            return render.Render(parser.ParseFromString(usfm));
        }

    }
}
