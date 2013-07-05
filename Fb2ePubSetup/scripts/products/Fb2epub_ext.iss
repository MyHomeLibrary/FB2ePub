//extensions
// contains functionality used to register Fb2Epub  windows shell extensions
//

#define CLSID_Fb2EpubShlExt "{{5FFF3DF0-E213-4CA6-A37E-0C3BA1FE35FC}"
#define Fb2EpubShlExtName   "Fb2EpubShlExt"
#define FB2_Extension_Path "{code:GetAssociationByExtension|.fb2}"
#define ZIP_Extension_Path "{code:GetAssociationByExtension|.zip}"
#define RAR_Extension_Path "{code:GetAssociationByExtension|.rar}"
#define Any_Extension_Path "{code:GetAssociationByExtension|.*}"


[CustomMessages]
en.assosiateFB2 = Associate context menu with "fb2" extension
ru.assosiateFB2 = Привязать правую кнопку манипулятора к расширению файла "fb2" 

en.assosiateZIP = Associate context menu with "zip" extension
ru.assosiateZIP = Привязать правую кнопку манипулятора к расширению файла "zip"

en.assosiateRAR = Associate context menu with "rar" extension
ru.assosiateRAR = Привязать правую кнопку манипулятора к расширению файла "rar"

en.assosiateAny = Associate context menu with any file extension
ru.assosiateAny = Привязать правую кнопку манипулятора к любому расширению файла

en.fileExtGroup = File extensions
ru.fileExtGroup = Расширения файлов

en.ConfigSelection = Embed cyrillic fonts
ru.ConfigSelection = Встраивать русские шрифты

en.EmbedFonts = Embed cyrillic fonts (for devices without russian lang. support)
ru.EmbedFonts = Встраивать рус. шрифты (для ридеров без поддержки русского)

en.NoEmbedFonts = No fonts embedding (for devices with russian lang. support)
ru.NoEmbedFonts = Не встраивать шрифты (для ридеров отображающих русский текст)

[Code]
// returns the assosiation for the extension if present 
// or extension iteslf to know where to register 
function GetAssociationByExtension(extension : string) : string;
var 
 readValue : string;
begin
Result := extension;
if ( RegQueryStringValue(HKCR,extension,'',readValue)) then
begin
Result := readValue;
end;
end;
