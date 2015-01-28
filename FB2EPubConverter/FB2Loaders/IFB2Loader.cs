using System.Collections.Generic;
using Fb2epubSettings;
using FB2Library;
using ConverterContracts.Settings;

namespace FB2EPubConverter.FB2Loaders
{
    interface IFB2Loader
    {
        List<FB2File> LoadFile(string fileName,IFB2ImportSettings settings);
    }
}
