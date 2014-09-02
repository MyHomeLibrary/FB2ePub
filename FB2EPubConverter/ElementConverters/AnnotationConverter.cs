using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class AnnotationConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 annotation elememt
        /// </summary>
        /// <param name="annotationItem">item to convert</param>
        /// <param name="level">"deepness" of the annotation, affects representation</param>
        /// <returns>XHTML representation</returns>
        public Div Convert(AnnotationType annotationItem, int level)
        {
            if (annotationItem == null)
            {
                throw new ArgumentNullException("annotationItem");
            }
            Div resAnnotation = new Div();

            foreach (var element in annotationItem.Content)
            {
                if (element is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverter {Settings = Settings};
                    resAnnotation.Add(paragraphConverter.Convert(element as ParagraphItem,ParagraphConvTargetEnum.Paragraph));
                }
                else if (element is PoemItem)
                {
                    PoemConverter poemConverter = new PoemConverter {Settings = Settings};
                    resAnnotation.Add(poemConverter.Convert(element as PoemItem,level + 1));
                }
                else if (element is CiteItem)
                {
                    CitationConverter citationConverter = new CitationConverter {Settings = Settings};
                    resAnnotation.Add(citationConverter.Convert(element as CiteItem,level + 1));
                }
                else if (element is SubTitleItem)
                {
                    SubtitleConverter subtitleConverter = new SubtitleConverter {Settings = Settings};
                    resAnnotation.Add(subtitleConverter.Convert(element as SubTitleItem));
                }
                else if (element is TableItem)
                {
                    TableConverter tableConverter = new TableConverter {Settings = Settings};
                    resAnnotation.Add(tableConverter.Convert(element as TableItem));
                }
                else if (element is EmptyLineItem)
                {
                    EmptyLineConverter emptyLineConverter = new EmptyLineConverter();
                    resAnnotation.Add(emptyLineConverter.Convert());
                }
            }

            resAnnotation.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(annotationItem.ID, resAnnotation);

            SetClassType(resAnnotation);
            return resAnnotation;
        }

        public override string GetElementType()
        {
            return "annotation";
        }
    }
}
