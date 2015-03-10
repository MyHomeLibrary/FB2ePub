using System;
using System.Collections.Generic;

namespace EPubLibraryContracts
{
    public enum PathType
    {
        Root = 0,
        Folder = 1,
        File = 2,
    };

    public interface IPathElement : ICloneable
    {
        PathType Type { get; }
        string Name { get; }
    }

    public interface IEPubInternalPath : ICloneable
    {
        bool SupportFlatStructure { get; set; }
        PathType GetPathType();
        int GetNumberOfElementsInChain();
        void GetElement(int elementOrder, out string name, out PathType type);
        string GetFilePathInZip(bool flatStructure);
        string GetRelativePath(IEPubInternalPath otherObject, bool flatStructure);
        IEPubInternalPath GetPathWithoutFileName();
        string GetPathWithoutFileNameAsString();
        string GetRelativePath(string path, bool flatStructure);
        IList<IPathElement> Path { get; }
    }
}
