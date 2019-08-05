using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Renderers.JSON
{
    public class JSONConfig
    {
        public bool isMinified;
        public JSONConfig(bool isMinified = false)
        {
            this.isMinified = isMinified;
        }
    }
}
