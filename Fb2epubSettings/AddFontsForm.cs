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
    public partial class AddFontsForm : Form
    {
        private Dictionary<string, CSSFontFamily> _fonts = null;
        private string _selectedFont = null;


        public string SelectedFont
        {
            get { return _selectedFont; } 
        }

        public AddFontsForm(Dictionary<string, CSSFontFamily> fonts)
        {
            _fonts = fonts;
            InitializeComponent();
            foreach (var cssFontFamily in _fonts.Keys)
            {
                listBoxFonts.Items.Add(cssFontFamily);
            }
            UpdateButtons();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _selectedFont = listBoxFonts.SelectedItem as string;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void listBoxFonts_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            buttonOK.Enabled = (listBoxFonts.SelectedItem != null);
        }
    }
}
