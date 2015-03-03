using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterTester
{
    public interface ITester
    {
        bool Test();
        string Name { get; }
    }
}
