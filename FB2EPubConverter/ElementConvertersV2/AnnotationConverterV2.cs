using System;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class AnnotationConverterParams
    {
        public ConverterOptionsV2 Settings { get; set; }
        public int Level { get; set; }
    }

    internal class AnnotationConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 annotation element
        /// </summary>
        /// <param name="annotationItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="converterParams"></param>
        /// <returns>XHTML representation</returns>
        public HTMLItem Convert(AnnotationType annotationItem,HTMLElementType compatibility,AnnotationConverterParams converterParams)
        {
            if (annotationItem == null)
            {
                throw new ArgumentNullException("annotationItem");
            }
            var resAnnotation = new Div(compatibility);

            foreach (var element in annotationItem.Content)
            {
                if (element is SubTitleItem)
                {
                    var subtitleConverter = new SubtitleConverterV2();
                    resAnnotation.Add(subtitleConverter.Convert(element as SubTitleItem,compatibility,
                        new SubtitleConverterParamsV2{Settings = converterParams.Settings}));
                }
                else if (element is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverterV2();
                    resAnnotation.Add(paragraphConverter.Convert(element as ParagraphItem,compatibility,
                        new ParagraphConverterParams{ Settings = converterParams.Settings,ResultType = ParagraphConvTargetEnum.Paragraph, StartSection = false}));
                }
                else if (element is PoemItem)
                {
                    var poemConverter = new PoemConverterV2();
                    resAnnotation.Add(poemConverter.Convert(element as PoemItem,compatibility,
                        new PoemConverterParams { Level = converterParams.Level + 1, Settings = converterParams.Settings}));
                }
                else if (element is CiteItem)
                {
                    var citationConverter = new CitationConverterV2();
                    resAnnotation.Add(citationConverter.Convert(element as CiteItem,compatibility ,
                        new CitationConverterParams {Level = converterParams.Level + 1,Settings = converterParams.Settings}));
                }
                else if (element is TableItem)
                {
                    var tableConverter = new TableConverterV2();
                    resAnnotation.Add(tableConverter.Convert(element as TableItem,compatibility,
                        new TableConverterParamsV2 { Settings = converterParams.Settings}
                        ));
                }
                else if (element is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverterV2();
                    resAnnotation.Add(emptyLineConverter.Convert(compatibility));
                }
            }

            resAnnotation.GlobalAttributes.ID.Value = converterParams.Settings.ReferencesManager.AddIdUsed(annotationItem.ID, resAnnotation);

            SetClassType(resAnnotation, "annotation");
            return resAnnotation;
        }
    }
}
