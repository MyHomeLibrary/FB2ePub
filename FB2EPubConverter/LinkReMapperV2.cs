﻿using System.Collections.Generic;
using ConverterContracts.ConversionElementsStyles;
using EPubLibrary.ReferenceUtils;
using EPubLibrary.XHTML_Items;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter
{
    internal class LinkReMapperV2
    {
        private readonly string _idString;
        private readonly IHTMLItem _linkTargetItem;
        private readonly BaseXHTMLFileV2 _linkTargetDocument;
        private readonly IHTMLItem _linkParentContainer;
        private readonly KeyValuePair<string, List<Anchor>> _link;
        private readonly BookStructureManager _structureManager;

        private int _linksCount;

        public LinkReMapperV2(KeyValuePair<string, List<Anchor>> link, Dictionary<string, IHTMLItem> ids, BookStructureManager structureManager)
        {
            _link = link;
            _structureManager = structureManager;
            _idString = ReferencesUtils.GetIdFromLink(link.Key); // Get ID of a link target;
            _linkTargetItem = ids[_idString]; // get object targeted by link
            _linkTargetDocument = GetIDParentDocument(structureManager, _linkTargetItem); // get parent document (file) containing targeted object
            if (_linkTargetDocument != null)
            {
                _linkParentContainer = DetectItemParentContainer(_linkTargetItem); // get parent container of link target item
            }
        }

        public void Remap()
        {
            if (_linkTargetDocument == null)
            {
                Logger.Log.Error(string.Format("Internal consistency error - Used ID ({0}) has to be in one of the book documents objects", _linkTargetItem));
                return;
            }
            if (_linkParentContainer == null) // if no parent container found , means the link is directly to document , which can't be , so we ignore
            {
                Logger.Log.Error(string.Format("Internal consistency error - target link item ( {0} )has no parent container", _linkTargetItem));
                return;
            }
            if (_linkTargetDocument.Type == SectionTypeEnum.Links) // if it's FBE notes section
            {
                RemapLinkSecionReference();
            }
            else
            {
                RemapNormalReference();
            }
        }

        private void RemapNormalReference()
        {
            foreach (var anchor in _link.Value)
            {
                BaseXHTMLFileV2 anchorDocument = GetIDParentDocument(_structureManager, anchor); // get document containing anchor pointing to target ID
                if (anchorDocument == null) // if anchor not contained (not found) in any document
                {
                    Logger.Log.Error(string.Format("Internal consistency error - anchor ({0}) for id ({1}) not contained (not found) in any document", anchor, _linkTargetItem));
                    continue;
                }
                // if in same document - local reference (link) , if in different - create link to that document
                anchor.HRef.Value = (anchorDocument == _linkTargetDocument) ? GenerateLocalLinkReference(_idString) : GenerateFarLinkReference(_idString, _linkTargetDocument.FileName);
            }                   
        }

        private void RemapLinkSecionReference()
        {
            foreach (var anchor in _link.Value)
            {
                BaseXHTMLFileV2 anchorDocument = GetIDParentDocument(_structureManager, anchor); // get document containing anchor pointing to target ID
                if (anchorDocument == null) // if anchor not contained (not found) in any document
                {
                    Logger.Log.Error(string.Format("Internal consistency error - anchor ({0}) for id ({1}) not contained (not found) in any document", anchor, _linkTargetItem));
                    continue;
                }
                string backlinkRef;
                if (anchorDocument == _linkTargetDocument) // if anchor (link) and target (id) located in same document
                {
                    anchor.HRef.Value = GenerateLocalLinkReference(_idString);// update reference link for an anchor, local one (without file name)
                    backlinkRef = GenerateLocalLinkReference(anchor.GlobalAttributes.ID.Value as string); // in case we going to insert backlin - create a local reference
                }
                else // if they are located in different documents
                {
                    anchor.HRef.Value = GenerateFarLinkReference(_idString, _linkTargetDocument.FileName); // update reference link for an anchor, "far" one (with, pointing to another file name)
                    backlinkRef = GenerateFarLinkReference(anchor.GlobalAttributes.ID.Value as string, anchorDocument.FileName); // in case we going to insert backlin - create a "far" reference
                }
                var backLinkAnchor = new Anchor(_linkParentContainer.HTMLStandard);
                backLinkAnchor.HRef.Value = backlinkRef;
                backLinkAnchor.GlobalAttributes.Class.Value = ElementStylesV2.NoteAnchor;
                _linkParentContainer.Add(new EmptyLine(_linkParentContainer.HTMLStandard));
                _linkParentContainer.Add(backLinkAnchor);
                _linksCount++;
                backLinkAnchor.Add(new SimpleHTML5Text(backLinkAnchor.HTMLStandard) { Text = (_link.Value.Count > 1) ? string.Format("(<< back {0})  ", _linksCount) : string.Format("(<< back)  ") });
            }

        }

        private string GenerateLocalLinkReference(string idToReference)
        {
            return string.Format("#{0}", idToReference);
        }

        private string GenerateFarLinkReference(string idToReference, string fileName)
        {
            return string.Format("{0}#{1}", fileName, idToReference);
        }



        private BaseXHTMLFileV2 GetIDParentDocument(BookStructureManager structureManager, IHTMLItem value)
        {
            return structureManager.GetItemParentDocument(value) as BaseXHTMLFileV2;
        }

        /// <summary>
        /// Detect parent container of the element
        /// </summary>
        /// <param name="referencedItem"></param>
        /// <returns></returns>
        private IHTMLItem DetectItemParentContainer(IHTMLItem referencedItem)
        {
            if (referencedItem is IBlockElement) // if item itself is container - return it
            {
                return referencedItem;
            }
            if (referencedItem.Parent != null)
            {
                if (referencedItem.Parent is IBlockElement) // if item is located inside container
                {
                    return referencedItem.Parent;
                }
                if (referencedItem.Parent is TextBasedElement) // if parent is text, i's ok for container
                {
                    return referencedItem.Parent;
                }
                return DetectItemParentContainer(referencedItem.Parent); // go up the inclusion chain
            }

            return null;
        }

    }
}
