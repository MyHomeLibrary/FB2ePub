using System;
using System.Collections.Generic;
using EPubLibrary;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter
{
    internal class HRefManagerV3 
    {
        public bool FlatStructure { get; set; }

        public void Reset()
        {
            
        }

        public string AddImageRefferenced(InlineImageItem item, Image img)
        {
            throw new NotImplementedException();
        }

        public string AddImageRefferenced(ImageItem item, Image img)
        {
            throw new NotImplementedException();
        }

        public string AddIdUsed(string id, IHTMLItem item)
        {
            throw new NotImplementedException();
        }

        public void AddReference(string reference, Anchor anchor)
        {
            throw new NotImplementedException();
        }

        public void AddBackReference(string reference, Anchor anchor)
        {
            throw new NotImplementedException();
        }

        public void RemoveInvalidAnchors()
        {
            throw new NotImplementedException();
        }

        public string EnsureGoodId(string id)
        {
            throw new NotImplementedException();
        }

        public string EnsureGoodReference(string reference)
        {
            throw new NotImplementedException();
        }

        public void RemapAnchors(EPubFileV3 epubFile)
        {
            throw new NotImplementedException();
        }

        public void RemoveInvalidImages(Dictionary<string, BinaryItem> dictionary)
        {
            throw new NotImplementedException();
        }
    }
}
