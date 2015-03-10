using System;
using System.ComponentModel;

namespace EPubLibrary.Content
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false,AllowMultiple = false)]
    public class EPubCoreMediaTypeAttribute : Attribute
    {
        public string XmlEntryName { get; set; }
    }

    [TypeConverter(typeof(EPubCoreMediaTypesConverter<EPubCoreMediaType>))]
    public enum EPubCoreMediaType
    {
        [EPubCoreMediaType(XmlEntryName = @"image/gif")]
        ImageGif,                   // GIF Images

        [EPubCoreMediaType(XmlEntryName = @"image/jpeg")]
        ImageJpeg,                  // JPEG Images

        [EPubCoreMediaType(XmlEntryName = @"image/png")]
        ImagePng,                   // PNG Images

        [EPubCoreMediaType(XmlEntryName = @"image/svg+xml")]
        ImageSvgXml,                // SVG documents

        [EPubCoreMediaType(XmlEntryName = @"application/xhtml+xml")]
        ApplicationXhtmlXml,        // XHTML Content Documents and the EPUB Navigation Document. 

        [EPubCoreMediaType(XmlEntryName = @"application/x-dtbncx+xml")]
        ApplicationNCX,             // The superseded NCX 

        [EPubCoreMediaType(XmlEntryName = @"application/vnd.ms-opentype")]
        ApplicationFontMSOpen,      // OpenType fonts

        [EPubCoreMediaType(XmlEntryName = @"application/font-woff")]
        ApplicationFontWoff,        // WOFF fonts

        [EPubCoreMediaType(XmlEntryName = @"application/smil+xml")]
        ApplicationSimlXml,         // EPUB Media Overlay documents

        [EPubCoreMediaType(XmlEntryName = @"application/pls+xml")]
        ApplicationPlsXml,          //  Text-to-Speech (TTS) Pronunciation lexicons

        [EPubCoreMediaType(XmlEntryName = @"audio/mpeg")]
        AudioMpeg,                  // MP3 audio

        [EPubCoreMediaType(XmlEntryName = @"audio/mp4")]
        AudioMp4,                   // AAC LC audio using MP4 container

        [EPubCoreMediaType(XmlEntryName = @"text/css")]
        TextCss,                    // EPUB Style Sheets. 

        [EPubCoreMediaType(XmlEntryName = @"text/javascript")]
        TextJavascript,             // Scripts


        /// additional non-Epub standard media types
        [EPubCoreMediaType(XmlEntryName = @"application/adobe-page-template+xml")]
        AdditionalAddobeTemplateXml,
    }

    public static class EPubCoreMediaTypesConversions
    {
        public static string GetAsSerializableString(this EPubCoreMediaType mediaType)
        {
            return (string)TypeDescriptor.GetConverter(typeof (EPubCoreMediaType)).ConvertTo(mediaType,typeof(string));
        }
    }
}
