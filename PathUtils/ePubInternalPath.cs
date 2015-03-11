using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibraryContracts;

namespace EPubLibrary.PathUtils
{
    public class EPubInternalPath : IEPubInternalPath
    {
        private bool _supportFlatStructure = true;

        public const string DefaultOebpsFolder = "OEBPS";

        private readonly List<IPathElement> _path = new List<IPathElement>();


        public IList<IPathElement> Path { get { return _path; }}
        public static EPubInternalPath GetDefaultLocation(DefaultLocations location)
        {
            switch (location)
            {
                case DefaultLocations.DefaultTextFolder:
                    return GetDefaultTextFilesFolder();
                case DefaultLocations.DefaultLicenseFolder:
                    return new EPubInternalPath(DefaultOebpsFolder + "/license/");
                case DefaultLocations.DefaultImagesFolder:
                    return new EPubInternalPath(DefaultOebpsFolder + "/images/");
            }
            throw new ArgumentException(@"Unknown path type","location");
        }

        public EPubInternalPath(string zipPath)
        {
            string resolvedZipPath = zipPath.Replace('\\', '/');
            if (resolvedZipPath.EndsWith("/"))
            {
                resolvedZipPath =   resolvedZipPath.TrimEnd('/');
            }
            string[] pathArray = resolvedZipPath.Split('/');
            LoadAsPath(pathArray);
        }

        public EPubInternalPath(EPubInternalPath folderPath, string zipPath)
        {
            string folder = folderPath.GetPathWithoutFileNameAsString();
            string path = folder + zipPath;
            string[] pathArray = path.Split('/');
            LoadAsPath(pathArray);
        }


        private static EPubInternalPath GetDefaultTextFilesFolder()
        {
            return new EPubInternalPath(DefaultOebpsFolder + "/text/");
        }

        /// <summary>
        /// Set if path support flat structure or not (should always be placed at specific location )
        /// </summary>
        public bool SupportFlatStructure { 
            get { return _supportFlatStructure; }
            set { _supportFlatStructure = value; }
        }

        /// <summary>
        /// Returns type of the path contained (folder or file)
        /// </summary>
        /// <returns></returns>
        public PathType GetPathType()
        {
            if (_path.Count <= 1)
            {
                return PathType.Root;
            }
            if (_path[_path.Count - 1].Type == PathType.File)
            {
                return PathType.File;
            }
            return PathType.Folder;
        }

        private void LoadAsPath(string[] pathArray)
        {
            _path.Clear();
            // first element is always root
            _path.Add(new PathElement(string.Empty, PathType.Root));
            for (int i = 0; i < pathArray.Length; i++)
            {
                PathType type = PathType.Folder;
                if (i == pathArray.Length - 1) // for the last element in the path
                {
                    // we assume that if it contains "." then it's a file
                    // so last folder in path without a filename can't contain "." and filename has to have "." otherwise it will be miss detected
                    type = pathArray[i].Contains('.') ? PathType.File : PathType.Folder;
                }
                if (!string.IsNullOrEmpty(pathArray[i])) // for case when coping
                {
                    _path.Add(new PathElement(pathArray[i], type));
                }
            }
        }

        /// <summary>
        /// Returns number of elements in  path chain 
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfElementsInChain()
        {
            return _path.Count;
        }


        /// <summary>
        /// Returns specific element in path chain data
        /// </summary>
        /// <param name="elementOrder">element to return</param>
        /// <param name="name">name of the element</param>
        /// <param name="type">type of the element</param>
        public void GetElement(int elementOrder, out string name, out PathType type)
        {
            if (elementOrder >= _path.Count)
            {
                throw new ArgumentException(string.Format("Requested element order {0} exceeds number of elements present {1}",elementOrder,_path.Count));
            }
            if (elementOrder < 0)
            {
                throw new ArgumentException("Element order can't be negative");
            }
            name = _path[elementOrder].Name;
            type = _path[elementOrder].Type;
        }

        /// <summary>
        /// Returns path to the object inside this always assuming it's not a flat structure
        /// </summary>
        /// <returns>internal path</returns>
        private string GetFilePathInZip()
        {
            // we assume the structure is valid and file name can be only at last element
            StringBuilder result = new StringBuilder();
            foreach (IPathElement t in _path)
            {
                switch (t.Type)
                {
                    case PathType.Root:
                        result.Append('/');
                        break;
                    case PathType.Folder:
                        result.AppendFormat("{0}/", t.Name);
                        break;
                    case PathType.File:
                        result.Append(t.Name);
                        break;
                }
            }
            return result.ToString();           
        }

        /// <summary>
        /// Returns path to object inside a ZIP (ePub)
        /// </summary>
        /// <returns>internal path</returns>
        public string GetFilePathInZip(bool flatStructure)
        {
            if (flatStructure && _supportFlatStructure)
            {
                return _path[_path.Count - 1].Name;
            }
            return GetFilePathInZip();
        }


        /// <summary>
        /// Get relative path , relative to another object 
        /// based on most common root
        /// </summary>
        /// <param name="otherObject"></param>
        /// <param name="flatStructure"></param>
        /// <returns></returns>
        public string GetRelativePath(IEPubInternalPath otherObject, bool flatStructure)
        {
            if (flatStructure && _supportFlatStructure)
            {
                if (_path.Count < 1)
                {
                    throw new Exception("Path has to contain at least one element besides root");
                }
                return _path[_path.Count - 1].Name;
            }
            int commongPathIndex = 0;
            // locate position where common path starts
            while ((commongPathIndex < _path.Count)
                && (commongPathIndex < otherObject.Path.Count)
                && (0 == String.Compare(_path[commongPathIndex].Name, otherObject.Path[commongPathIndex].Name, StringComparison.OrdinalIgnoreCase)))
            {
                commongPathIndex++;
            }
            var sb = new StringBuilder();
            int i = commongPathIndex;
            // for all not common folders in path depth of other object we need to go folder up
            while ( (i < otherObject.Path.Count) 
                && (otherObject.Path[i].Type != PathType.File))
            {
                sb.Append("../");
                i++;
            }
            // add the rest of the path starting from the common root
            for (i = commongPathIndex; i < _path.Count; i++)
            {
                if (_path[i].Type == PathType.Folder)
                {
                    sb.AppendFormat("{0}/",_path[i].Name);
                }
                else if (_path[i].Type == PathType.File)
                {
                    sb.Append(_path[i].Name);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Return Path only part of the path
        /// </summary>
        /// <returns></returns>
        public IEPubInternalPath GetPathWithoutFileName()
        {
            EPubInternalPath newPathObject = (EPubInternalPath)MemberwiseClone();
            newPathObject._path.Clear();
            foreach (var pathElement in _path) // copy all without Filename
            {
                var element = (PathElement) pathElement;
                if (element.Type != PathType.File)
                {
                    newPathObject._path.Add((PathElement)element.Clone());
                }
            }
            return newPathObject;
        }

        /// <summary>
        /// return path without file name as string
        /// </summary>
        /// <returns></returns>
        public string GetPathWithoutFileNameAsString()
        {
            // we assume the structure is valid and file name can be only at last element
            StringBuilder result = new StringBuilder();
            foreach (IPathElement t in _path)
            {
                switch (t.Type)
                {
                    case PathType.Root:
                        result.Append('/');
                        break;
                    case PathType.Folder:
                        result.AppendFormat("{0}/", t.Name);
                        break;
                }
            }
            return result.ToString();           
        }

        public object Clone()
        {
            EPubInternalPath newPath = (EPubInternalPath)MemberwiseClone();
            newPath._path.Clear();
            foreach (var pathElement in _path)
            {
                var element = (PathElement) pathElement;
                newPath._path.Add((PathElement)element.Clone());
            }
            return newPath;
        }

        public string GetRelativePath(string path, bool flatStructure)
        {
            EPubInternalPath newPath = new EPubInternalPath(path);
            return GetRelativePath(newPath, flatStructure);
        }

        public override string ToString()
        {
            return GetFilePathInZip();
        }
    }
}
