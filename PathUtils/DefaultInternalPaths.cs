namespace EPubLibrary.PathUtils
{
    static internal class DefaultInternalPaths
    {
        public static readonly EPubInternalPath ContentFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/content.opf");
        public static readonly EPubInternalPath TOCFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/toc.ncx");

    }
}
