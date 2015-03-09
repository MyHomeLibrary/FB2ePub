using System.Collections.Generic;
using EPubLibrary;
using EPubLibraryContracts;
using FB2Library.HeaderItems;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal static class SequencesInfoConverterV2
    {
        public static void Convert(ItemTitleInfo itemInfo, IBookInformationData titleInformation)
        {
            foreach (var seq in itemInfo.Sequences)
            {
                List<string> allSequences = DescriptionConverters.GetSequencesAsStrings(seq);
                if (allSequences.Count != 0)
                {
                    foreach (var sequence in allSequences)
                    {
                        titleInformation.Series.Add(sequence);
                        titleInformation.AllSequences.Add(sequence);
                    }
                }
            }
        }

    }
}
