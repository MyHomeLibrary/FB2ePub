using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EPubLibrary.CSS_Items
{
    public class CSSFile //: List<BaseCSSItem>
    {
        private readonly List<FontDefinition> fonts = new List<FontDefinition>();
        private string ePubFilePath;

        private readonly List<BaseCSSItem> targets =  new List<BaseCSSItem>();

        /// <summary>
        /// Get/Set file name of the CSS file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Get/Set ID of the current element to be used in manifest etc
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Get default CSS media type
        /// </summary>
        public static string MediaType { get { return @"text/css"; } }

        /// <summary>
        /// Get/Set full path to the file location on disk
        /// </summary>
        public string FileExtPath { get; set; }

        /// <summary>
        /// Get/Set relative location of the file inside ePub
        /// </summary>
        public string EPubFilePath
        {
            get
            {
                return ePubFilePath;
            }

            set
            {
                ePubFilePath = value.Replace('\\','/');
            }
        }

        /// <summary>
        /// Loads items from another CSS file
        /// </summary>
        /// <param name="fileName">file to load</param>
        /// <param name="add">selects if new data should be added to current "in memory" data or replace it</param>
        public void Load(string fileName, bool add)
        {
            if (!add)
            {
                targets.Clear();
                fonts.Clear();
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
                            catch (Exception)
                            {
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
        public void AddFont(FontDefinition font)
        {
            if (!fonts.Contains(font))
            {
                fonts.Add(font);                
            }
        }

        /// <summary>
        /// Add CSS "target" section ( like p , body , etc) to CSS file
        /// </summary>
        /// <param name="item"></param>
        public void AddTarget(BaseCSSItem item)
        {
            // If it's a new item just add it
            int sameItemPos = targets.FindIndex(x => x.Name.ToLower() == item.Name.ToLower());
            if ( sameItemPos == -1)
            {
                targets.Add(item);
            }
            else
            {
                // if similar item already exists we copy parameters
                foreach (var parameter in item.Parameters)
                {
                    // but copy only in case that it's new parameters, ignore same 
                    if (!targets[sameItemPos].Parameters.ContainsKey(parameter.Key))
                    {
                        targets[sameItemPos].Parameters.Add(parameter.Key,parameter.Value);
                    }
                    else if (targets[sameItemPos].Parameters[parameter.Key].ToString().ToLower() != parameter.Value.ToString().ToLower())
                    {
                        string old = (string)targets[sameItemPos].Parameters[parameter.Key];
                        targets[sameItemPos].Parameters[parameter.Key] = string.Format("{0}, {1}", old, parameter.Value);
                        
                    //    Logger.log.ErrorFormat("CSS values conflict for parameter \"{0}\" ",parameter.Key);
                    }
                }
            }
        }

        public bool Empty() 
        {
            return ((fonts.Count == 0) && (targets.Count == 0));
        }

        /// <summary>
        /// Writes CSS file to stream
        /// </summary>
        /// <param name="stream"></param>
        public void Write(Stream stream)
        {
            foreach (var item in fonts)
            {
                item.Write(stream);
            }

            foreach (var item in targets)
            {
                item.Write(stream);
            }
        }

    }
}