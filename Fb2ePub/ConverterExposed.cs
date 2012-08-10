using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Fb2ePub
{
    //[Guid("A7A4EF1F-C394-4E83-9C6B-6D55E864C801")]
    interface IConverterExposed
    {
        bool Convert(List<string> files, string outputFolder);

        bool Convert(string inputPath, string outputFolder);
    }
}
