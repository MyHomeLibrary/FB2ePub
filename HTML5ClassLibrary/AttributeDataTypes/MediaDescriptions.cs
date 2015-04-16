using System.Collections.Generic;
using System.Text;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A comma-separated list of media descriptors. 
    /// The following is a list of recognized media descriptors: screen, tty, tv, projection, handheld, print, braille, aural and all.
    /// </summary>
    public class MediaDescriptions : IAttributeDataType
    {
        private enum MediaDescriptionsEnum
        {
            Invalid,
            Screen, 
            Tty, 
            Tv, 
            Projection, 
            Handheld, 
            Print, 
            Braille, 
            Aural , 
            All,
        }

        private class MediaDescription
        {
            private MediaDescriptionsEnum _description = MediaDescriptionsEnum.Invalid;

            public string Value
            {
                get
                {
                    switch (_description)
                    {
                        case MediaDescriptionsEnum.Screen:
                            return "screen";
                        case MediaDescriptionsEnum.Tty:
                            return "tty";
                        case MediaDescriptionsEnum.Tv:
                            return "tv";
                        case MediaDescriptionsEnum.Projection:
                            return "projection";
                        case MediaDescriptionsEnum.Handheld:
                            return "handheld";
                        case MediaDescriptionsEnum.Print:
                            return "print";
                        case MediaDescriptionsEnum.Braille:
                            return "braille";
                        case MediaDescriptionsEnum.Aural:
                            return "aural";
                        case MediaDescriptionsEnum.All:
                            return "all";
                    }
                    return string.Empty;
                }

                set
                {
                    switch (value.ToLower())
                    {
                        case "tty":
                            _description = MediaDescriptionsEnum.Tty;
                            break;
                        case "tv":
                            _description = MediaDescriptionsEnum.Tv;
                            break;
                        case "projection":
                            _description = MediaDescriptionsEnum.Projection;
                            break;
                        case "handheld":
                            _description = MediaDescriptionsEnum.Handheld;
                            break;
                        case "print":
                            _description = MediaDescriptionsEnum.Print;
                            break;
                        case "braille":
                            _description = MediaDescriptionsEnum.Braille;
                            break;
                        case "aural":
                            _description = MediaDescriptionsEnum.Aural;
                            break;
                        case "all":
                            _description = MediaDescriptionsEnum.All;
                            break;
                        default:
                            _description = MediaDescriptionsEnum.Invalid;
                            break;
                    }
                }
            }
            
        }

        private readonly List<MediaDescription> _descriptions = new List<MediaDescription>();

        public string Value
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (var description in _descriptions)
                {
                    builder.AppendFormat("{0},", description.Value);
                }
                if (builder.Length > 0)
                {
                    // remove last one
                    builder.Remove(builder.Length - 1, 1);
                }
                return builder.ToString();
               
            }

            set
            {
                _descriptions.Clear();
                string[] ar = value.Split(',');
                foreach (var s in ar)
                {
                    var description = new MediaDescription {Value = s};
                    if (!string.IsNullOrEmpty(description.Value))
                    {
                        _descriptions.Add(description);                        
                    }
                }
                
            }
        }
    }
}
