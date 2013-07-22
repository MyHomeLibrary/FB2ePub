using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary.PathUtils
{
    public class EPubInternalPath : ICloneable
    {
        private bool _supportFlatStructure = true;

        public const string DefaultOebpsFolder = "OEBPS";

        private readonly List<PathElement> _path = new List<PathElement>();

        public EPubInternalPath(string zipPath)
        {
            string resolvedZipPath = zipPath.Replace('\\', '/');
            if (resolvedZipPath.EndsWith("/"))
            {
                resolvedZipPath =   resolvedZipPath.TrimEnd(new[] { '/' });
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


        private string GetFilePathInZip()
        {
            // we assume the structure is valid and file name can be only at last element
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _path.Count; i++)
            {
                switch (_path[i].Type)
                {
                    case PathType.Root:
                        result.Append('/');
                        break;
                    case PathType.Folder:
                        result.AppendFormat("{0}/", _path[i].Name);
                        break;
                    case PathType.File:
                        result.Append(_path[i].Name);
                        break;
                }
            }
            return result.ToString();           
        }

        /// <summary>
        /// Returns path to object inside a ZIP (ePub)
        /// </summary>
        /// <returns></returns>
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
        public string GetRelativePath(EPubInternalPath otherObject, bool flatStructure)
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
                && (commongPathIndex < otherObject._path.Count)
                && (0 == String.Compare(_path[commongPathIndex].Name, otherObject._path[commongPathIndex].Name, StringComparison.OrdinalIgnoreCase)))
            {
                commongPathIndex++;
            }
            StringBuilder sb = new StringBuilder();
            int i = commongPathIndex;
            // for all not common folders in path depth of other object we need to go folder up
            while ( (i < otherObject._path.Count) 
                && (otherObject._path[i].Type != PathType.File))
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
        public EPubInternalPath GetPathWithoutFileName()
        {
            string path = GetFilePathInZip();
            if (GetPathType() == PathType.File)
            {
                path = path.Substring(0, path.LastIndexOf('/'));
            }
            EPubInternalPath newPathObject = new EPubInternalPath(path);
            return newPathObject;
        }

        /// <summary>
        /// return path without file name as string
        /// </summary>
        /// <returns></returns>
        public string GetPathWithoutFileNameAsString()
        {
            string path = GetFilePathInZip();
            if (GetPathType() == PathType.File)
            {
                path = path.Substring(0, path.LastIndexOf('/'));
            }
            return path;
        }

        public object Clone()
        {
            EPubInternalPath newPath = new EPubInternalPath(GetFilePathInZip());
            return newPath;
        }

        public string GetRelativePath(string path, bool flatStructure)
        {
            EPubInternalPath newPath = new EPubInternalPath(path);
            return GetRelativePath(newPath, flatStructure);
        }
    }
}
