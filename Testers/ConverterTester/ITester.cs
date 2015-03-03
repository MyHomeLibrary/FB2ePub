using System;

namespace ConverterTester
{
    public interface ITester
    {
        bool Test();
        string Name { get; }
        Exception TestError { get; }
    }
}
