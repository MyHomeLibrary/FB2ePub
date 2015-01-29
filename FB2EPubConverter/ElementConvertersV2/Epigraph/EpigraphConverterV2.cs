using System;
using FB2EPubConverter.ElementConvertersV2.Poem;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2.Epigraph
{
    internal class EpigraphConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }    
        public int Level { get; set; }
    }

    internal class EpigraphConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 epigraph element
        /// </summary>
        /// <param name="epigraphItem"></param>
        /// <param name="epigraphConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public Div Convert(EpigraphItem epigraphItem,EpigraphConverterParamsV2 epigraphConverterParams)
        {
            if (epigraphItem == null)
            {
                throw new ArgumentNullException("epigraphItem");
            }
            var content = new Div(HTMLElementType.XHTML11);

            foreach (var element in epigraphItem.EpigraphData)
            {
                if (element is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverterV2();
                    content.Add(paragraphConverter.Convert(element as ParagraphItem, 
                        new ParagraphConverterParamsV2 {ResultType = ParagraphConvTargetEnumV2.Paragraph , Settings = epigraphConverterParams.Settings, StartSection = false}));
                }
                if (element is PoemItem)
                {
                    var poemConverter = new PoemConverterV2();
                    content.Add(poemConverter.Convert(element as PoemItem,
                        new PoemConverterParamsV2 { Level = epigraphConverterParams.Level + 1 ,Settings = epigraphConverterParams.Settings}
                        ));
                }
                if (element is CiteItem)
                {
                    var citationConverter = new CitationConverterV2();
                    content.Add(citationConverter.Convert(element as CiteItem, 
                        new CitationConverterParamsV2 { Level = epigraphConverterParams.Level + 1,Settings = epigraphConverterParams.Settings}));
                }
                if (element is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverterV2();
                    content.Add(emptyLineConverter.Convert());
                }
            }

            foreach (var author in epigraphItem.TextAuthors)
            {
                var epigraphAuthorConverter = new EpigraphAuthorConverterV2();
                content.Add(epigraphAuthorConverter.Convert(author as TextAuthorItem,  new EpigraphAuthorConverterParamsV2 { Settings = epigraphConverterParams.Settings }));
            }

            SetClassType(content,"epigraph");

            content.GlobalAttributes.ID.Value = epigraphConverterParams.Settings.ReferencesManager.AddIdUsed(epigraphItem.ID, content);

            return content;
        }
    }
}
