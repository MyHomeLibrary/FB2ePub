using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{
    class PoemSubtitleConverter : BaseElementConverter
    {

        public IHTMLItem Convert(SubTitleItem subtitleItem, HTMLElementType compatibility, SubtitleConverterParams subtitleConverterParams)
        {
            if (subtitleItem == null)
            {
                throw new ArgumentNullException("subtitleItem");
            }
            //Div subtitle = new Div();
            var paragraphConverter = new ParagraphConverter();
            var internalData = paragraphConverter.Convert(subtitleItem, compatibility,
                new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = subtitleConverterParams.Settings, StartSection = false });
            SetClassType(internalData, "poem_subtitle");
            //subtitle.Add(internalData);

            //SetClassType(subtitle);
            //return subtitle;
            return internalData;
        }
    }
}
