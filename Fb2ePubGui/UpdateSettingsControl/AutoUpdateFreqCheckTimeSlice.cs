using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Fb2ePubGui.Properties;

namespace Fb2ePubGui.UpdateSettingsControl
{
    /// <summary>
    /// Used to define how often to check for automatic update
    /// </summary>
    [TypeConverter(typeof(AutoUpdateFreqCheckTypeConverter))]
    public enum AutoUpdateFreqCheckTimeSlice
    {
        EveryRun, // check each time program loads
        OnceADay, // every day (date change)
        OnceAWeek, // every week
        OnceAMonths, // every months
    };

    /// <summary>
    /// Convert to/from string, used to display enum in combo box and localized
    /// </summary>
    internal class AutoUpdateFreqCheckTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Check if we can convert to speciffic type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Used to convert from string to AutoUpdateFreqCheckTimeSlice
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string stringValue = value as string;
                switch (stringValue)
                {
                    case "EveryRun":
                        return AutoUpdateFreqCheckTimeSlice.EveryRun;
                    case "OnceADay":
                        return AutoUpdateFreqCheckTimeSlice.OnceADay;
                    case "OnceAWeek":
                        return AutoUpdateFreqCheckTimeSlice.OnceAWeek;
                    case "OnceAMonths":
                        return AutoUpdateFreqCheckTimeSlice.OnceAMonths;
                    default:
                        if (stringValue ==
                            AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_EveryRun_Text)
                        {
                            return AutoUpdateFreqCheckTimeSlice.EveryRun;
                        }
                        else if (stringValue == 
                            AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_OnceADay_Text)
                        {
                            return AutoUpdateFreqCheckTimeSlice.OnceADay;
                        }
                        else if (stringValue ==
                            AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_OnceAWeek_Text)
                        {
                            return AutoUpdateFreqCheckTimeSlice.OnceAWeek;
                        }
                        else if (stringValue ==
                            AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_OnceAMonth_Text)
                        {
                            return AutoUpdateFreqCheckTimeSlice.OnceAMonths;
                        }
                        break;
                }
                throw new ArgumentException(string.Format("The value '{0}' is not one of the AutoUpdateFreqCheckTimeSlice enumeration values", value as string));
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Used to convert from AutoUpdateFreqCheckTimeSlice to string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is AutoUpdateFreqCheckTimeSlice)
            {
                AutoUpdateFreqCheckTimeSlice enumValue = (AutoUpdateFreqCheckTimeSlice)value;
                switch (enumValue)
                {
                    case AutoUpdateFreqCheckTimeSlice.EveryRun:
                        return AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_EveryRun_Text;
                    case AutoUpdateFreqCheckTimeSlice.OnceADay:
                        return AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_OnceADay_Text;
                    case AutoUpdateFreqCheckTimeSlice.OnceAWeek:
                        return AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_OnceAWeek_Text;
                    case AutoUpdateFreqCheckTimeSlice.OnceAMonths:
                        return AutoUpdateFreqCheckTimeSliceTranslations.AutoUpdateFreqCheckTimeSlice_OnceAMonth_Text;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
