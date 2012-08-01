using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontsSettings
{

    /// <summary>
    /// Represent and manage fonts with their assignments
    /// </summary>
    public class CSSFontSettingsCollection 
    {
        private readonly Dictionary<string,CSSFontFamily> _fonts = new Dictionary<string, CSSFontFamily>();
        private readonly Dictionary<string, Dictionary<string, List<CSSFontFamily>>> _elements = new Dictionary<string, Dictionary<string, List<CSSFontFamily>>>();
        private readonly Dictionary<string,List<CSSFont>> _fontFiles = new Dictionary<string, List<CSSFont>>();

        private EPubFontSettings _storageSettings = null;
        private string _loadedDecoration = string.Empty;

        public const string MacroMask = "%ResourceFolder%";

        private string _resourcePath = string.Empty;

       

        public CSSFontSettingsCollection() {}

        public CSSFontSettingsCollection(EPubFontSettings settings)
        {
            Load(settings, string.Empty);
        }

        /// <summary>
        /// Return dictionary of all CSS elements
        /// </summary>
        public Dictionary<string, Dictionary<string, List<CSSFontFamily>>> CssElements { get { return _elements; } }

        /// <summary>
        /// Return dictionary containing all the fonts families
        /// </summary>
        public Dictionary<string, CSSFontFamily> Fonts { get { return _fonts; } }

        /// <summary>
        /// Return number of embedded files in collection
        /// </summary>
        public int NumberOfEmbededFiles 
        {
            get { return _fontFiles.Keys.Count; }
        }

        /// <summary>
        /// return list of all embeded files
        /// </summary>
        public List<string> EmbededFilesLocations
        {
            get { return _fontFiles.Keys.ToList(); }
        }


        /// <summary>
        /// Value (path)  %ResourceFolder% to be replaced 
        /// Can't be changed once loaded and applied
        /// </summary>
        public string ResourceMask
        {
            get { return _resourcePath; }
            set { _resourcePath = value;
                UpdateSourceLocations();
            }
        }

        /// <summary>
        /// Updates existing locations if not updated before
        /// </summary>
        private void UpdateSourceLocations()
        {
            foreach (var fontFileLocation in _fontFiles.Keys)
            {
                if (fontFileLocation.Contains(MacroMask.ToLower()))
                {
                    string newLocation = fontFileLocation.Replace(MacroMask.ToLower(), _resourcePath);
                    List<CSSFont> temp = _fontFiles[fontFileLocation];
                    _fontFiles.Remove(fontFileLocation);
                    if (_fontFiles.ContainsKey(newLocation))
                    {
                        _fontFiles[newLocation].AddRange(temp);
                    }
                    else
                    {
                        _fontFiles.Add(newLocation,temp);
                    }
                }
            }
        }

        /// <summary>
        /// Load settings into a workacle helper class structure
        /// </summary>
        /// <param name="settings">settings to load</param>
        /// <param name="decoration">decoration to be added to family font names</param>
        public void Load(EPubFontSettings settings, string decoration)
        {
            // Copy all font families 
            _fonts.Clear();
            _fontFiles.Clear();
            foreach (var cssFontFamily in settings.FontFamilies)
            {
                CSSFontFamily newFamily = new CSSFontFamily();
                newFamily.CopyFrom(cssFontFamily);
                newFamily.DecorationId = decoration;
                _fonts.Add(newFamily.Name,newFamily);
                foreach (var cssFont in newFamily.Fonts) // go over all fonts in family
                {
                    foreach (var fontSource in cssFont.Sources) // and all sources of the fonts in font family
                    {
                        if (fontSource.Type == SourceTypes.Embedded) // if font is embeded font
                        {
                            string locationKey = fontSource.Location.ToLower(); // one case good for comperason
                            if (!string.IsNullOrEmpty(_resourcePath) && locationKey.Contains(MacroMask.ToLower())) // in case we need to update resource path
                            {
                                locationKey = locationKey.Replace(MacroMask.ToLower(), _resourcePath);   
                            }
                            if (!_fonts.ContainsKey(locationKey)) // if key/location not present - add it
                            {
                                _fontFiles.Add(locationKey, new List<CSSFont>()); 
                            }
                            _fontFiles[locationKey].Add(cssFont); // save reference to the font object
                        }
                    }
                }
            }

            // now fill the list with pointers to the font families instead of names
            _elements.Clear();
            foreach (var element in settings.CssElements)
            {
                CSSStylableElement newElement = new CSSStylableElement();
                newElement.CopyFrom(element);
                if (!_elements.ContainsKey(element.Name)) // if key not present 
                {
                    _elements.Add(element.Name,new Dictionary<string, List<CSSFontFamily>>()); // add
                }
                _elements[element.Name].Add(element.Class,new List<CSSFontFamily>()); // reserve place for new list
                // now fill the list with pointers to the font families instead of names
                foreach (var assignedFontFamily in element.AssignedFontFamilies)
                {
                    string updatedFamilyName = CSSFontFamily.MakeDecoratedName(assignedFontFamily,decoration);
                    if (_fonts.ContainsKey(updatedFamilyName))
                    {
                        _elements[element.Name][element.Class].Add(_fonts[updatedFamilyName]);
                    }
                }
            }

            _storageSettings = settings;
            _loadedDecoration = decoration;
        }

        public void StoreTo(EPubFontSettings settings)
        {
            settings.FontFamilies.Clear();
            settings.CssElements.Clear();
            foreach (var cssFontFamily in _fonts.Keys)
            {
                CSSFontFamily newFamily = new CSSFontFamily();
                newFamily.CopyFrom(_fonts[cssFontFamily]);
                newFamily.DecorationId = _loadedDecoration;
                settings.FontFamilies.Add(newFamily);
            }

            foreach (var elementName in _elements.Keys)
            {
                foreach (var elementClass in _elements[elementName].Keys)
                {
                    CSSStylableElement item = new CSSStylableElement();
                    item.Name = elementName;
                    item.Class = elementClass;
                    foreach (var fontFamily in _elements[elementName][elementClass])
                    {
                        item.AssignedFontFamilies.Add(fontFamily.Name);
                    }
                    settings.CssElements.Add(item);
                }
            }
            _storageSettings = settings;
        }

        public void Reload()
        {
            Load(_storageSettings,_loadedDecoration);
        }

        /// <summary>
        /// Checks if font (family, by name) used in one of the CSS elements
        /// </summary>
        /// <param name="text">font family name</param>
        /// <returns>if font used by any CSS element</returns>
        public bool IsFontUsed(string text)
        {
            return _elements.AsParallel().Any(element => element.Value.AsParallel().Any(pair => pair.Value.AsParallel().Any(cssFontFamily => cssFontFamily.Name == text)));
        }

        public string GetMediaType(string embededFileLocation)
        {
            foreach (var cssFontFamily in _fontFiles)
            {
                foreach (var cssFont in cssFontFamily.Value)
                {
                    foreach (var fontSource in cssFont.Sources)
                    {
                        if (fontSource.EmbeddedLocation && fontSource.Location.ToLower() == embededFileLocation.ToLower())
                        {
                            return ConverMediaTypeToString(fontSource.Format);
                        }
                    }
                }
            }
            return "application/x-font-ttf";
        }

        private string ConverMediaTypeToString(FontFormat format)
        {
            switch (format)
            {
                case FontFormat.TrueType:
                    return "application/x-font-ttf";
                case FontFormat.OpenType:
                    return "application/vnd.ms-opentype";
                case FontFormat.EmbeddedOpenType:
                case FontFormat.SVGFont:
                case FontFormat.Unknown:
                case FontFormat.WOFF:
                default:
                    return "application/x-font-ttf";
            }
        }
    }
}
