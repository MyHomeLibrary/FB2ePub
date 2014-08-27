using System;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    public class Sizes : IAttributeDataType
    {

private enum SizesValues
        {
            Invalid,
            Any,
            Values,
        }

        private struct Pair
        {
            public int X;
            public int Y;
        }

        private SizesValues _type = SizesValues.Invalid;
        private Pair _sizes;


        public string Value
        {
            get
            {
                switch (_type)
                {
                    case SizesValues.Any:
                        return "any";
                    case SizesValues.Values:
                        return string.Format("{0}x{1}", _sizes.X, _sizes.Y);
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "any":
                        _type = SizesValues.Any;
                        break;
                    default:
                        int indexOfX = value.IndexOf("x", StringComparison.InvariantCulture);
                        if (indexOfX != -1 && indexOfX + 1 < value.Length)
                        {
                            string before = value.Substring(0, indexOfX);
                            string after = value.Substring(indexOfX + 1);
                            int x;
                            int y;
                            if (int.TryParse(before, out x) && int.TryParse(after, out y))
                            {
                                _sizes.X = x;
                                _sizes.Y = y;
                                _type = SizesValues.Values;
                            }
                        }
                        _type = SizesValues.Invalid;
                        break;
                }
            }
        }
    }
}
