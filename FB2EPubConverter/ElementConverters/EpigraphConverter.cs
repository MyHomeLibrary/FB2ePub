using System;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EpigraphConverter : BaseElementConverter
    {

        /// <summary>
        /// Converts FB2 epigraph element
        /// </summary>
        /// <param name="epigraphItem"></param>
        /// <param name="level">"recursion" level</param>
        /// <returns>XHTML representation</returns>
        public Div Convert(EpigraphItem epigraphItem,int level)
        {
            if (epigraphItem == null)
            {
                throw new ArgumentNullException("epigraphItem");
            }
            var content = new Div();

            foreach (var element in epigraphItem.EpigraphData)
            {
                if (element is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverter {Settings = Settings};
                    content.Add(paragraphConverter.Convert(element as ParagraphItem,ParagraphConvTargetEnum.Paragraph));
                }
                if (element is PoemItem)
                {
                    var poemConverter = new PoemConverter {Settings = Settings};
                    content.Add(poemConverter.Convert(element as PoemItem,level + 1));
                }
                if (element is CiteItem)
                {
                    var citationConverter = new CitationConverter {Settings = Settings};
                    content.Add(citationConverter.Convert(element as CiteItem,level + 1));
                }
                if (element is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverter {Settings = Settings};
                    content.Add(emptyLineConverter.Convert());
                }
            }

            foreach (var author in epigraphItem.TextAuthors)
            {
                var epigraphAuthorConverter = new EpigraphAuthorConverter {Settings = Settings};
                content.Add(epigraphAuthorConverter.Convert(author as TextAuthorItem));
            }

            SetClassType(content);

            content.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(epigraphItem.ID, content);

            return content;
        }

        public override string GetElementType()
        {
            return "epigraph";
        }
    }
}
