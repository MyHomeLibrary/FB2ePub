using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary;
using Fb2epubSettings;
using FB2Library.HeaderItems;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal static class TranslatorsInfoConverterV2
    {
        public static void Convert(ItemTitleInfo titleInfo, EPubFile epubFile, EPubCommonSettings settings)
        {
            foreach (var translator in titleInfo.Translators)
            {
                var person = new PersoneWithRole
                {
                    PersonName = epubFile.Transliterator.Translate(DescriptionConverters.GenerateAuthorString(translator, settings),
                        epubFile.TranslitMode),
                    FileAs = DescriptionConverters.GenerateFileAsString(translator, settings),
                    Role = RolesEnum.Translator,
                    Language = titleInfo.Language
                };
                epubFile.Title.Contributors.Add(person);
            }
        }

    }
}
