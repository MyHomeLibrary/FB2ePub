using System;
using EPubLibraryContracts;

namespace EPubLibrary.PathUtils
{

    public class PathElement : IPathElement
    {
        private readonly PathType _pathType;
        private readonly string _name;

        public PathElement(string name, PathType type)
        {
            if (string.IsNullOrEmpty(name) && type != PathType.Root)
            {
                throw new ArgumentException("Can't have empty name for non root path element");
            }
            _pathType = type;
            _name = name;
        }


        public PathType Type
        {
            get { return _pathType; }
        }



        public string Name
        {
            get { return _name; }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
