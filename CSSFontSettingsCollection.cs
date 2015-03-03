using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FontSettingsContracts;

namespace FontsSettings
{

    /// <summary>
    /// Represent and manage fonts with their assignments
    /// </summary>
    public class CSSFontSettingsCollection 
    {
        private readonly Dictionary<string,ICSSFontFamily> _fonts = new Dictionary<string, ICSSFontFamily>();
        private readonly Dictionary<string, Dictionary<string, List<ICSSFontFamily>>> _elements = new Dictionary<string, Dictionary<string, List<ICSSFontFamily>>>();
        private readonly Dictionary<string,List<ICSSFont>> _fontFiles = new Dictionary<string, List<ICSSFont>>();

        private IEPubFontSettings _storageSettings;
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
        public Dictionary<string, Dictionary<string, List<ICSSFontFamily>>> CssElements { get { return _elements; } }

        /// <summary>
        /// Return dictionary containing all the fonts families
        /// </summary>
        public Dictionary<string, ICSSFontFamily> Fonts { get { return _fonts; } }

        /// <summary>
        /// Return number of embedded files in collection
        /// </summary>
        public int NumberOfEmbededFiles 
        {
            get { return _fontFiles.Keys.Count; }
        }

        /// <summary>
        /// return list of all embedded files
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
                    List<ICSSFont> temp = _fontFiles[fontFileLocation];
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
        /// Load settings into a workable helper class structure
        /// </summary>
        /// <param name="settings">settings to load</param>
        /// <param name="decoration">decoration to be added to family font names</param>
        public void Load(IEPubFontSettings settings, string decoration)
        {
            // Copy all font families 
            _fonts.Clear();
            _fontFiles.Clear();
            MakeDecorationValid(ref decoration);
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
                        if (fontSource.Type == SourceTypes.Embedded) // if font is embedded font
                        {
                            string locationKey = fontSource.Location.ToLower(); // one case good for comparison
                            if (!string.IsNullOrEmpty(_resourcePath) && locationKey.Contains(MacroMask.ToLower())) // in case we need to update resource path
                            {
                                locationKey = locationKey.Replace(MacroMask.ToLower(), _resourcePath);   
                            }
                            if (!_fonts.ContainsKey(locationKey)) // if key/location not present - add it
                            {
                                _fontFiles.Add(locationKey, new List<ICSSFont>()); 
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
                    _elements.Add(element.Name,new Dictionary<string, List<ICSSFontFamily>>()); // add
                }
                _elements[element.Name].Add(element.Class,new List<ICSSFontFamily>()); // reserve place for new list
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

        /// <summary>
        /// Some decorations are not valid, according to CSS standard
        /// here we filter them out
        /// http://mathiasbynens.be/notes/unquoted-font-family
        /// </summary>
        /// <param name="decoration"></param>
        private void MakeDecorationValid(ref string decoration)
        {
            if ( string.IsNullOrEmpty(decoration) )
            {
                return;
            }
            //In CSS, identifiers (including element names, classes, and IDs in selectors) can contain only the characters [a-zA-Z0-9] and ISO 10646 characters U+00A0 and higher, plus the hyphen (-) and the underscore (_).
            const string pattern1 = "[^-_a-zA-Z0-9\u00A0-\u10FFFF]";
            const string replacement = "a";
            Regex processor = new Regex(pattern1);
            decoration = processor.Replace(decoration, replacement);
            // [Identifiers] cannot start with a digit, two hyphens, or a hyphen followed by a digit. Identifiers can also contain escaped characters and any ISO 10646 character as a numeric code
            const string pattern2 = @"^(-?\d|--)";
           processor = new Regex(pattern2);
            if (processor.IsMatch(decoration))
            {
                decoration = string.Format("a{0}",decoration);
            }
        }

        public void StoreTo(IEPubFontSettings settings)
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
                    var item = new CSSStylableElement {Name = elementName, Class = elementClass};
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

        public FontFormat GetFontFormat(string embededFileLocation)
        {
            foreach (var fontSource in _fontFiles.SelectMany(cssFontFamily => cssFontFamily.Value.SelectMany(cssFont => 
                cssFont.Sources.Where(fontSource => 
                    fontSource.EmbeddedLocation && String.Equals(fontSource.Location, embededFileLocation, StringComparison.CurrentCultureIgnoreCase)))))
            {
                return fontSource.Format;
            }
            return FontFormat.OpenType;
        }
    }
}
