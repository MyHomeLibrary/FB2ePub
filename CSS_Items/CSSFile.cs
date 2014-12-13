using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EPubLibrary.Content;
using EPubLibrary.PathUtils;

namespace EPubLibrary.CSS_Items
{
    public class CSSFile : StyleElement
    {
        private readonly List<CssFontDefinition> _fonts = new List<CssFontDefinition>();

        private readonly List<BaseCSSItem> _targets =  new List<BaseCSSItem>();

        /// <summary>
        /// path to the file inside ePub
        /// </summary>
        private readonly EPubInternalPath _pathInEPub = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder +"/css");

        /// <summary>
        /// Name of the file in ePub
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Get/Set ID of the current element to be used in manifest etc
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Get default CSS media type
        /// </summary>
        public static EPubCoreMediaType MediaType { get { return EPubCoreMediaType.TextCss; } }

        public override EPubInternalPath PathInEPUB
        {
            get
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    throw new NullReferenceException("FileName property has to be set");
                }
                return new EPubInternalPath(_pathInEPub,FileName);
            }
        }

        /// <summary>
        /// Get/Set full path to the file location on disk
        /// </summary>
        public string FilePathOnDisk { get; set; }


        /// <summary>
        /// Loads items from another CSS file
        /// </summary>
        /// <param name="fileName">file to load</param>
        /// <param name="add">selects if new data should be added to current "in memory" data or replace it</param>
        public void Load(string fileName, bool add)
        {
            if (!add)
            {
                _targets.Clear();
                _fonts.Clear();
            }
            using (var textReader = File.OpenText(fileName))
            {
                string line;
                StringBuilder elementString = new StringBuilder();
                while ((line = textReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        line = line.Trim();
                        int endPosition = line.IndexOf('}');
                        int startPosition = 0;
                        while (endPosition != -1)
                        {
                            elementString.Append(line.Substring(startPosition, endPosition - startPosition+1));
                            BaseCSSItem cssItem = new BaseCSSItem();
                            try
                            {
                                cssItem.Parse(elementString.ToString());
                                AddTarget(cssItem);
                            }
                            catch (Exception ex)
                            {
                                Logger.Log.Error(ex);
                            }
                            elementString.Remove(0, elementString.Length);
                            startPosition = endPosition+1;
                            endPosition = line.IndexOf('}', startPosition);
                        }
                        elementString.Append(line.Substring(startPosition,line.Length - startPosition));
                    }
                }
            }
        }

        /// <summary>
        /// Add font object definition to CSS file
        /// </summary>
        /// <param name="font"></param>
        public void AddFont(CssFontDefinition font)
        {
            if (!_fonts.Contains(font))
            {
                _fonts.Add(font);                
            }
        }

        /// <summary>
        /// Add CSS "target" section ( like p , body , etc) to CSS file
        /// </summary>
        /// <param name="item"></param>
        public void AddTarget(BaseCSSItem item)
        {
            // If it's a new item just add it
            int sameItemPos = _targets.FindIndex(x => x.Name.ToLower() == item.Name.ToLower());
            if ( sameItemPos == -1)
            {
                _targets.Add(item);
            }
            else
            {
                // if similar item already exists we copy parameters
                foreach (var parameter in item.Parameters)
                {
                    // but copy only in case that it's new parameters, ignore same 
                    if (!_targets[sameItemPos].Parameters.ContainsKey(parameter.Key))
                    {
                        _targets[sameItemPos].Parameters.Add(parameter.Key,parameter.Value);
                    }
                    else if (_targets[sameItemPos].Parameters[parameter.Key].ToString().ToLower() != parameter.Value.ToString().ToLower())
                    {
                        string old = (string)_targets[sameItemPos].Parameters[parameter.Key];
                        _targets[sameItemPos].Parameters[parameter.Key] = string.Format("{0}, {1}", old, parameter.Value);
                        
                    //    Logger.log.ErrorFormat("CSS values conflict for parameter \"{0}\" ",parameter.Key);
                    }
                }
            }
        }

        public bool Empty() 
        {
            return ((_fonts.Count == 0) && (_targets.Count == 0));
        }

        /// <summary>
        /// Writes CSS file to stream
        /// </summary>
        /// <param name="stream"></param>
        public override void Write(Stream stream)
        {
            foreach (var item in _fonts)
            {
                item.Write(stream);
            }

            foreach (var item in _targets)
            {
                item.Write(stream);
            }
        }


        public override EPubCoreMediaType GetMediaType()
        {
            return MediaType;
        }

    }
}