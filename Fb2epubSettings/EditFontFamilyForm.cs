using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConverterContracts.FontSettings;
using FontsSettings;


namespace Fb2epubSettings
{
    public partial class EditFontFamilyForm : Form
    {
        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        private string _familyFontName;
        private CSSFontSettingsCollection _fontSettings;

        private readonly BindingSource _myDataSourceFonts = new BindingSource();
        private readonly BindingSource _myDataSourceSources= new BindingSource();

        public EditFontFamilyForm(CSSFontSettingsCollection fontSettings, string familyFontName)
        {
            _familyFontName = familyFontName;
            _fontSettings = fontSettings;
            InitializeComponent();
            buttonCancel.CausesValidation = false;

            SetupBindings();

        }

        private void SetupBindings()
        {
            SetWidthBoxItems();
            SetStretchBoxItems();
            SetSourceTypesItems();
            SetSourceFormatItems();
            _myDataSourceFonts.CurrentChanged += MyDataSourceFontsCurrentChanged;
            _myDataSourceFonts.DataSource = (_fontSettings.Fonts[_familyFontName].Fonts);
            listBoxFontNames.DataSource = _myDataSourceFonts;
            listBoxFontNames.DisplayMember = "Name";


            _myDataSourceSources.CurrentChanged += _myDataSourceSources_CurrentChanged;
            _myDataSourceSources.DataSource = _myDataSourceFonts;
            _myDataSourceSources.DataMember = "Sources";
            listBoxSources.DataSource = _myDataSourceSources;
            listBoxSources.DisplayMember = "Name";

            textBoxLocation.DataBindings.Add("Text", _myDataSourceSources, "Location");

            buttonBrowseForLocation.DataBindings.Add("Enabled", _myDataSourceSources, "EmbeddedLocation");

            comboBoxSourceType.DataBindings.Add("Enabled", _myDataSourceFonts, "HasSources");
            comboBoxFormat.DataBindings.Add("Enabled", _myDataSourceFonts, "HasSources");
            textBoxLocation.DataBindings.Add("Enabled", _myDataSourceFonts, "HasSources");
            labelLocation.DataBindings.Add("Enabled", _myDataSourceFonts, "HasSources");
            labelType.DataBindings.Add("Enabled", _myDataSourceFonts, "HasSources");
            labelFormat.DataBindings.Add("Enabled", _myDataSourceFonts, "HasSources");
            buttonRemoveSource.DataBindings.Add("Enabled", _myDataSourceFonts, "HasSources");

            ShowCurrentFontDetails(_myDataSourceFonts.Current as CSSFont);
            ShowCurrentSourceDetails(_myDataSourceSources.Current as FontSource);

            textBoxFontFamilyName.Text = _familyFontName;
        }

        void _myDataSourceSources_CurrentChanged(object sender, EventArgs e)
        {
            FontSource currentSource = _myDataSourceSources.Current as FontSource;
            if (currentSource != null)
            {
                ShowCurrentSourceDetails(currentSource);
            }
            
        }

        private void ResetSettings()
        {
            textBoxFontFamilyName.Text = _familyFontName;
            _fontSettings.Reload();
        }

        private void SetSourceFormatItems()
        {
            comboBoxFormat.BeginUpdate();
            comboBoxFormat.Items.Clear();

            comboBoxFormat.Items.Add("woff");
            comboBoxFormat.Items.Add("NA");
            comboBoxFormat.Items.Add("truetype");
            comboBoxFormat.Items.Add("svg");
            comboBoxFormat.Items.Add("opentype");
            comboBoxFormat.Items.Add("embedded-opentype");

            comboBoxFormat.EndUpdate();
        }

        private void SetSourceTypesItems()
        {
            comboBoxSourceType.BeginUpdate();
            comboBoxSourceType.Items.Clear();

            comboBoxSourceType.Items.Add("Embedded");
            comboBoxSourceType.Items.Add("External");
            comboBoxSourceType.Items.Add("Local");

            comboBoxSourceType.EndUpdate();
        }

        private void ShowCurrentSourceDetails(FontSource fontSource)
        {
            if (fontSource != null)
            {
                SetSourceTypeToBox(fontSource.Type);
                SetSourceFormatToBox(fontSource.Format);                
            }
        }

        private void SetStretchBoxItems()
        {
            comboBoxStretch.BeginUpdate();
            comboBoxStretch.Items.Clear();

            comboBoxStretch.Items.Add("condensed");
            comboBoxStretch.Items.Add("expanded");
            comboBoxStretch.Items.Add("extra-condensed");
            comboBoxStretch.Items.Add("extra-expanded");
            comboBoxStretch.Items.Add("normal");
            comboBoxStretch.Items.Add("semi-condensed");
            comboBoxStretch.Items.Add("semi-expanded");
            comboBoxStretch.Items.Add("ultra-condensed");
            comboBoxStretch.Items.Add("ultra-expanded");

            comboBoxStretch.EndUpdate();
        }

        private void SetWidthBoxItems()
        {
            comboBoxWidth.BeginUpdate();
            comboBoxWidth.Items.Clear();

            comboBoxWidth.Items.Add("400 (normal)");
            comboBoxWidth.Items.Add("lighter");
            comboBoxWidth.Items.Add("bolder");
            comboBoxWidth.Items.Add("100");
            comboBoxWidth.Items.Add("200");
            comboBoxWidth.Items.Add("300");
            comboBoxWidth.Items.Add("500");
            comboBoxWidth.Items.Add("600");
            comboBoxWidth.Items.Add("800");
            comboBoxWidth.Items.Add("900");
            comboBoxWidth.Items.Add("700 (bold)");

            comboBoxWidth.EndUpdate();
        }

        void MyDataSourceFontsCurrentChanged(object sender, EventArgs e)
        {
            CSSFont currentFont = _myDataSourceFonts.Current as CSSFont;
            ShowCurrentFontDetails(currentFont);

            FontSource currentSource = _myDataSourceSources.Current as FontSource;
            if (currentSource != null)
            {
                ShowCurrentSourceDetails(currentSource);                
            }
        }

        private void ShowCurrentFontDetails(CSSFont currentFont)
        {
            if (currentFont != null)
            {
                SetStyleCheck(currentFont.FontStyle);
                SetVariantCheck(currentFont.FontVariant);
                SetWidthBox(currentFont.FontWidth);
                SetStretchBox(currentFont.FontStretch);
            }
            buttonDeleteFont.Enabled = (currentFont != null);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_familyFontName != textBoxFontFamilyName.Text)
            {
                var x = _fontSettings.Fonts[_familyFontName];
                x.Name = textBoxFontFamilyName.Text;
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
            CSSFont newFont = new CSSFont
                                  {
                                      FontStretch = FontStretch.Normal,
                                      FontStyle = FontStylesEnum.Normal,
                                      FontVariant = FontVaiantEnum.Normal,
                                      FontWidth = FontBoldnessEnum.Normal
                                  };
            _fontSettings.Fonts[_familyFontName].Fonts.Add(newFont);
            _myDataSourceFonts.ResetBindings(false);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            _fontSettings.Fonts[_familyFontName].Fonts.Remove(_myDataSourceFonts.Current as CSSFont);
            _myDataSourceFonts.ResetBindings(false);
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
            dialog.Filter = "All font files|*.ttf; *.otf; *.ttc|TTF files (*.ttf)|*.ttf|All files (*.*)|*.*";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                //textBoxLocation.Text = ReplaceData(dialog.FileName);   
                FontSource fs = _myDataSourceSources.Current as FontSource;
                if (fs != null)
                {
                    fs.Location = ReplaceData(dialog.FileName);   
                }
                _myDataSourceFonts.ResetCurrentItem();
            }
        }




        private string ReplaceData(string p)
        {
            return p;
        }

        private void textBoxLocation_TextChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = textBoxLocation_TextChanged;
                Invoke(d, new [] { sender, e });
                return;
            }

            if (sender == textBoxLocation)
            {
               _myDataSourceFonts.ResetCurrentItem();
            }
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
            FontSource currentSource = _myDataSourceSources.Current as FontSource;
            if (currentSource != null)
            {
                currentSource.Type = sourceType;
                _myDataSourceFonts.ResetCurrentItem();
            }
        }


        private void comboBoxFormat_SelectedValueChanged(object sender, EventArgs e)
        {
            FontFormat format = FontFormat.Unknown;
            switch (comboBoxFormat.SelectedItem.ToString())
            {
                case "NA":
                    format = FontFormat.Unknown;
                    break;
                case "woff":
                    format = FontFormat.WOFF;
                    break;
                case "truetype":
                    format = FontFormat.TrueType;
                    break;
                case "opentype":
                    format = FontFormat.OpenType;
                    break;
                case "embedded-opentype":
                    format = FontFormat.EmbeddedOpenType;
                    break;
                case "svg":
                    format = FontFormat.SVGFont;
                    break;
            }
            SetSourceFormatToFont(format);
        }

        private void SetSourceFormatToFont(FontFormat format)
        {
            FontSource currentSource = _myDataSourceSources.Current as FontSource;
            if (currentSource != null)
            {
                currentSource.Format = format;
                _myDataSourceFonts.ResetCurrentItem();
            }
        }

        private void buttonAddSource_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonAddSource_Click;
                Invoke(d, new[] { sender, e });
                return;
            }

            FontSource source = new FontSource {Format = FontFormat.Unknown, Location = "Undefined", Type = SourceTypes.Local};

            CSSFont currentFont = _myDataSourceFonts.Current as CSSFont;
            if (currentFont != null)
            {
                currentFont.Sources.Add(source);
                _myDataSourceFonts.ResetCurrentItem();
            }
        }

        private void buttonRemoveSource_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonRemoveSource_Click;
                Invoke(d, new[] { sender, e });
                return;
            }
            _myDataSourceSources.RemoveCurrent();
        }

        private void radioButtonStyleNormal_CheckedChanged(object sender, EventArgs e)
        {
            SetStyle(FontStylesEnum.Normal);
            
        }

        private void SetStyle(FontStylesEnum fontStyle)
        {
            CSSFont currentFont= _myDataSourceFonts.Current as CSSFont;
            if (currentFont != null)
            {
                currentFont.FontStyle = fontStyle;
                _myDataSourceFonts.ResetCurrentItem();
            }
        }

        private void radioButtonStyleItalic_CheckedChanged(object sender, EventArgs e)
        {
            SetStyle(FontStylesEnum.Italic);
        }

        private void radioButtonStyleOblique_CheckedChanged(object sender, EventArgs e)
        {
            SetStyle(FontStylesEnum.Oblique);
        }

        private void radioButtonVariantNormal_CheckedChanged(object sender, EventArgs e)
        {
            SetVariant(FontVaiantEnum.Normal);
        }

        private void SetVariant(FontVaiantEnum variant)
        {
            CSSFont currentFont= _myDataSourceFonts.Current as CSSFont;
            if (currentFont != null)
            {
                currentFont.FontVariant = variant;
                _myDataSourceFonts.ResetCurrentItem();
            }
        }

        private void radioButtonVariantSmallCaps_CheckedChanged(object sender, EventArgs e)
        {
            SetVariant(FontVaiantEnum.SmallCaps);
        }

        private void comboBoxWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            CSSFont currentFont = _myDataSourceFonts.Current as CSSFont;
            if (currentFont != null)
            {
                currentFont.FontWidth = GetSelectedWidth();
                _myDataSourceFonts.ResetCurrentItem();
            }
        }

        private FontBoldnessEnum GetSelectedWidth()
        {
            FontBoldnessEnum selected = FontBoldnessEnum.Normal;
            switch (comboBoxWidth.SelectedItem as string)
            {
                case "400 (normal)":
                    selected = FontBoldnessEnum.Normal;
                    break;
                case "lighter":
                    selected = FontBoldnessEnum.Lighter;
                    break;
                case "bolder":
                    selected = FontBoldnessEnum.Bolder;
                    break;
                case "100":
                    selected = FontBoldnessEnum.B100;
                    break;
                case "200":
                    selected = FontBoldnessEnum.B200;
                    break;
                case "300":
                    selected = FontBoldnessEnum.B300;
                    break;
                case "500":
                    selected = FontBoldnessEnum.B500;
                    break;
                case "600":
                    selected = FontBoldnessEnum.B600;
                    break;
                case "800":
                    selected = FontBoldnessEnum.B800;
                    break;
                case "900":
                    selected = FontBoldnessEnum.B900;
                    break;
                case "700 (bold)":
                    selected = FontBoldnessEnum.Bold;
                    break;
            }
            return selected;
        }

        private void comboBoxStretch_SelectedIndexChanged(object sender, EventArgs e)
        {
            CSSFont currentFont = _myDataSourceFonts.Current as CSSFont;
            if (currentFont != null)
            {
                currentFont.FontStretch = GetSelectedStretch();
                _myDataSourceFonts.ResetCurrentItem();
            }

        }


        private FontStretch GetSelectedStretch()
        {
            FontStretch selected = FontStretch.Normal;
            switch (comboBoxStretch.SelectedItem as string)
            {
                case "condensed":
                    selected = FontStretch.Condenced;
                    break;
                case "expanded":
                    selected = FontStretch.Expanded;
                    break;
                case "extra-condensed":
                    selected = FontStretch.ExtraCondenced;
                    break;
                case "extra-expanded":
                    selected = FontStretch.ExtraExpanded;
                    break;
                case "normal":
                    selected = FontStretch.Normal;
                    break;
                case "semi-condensed":
                    selected = FontStretch.SemiCondenced;
                    break;
                case "semi-expanded":
                    selected = FontStretch.SemiExpanded;
                    break;
                case "ultra-condensed":
                    selected = FontStretch.UltraCondenced;
                    break;
                case "ultra-expanded":
                    selected = FontStretch.UltraExpanded;
                    break;
            }
            return selected;
        }
    }
}
