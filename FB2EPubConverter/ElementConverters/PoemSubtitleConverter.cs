using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB2EPubConverter.ElementConverters
{
    class PoemSubtitleConverter : SubtitleConverter
    {
        public override string GetElementType()
        {
            return "poem_subtitle";
        }
    }
}
