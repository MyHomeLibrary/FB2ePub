using System;
using FB2EPubConverter.ElementConvertersV3.Poem;
using FB2EPubConverter.ElementConvertersV3.Tables;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class AnnotationConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
        public int Level { get; set; }
    }

    internal class AnnotationConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 annotation element
        /// </summary>
        /// <param name="annotationItem">item to convert</param>
        /// <param name="converterParams"></param>
        /// <returns>XHTML representation</returns>
        public HTMLItem Convert(AnnotationType annotationItem, AnnotationConverterParamsV3 converterParams)
        {
            if (annotationItem == null)
            {
                throw new ArgumentNullException("annotationItem");
            }
            var resAnnotation = new Div(HTMLElementType.HTML5);

            foreach (var element in annotationItem.Content)
            {
                if (element is SubTitleItem)
                {
                    var subtitleConverter = new SubtitleConverterV3();
                    resAnnotation.Add(subtitleConverter.Convert(element as SubTitleItem,
                        new SubtitleConverterParamsV3 { Settings = converterParams.Settings }));
                }
                else if (element is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverterV3();
                    resAnnotation.Add(paragraphConverter.Convert(element as ParagraphItem,
                        new ParagraphConverterParamsV3 { Settings = converterParams.Settings, ResultType = ParagraphConvTargetEnumV3.Paragraph, StartSection = false }));
                }
                else if (element is PoemItem)
                {
                    var poemConverter = new PoemConverterV3();
                    resAnnotation.Add(poemConverter.Convert(element as PoemItem,
                        new PoemConverterParamsV3 { Level = converterParams.Level + 1, Settings = converterParams.Settings }));
                }
                else if (element is CiteItem)
                {
                    var citationConverter = new CitationConverterV3();
                    resAnnotation.Add(citationConverter.Convert(element as CiteItem,
                        new CitationConverterParamsV3 { Level = converterParams.Level + 1, Settings = converterParams.Settings }));
                }
                else if (element is TableItem)
                {
                    var tableConverter = new TableConverterV3();
                    resAnnotation.Add(tableConverter.Convert(element as TableItem,
                        new TableConverterParamsV3 { Settings = converterParams.Settings }
                        ));
                }
                else if (element is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverterV3();
                    resAnnotation.Add(emptyLineConverter.Convert());
                }
            }

            resAnnotation.GlobalAttributes.ID.Value = converterParams.Settings.ReferencesManager.AddIdUsed(annotationItem.ID, resAnnotation);

            SetClassType(resAnnotation, "annotation");
            return resAnnotation;
        }
    }
}
