using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fb2epubSettings
{
    public partial class ConverterSettingsForm : Form
    {
        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        public ConverterSettingsForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonSave_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }
            Fb2Epub.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonReset_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }
            Fb2Epub.Default.Reload();
            ConverterSettingsForm_Load(this,null);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonCancel_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConverterSettingsForm_Load(object sender, EventArgs e)
        {
            checkBoxTransliterateTOC.Checked = Fb2Epub.Default.TransliterateTOC;
            checkBoxTransliterateFileName.Checked = Fb2Epub.Default.TransliterateFileName;
            checkBoxTransliterateAdditional.Checked = Fb2Epub.Default.Transliterate;
        }

        private void checkBoxTransliterateTOC_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.TransliterateTOC = checkBoxTransliterateTOC.Checked;
        }

        private void checkBoxTransliterateFileName_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.TransliterateFileName = checkBoxTransliterateFileName.Checked;
        }

        private void checkBoxTransliterateAdditional_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.Transliterate = checkBoxTransliterateAdditional.Checked;
        }
    }
}
