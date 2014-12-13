using System.Collections.Generic;
using EPubLibrary;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter
{
    interface IHRefManager
    {
        bool FlatStructure { get; set; }
        void Reset();
        string AddImageRefferenced(InlineImageItem item, Image img);
        string AddImageRefferenced(ImageItem item, Image img);
        string AddIdUsed(string id, IHTMLItem item);
        void AddReference(string reference, Anchor anchor);
        void AddBackReference(string reference, Anchor anchor);
        void RemoveInvalidAnchors();
        string EnsureGoodId(string id);
        string EnsureGoodReference(string reference);
        void RemapAnchors(EPubFile epubFile);
        void RemoveInvalidImages(Dictionary<string, BinaryItem> dictionary);
    }
}
