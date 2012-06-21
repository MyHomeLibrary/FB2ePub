using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FontsSettings;

namespace Fb2epubSettings
{
    public partial class EditFontFamilyForm : Form
    {


        private string _familyFontName;
        private CSSFontSettingsCollection _fontSettings;

        public EditFontFamilyForm(CSSFontSettingsCollection fontSettings, string familyFontName)
        {
            _familyFontName = familyFontName;
            _fontSettings = fontSettings;
            InitializeComponent();
            buttonCancel.CausesValidation = false;
            ResetSettings();
        }

        private void ResetSettings()
        {
            textBoxFontFamilyName.Text = _familyFontName;
            listBoxFonts.Items.Clear();
            foreach (var font in _fontSettings.Fonts[_familyFontName].Fonts)
            {
                FontListItem fontItem = new FontListItem { EpubFont = font };
                listBoxFonts.Items.Add(fontItem);
            }
            listBoxFonts.SelectedIndex = 0;
            DisplayFontSelected();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_familyFontName != textBoxFontFamilyName.Text)
            {
               var x =  _fontSettings.Fonts[_familyFontName];
                _fontSettings.Fonts.Remove(_familyFontName);
                _fontSettings.Fonts[textBoxFontFamilyName.Text] = x;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxFontFamilyName_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (sender == textBoxFontFamilyName)
            {
                if(string.IsNullOrEmpty(textBoxFontFamilyName.Text))
                {
                    error = Resources.Fb2epubSettings.
                            EditFontFamilyForm_textBoxFontFamilyName_Validating_Font_family_name_can_t_be_empty;
                    e.Cancel = true;
                }
            }
            errorProvider1.SetError((Control)sender, error);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetSettings();
        }

        private void buttonAddFont_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void listBoxFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayFontSelected();
        }

        private void DisplayFontSelected()
        {
            FontListItem current = listBoxFonts.Items[listBoxFonts.SelectedIndex] as FontListItem;
            if (current != null)
            {
                CSSFont currentFont = current.EpubFont;
                SetStyleCheck(currentFont.FontStyle);
                SetVariantCheck(currentFont.FontVariant);
                SetWidthBox(currentFont.FontWidth);
                SetStretchBox(currentFont.FontStretch);
                SetSources(currentFont.Sources);
            }
        }

        private void SetSources(List<FontSource> sources)
        {
            listBoxSources.Items.Clear();
            foreach (var fontSource in sources)
            {
                SourceListItem listItem = new SourceListItem {Source = fontSource};
                listBoxSources.Items.Add(listItem);
            }
            listBoxSources.SelectedIndex = 0;
            DisplaySourceSelected();
        }

        private void DisplaySourceSelected()
        {
            SourceListItem selectedSource = listBoxSources.SelectedItem as SourceListItem;
            if (selectedSource!= null)
            {
                textBoxLocation.Text = selectedSource.Name;
                SetSourceTypeToBox(selectedSource.Source.Type);
                SetSourceFormatToBox(selectedSource.Source.Format);
            }
        }

        private void SetSourceFormatToBox(FontFormat fontFormat)
        {
            switch (fontFormat)
            {
                case FontFormat.WOFF:
                    comboBoxFormat.SelectedItem = "woff";
                    break;
                case FontFormat.Unknown:
                    comboBoxFormat.SelectedItem = "NA";
                    break;
                case FontFormat.TrueType:
                    comboBoxFormat.SelectedItem = "truetype";
                    break;
                case FontFormat.SVGFont:
                    comboBoxFormat.SelectedItem = "svg";
                    break;
                case FontFormat.OpenType:
                    comboBoxFormat.SelectedItem = "opentype";
                    break;
                case FontFormat.EmbeddedOpenType:
                    comboBoxFormat.SelectedItem = "embedded-opentype";
                    break;
            }
        }


        private void SetSourceTypeToBox(SourceTypes sourceType)
        {

            switch (sourceType)
            {
                case SourceTypes.Embedded:
                    comboBoxSourceType.SelectedItem = "Embedded";
                    break;
                case SourceTypes.External:
                    comboBoxSourceType.SelectedItem = "External";
                    break;
                case SourceTypes.Local:
                    comboBoxSourceType.SelectedItem = "Local";
                    break;
            } 
            UpdateSourceGUIOnSelectionChange();
        }

        private void UpdateSourceGUIOnSelectionChange()
        {
            SourceListItem selectedSource = listBoxSources.SelectedItem as SourceListItem;
            if (selectedSource != null)
            {
                buttonBrowseForLocation.Enabled = (selectedSource.Source.Type == SourceTypes.Embedded);
            }
        }


        private void SetStretchBox(FontStretch fontStretch)
        {
            switch (fontStretch)
            {
                case FontStretch.Condenced:
                    comboBoxStretch.SelectedItem = "condensed";
                    break;
                case FontStretch.Expanded:
                    comboBoxStretch.SelectedItem = "expanded";
                    break;
                case FontStretch.ExtraCondenced:
                    comboBoxStretch.SelectedItem = "extra-condensed";
                    break;
                case FontStretch.ExtraExpanded:
                    comboBoxStretch.SelectedItem = "extra-expanded";
                    break;
                case FontStretch.Normal:
                    comboBoxStretch.SelectedItem = "normal";
                    break;
                case FontStretch.SemiCondenced:
                    comboBoxStretch.SelectedItem = "semi-condensed";
                    break;
                case FontStretch.SemiExpanded:
                    comboBoxStretch.SelectedItem = "semi-expanded";
                    break;
                case FontStretch.UltraCondenced:
                    comboBoxStretch.SelectedItem = "ultra-condensed";
                    break;
                case FontStretch.UltraExpanded:
                    comboBoxStretch.SelectedItem = "ultra-expanded";
                    break;
            }
        }

        private void SetWidthBox(FontBoldnessEnum fontBoldnessEnum)
        {
            switch (fontBoldnessEnum)
            {
                case FontBoldnessEnum.Normal:
                    comboBoxWidth.SelectedItem = "400 (normal)";
                    break;
                case FontBoldnessEnum.Lighter:
                    comboBoxWidth.SelectedItem = "lighter";
                    break;
                case FontBoldnessEnum.Bolder:
                    comboBoxWidth.SelectedItem = "bolder";
                    break;
                case FontBoldnessEnum.B100:
                    comboBoxWidth.SelectedItem = "100";
                    break;
                case FontBoldnessEnum.B200:
                    comboBoxWidth.SelectedItem = "200";
                    break;
                case FontBoldnessEnum.B300:
                    comboBoxWidth.SelectedItem = "300";
                    break;
                case FontBoldnessEnum.B500:
                    comboBoxWidth.SelectedItem = "500";
                    break;
                case FontBoldnessEnum.B600:
                    comboBoxWidth.SelectedItem = "600";
                    break;
                case FontBoldnessEnum.B800:
                    comboBoxWidth.SelectedItem = "800";
                    break;
                case FontBoldnessEnum.B900:
                    comboBoxWidth.SelectedItem = "900";
                    break;
                case FontBoldnessEnum.Bold:
                    comboBoxWidth.SelectedItem = "700 (bold)";
                    break;

            }
        }

        private void SetVariantCheck(FontVaiantEnum fontVariant)
        {
            switch (fontVariant)
            {
                case FontVaiantEnum.Normal:
                    radioButtonVariantNormal.Checked = true;
                    break;
                case FontVaiantEnum.SmallCaps:
                    radioButtonVariantSmallCaps.Checked = true;
                    break;
            }
        }

        private void SetStyleCheck(FontStylesEnum fontStyle)
        {
            switch (fontStyle)
            {
                case FontStylesEnum.Normal:
                    radioButtonStyleNormal.Checked = true;
                    break;
                case FontStylesEnum.Italic:
                    radioButtonStyleItalic.Checked = true;
                    break;
                case FontStylesEnum.Oblique:
                    radioButtonStyleOblique.Checked = true;
                    break;
            }
        }

        private void buttonBrowseForLocation_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog{AutoUpgradeEnabled = true,CheckFileExists = true,CheckPathExists = true};
            dialog.Filter = "TTF files|*.ttf|All files|*.*";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxLocation.Text = ReplaceData(dialog.FileName);
                SelectedLocationChanged(textBoxLocation.Text);
            }
        }

        private void SelectedLocationChanged(string newLocation)
        {
            SourceListItem selectedSource = listBoxSources.SelectedItem as SourceListItem;
            int index = listBoxSources.SelectedIndex;
            if (selectedSource!= null)
            {
                listBoxSources.Items.Remove(selectedSource);
                selectedSource.Source.Location = newLocation;        
                listBoxSources.Items.Insert(index,selectedSource);
                listBoxSources.SelectedItem = selectedSource;
            }
        }

        private string ReplaceData(string p)
        {
            // TODO: replace the path with CSSFontSettingsCollection.MacroMask if present in path
            return p;
        }

        private void textBoxLocation_TextChanged(object sender, EventArgs e)
        {
            SelectedLocationChanged(textBoxLocation.Text);
        }

        private void comboBoxSourceType_SelectedValueChanged(object sender, EventArgs e)
        {
            SourceTypes sourceType = SourceTypes.Embedded;
            switch (comboBoxSourceType.SelectedItem.ToString())
            {
                case "Embedded":
                    sourceType = SourceTypes.Embedded;
                    break;
                case "External":
                    sourceType = SourceTypes.External;
                    break;
                case "Local":
                    sourceType = SourceTypes.Local;
                    break;
            }
            SetSourceTypeToFont(sourceType);
        }

        private void SetSourceTypeToFont(SourceTypes sourceType)
        {
            SourceListItem selectedSource = listBoxSources.SelectedItem as SourceListItem;
            if (selectedSource != null)
            {
                selectedSource.Source.Type = sourceType;
            }
            UpdateSourceGUIOnSelectionChange();
        }
    }
}
