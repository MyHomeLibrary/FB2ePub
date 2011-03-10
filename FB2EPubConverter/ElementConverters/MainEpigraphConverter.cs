using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB2EPubConverter.ElementConverters
{
    internal class MainEpigraphConverter : EpigraphConverter
    {
        public override string GetElementType()
        {
            return "epigraph_main";
        }

    }
}
