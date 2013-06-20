using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fb2epubSettings.AppleSettings
{
    /// <summary>
    /// This class represent and serialize all the apple ePub oriented additional settings in their versions
    /// currently it contain settings only for ePub v2 , but in future we plan to add v3 support and settings for it will be here too
    /// </summary>
    [Serializable]
    public class AppleConverterSettings
    {
        private readonly AppleConverterePub2Settings _v2Settings = new AppleConverterePub2Settings();

        /// <summary>
        /// Get/Set apple settings foe ePub v2 
        /// </summary>
        public AppleConverterePub2Settings V2Settings
        {
            get { return _v2Settings; }
            set { _v2Settings.CopyFrom(value); }
        }


        /// <summary>
        /// Copies content from another setting object
        /// </summary>
        /// <param name="appleConverterSettings"></param>
        public void CopyFrom(AppleConverterSettings appleConverterSettings)
        {
            if (appleConverterSettings == null)
            {
                throw new ArgumentNullException("appleConverterSettings");
            }
            if (appleConverterSettings == this)
            {
                return;
            }

            _v2Settings.CopyFrom(appleConverterSettings._v2Settings);
        }


        /// <summary>
        /// Resets to the default settings
        /// </summary>
        public void SetupDefaults()
        {
            _v2Settings.SetupDefaults();
        }
    }
}
