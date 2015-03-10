using System;
using System.Collections.Generic;
using System.Text;
using EPubLibraryContracts;

namespace EPubLibrary
{
    class SectionIDTracker
    {
        private class RegistrationNode
        {
            private readonly IList<long> _idLevels;

            public long ChildrenCount { get; set; }

            public RegistrationNode(List<long> idLevels)
            {
                _idLevels = idLevels;
            }

            private string GenerateID()
            {
                var sb = new StringBuilder();
                sb.Append(@"bookcontent");
                foreach (var t in _idLevels)
                {
                    sb.AppendFormat("_{0}",t);
                }
                return sb.ToString();
            }           

            public string ID
            {
                get { return GenerateID(); }
            }

            public IList<long> IdLevels
            {
                get { return _idLevels; }
            }
        }

        private readonly Dictionary<IBaseXHTMLFile, RegistrationNode> _sectionMapping = new Dictionary<IBaseXHTMLFile, RegistrationNode>();
        private long _topLevelCounter;

        public string GenerateSectionId(IBaseXHTMLFile section)
        {
            if (!_sectionMapping.ContainsKey(section))
            {
                _sectionMapping.Add(section,GenerateNewRegistration(section));
            }
            return _sectionMapping[section].ID;
        }

        private RegistrationNode GenerateNewRegistration(IBaseXHTMLFile section)
        {
            List<long> idLevels = new List<long>();

            long ownCount;
            if (section.NavigationParent != null)
            {
                if (!_sectionMapping.ContainsKey(section.NavigationParent))
                {
                    throw new ArgumentException("Section to add has a parent, but parent not added, add parent first");
                }
                _sectionMapping[section.NavigationParent].ChildrenCount++; // increase number of children
                ownCount = _sectionMapping[section.NavigationParent].ChildrenCount;
                idLevels.AddRange(_sectionMapping[section.NavigationParent].IdLevels);
            }
            else
            {
                ownCount = _topLevelCounter++;
            }
            idLevels.Add(ownCount);
            return new RegistrationNode(idLevels) {ChildrenCount = 0};
        }
    }
}
