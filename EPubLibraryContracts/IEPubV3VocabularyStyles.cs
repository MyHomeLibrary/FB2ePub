namespace EPubLibraryContracts
{
    public interface IEPubV3VocabularyStyles
    {
        void SetType(EpubV3Vocabulary typeStyle);
        void Remove(EpubV3Vocabulary typeStyle);
        void Clear();
        bool IsOfType(EpubV3Vocabulary typeStyle);
        bool IsPresent();
    }
}
