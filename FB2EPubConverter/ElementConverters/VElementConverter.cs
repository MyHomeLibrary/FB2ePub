using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class VElementConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts "v" (poem) sub-element
        /// </summary>
        /// <returns>XHTML formated representation</returns>
        public IHTMLItem Convert(VPoemParagraph paragraphItem)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var paragraphConverter = new ParagraphConverter {Settings = Settings,};
            var item = paragraphConverter.Convert(paragraphItem,ParagraphConvTargetEnum.Paragraph);

            SetClassType(item);

            //item.ID.Value = Settings.ReferencesManager.AddIdUsed(paragraphItem.ID, item);

            return item;
        }

        public override string GetElementType()
        {
            return "v";
        }
    }
}
