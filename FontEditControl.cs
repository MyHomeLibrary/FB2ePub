using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FontsSettings
{
    public partial class FontEditControl : UserControl
    {
        public FontEditControl()
        {
            InitializeComponent();
        }

        public CSSFontSettingsCollection CSSFontSettings { get; set; }

        public void RefreshData()
        {
            UpdateFontsList();
            UpdateFontsButtons();
        }


        private void ButtonRemoveFontClick(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selected = listViewFonts.SelectedItems;
            List<ListViewItem> used = GetUsed(selected);
            if (used.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat(Resources.FontSettings.ConverterSettingsForm_buttonRemoveFont_FontNameMessage, used[0].Text);
                MessageBox.Show(this, sb.ToString(), Resources.FontSettings.ConverterSettingsForm_buttonRemoveFont_Click_Font_used, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            List<ListViewItem> toDelete = selected.Cast<ListViewItem>().Where(item => (item != null) && !used.Contains(item)).ToList();
            listViewFonts.BeginUpdate();
            foreach (ListViewItem selectedItem in toDelete)
            {
                CSSFontSettings.Fonts.Remove(selectedItem.Text);
                listViewFonts.Items.Remove(selectedItem);
            }
            listViewFonts.EndUpdate();
            RefreshData();
        }

        private void ButtonEditFontClick(object sender, EventArgs e)
        {
            EditFontFamily(listViewFonts.SelectedItems[0].Text);
        }

        private void ButtonAddFontClick(object sender, EventArgs e)
        {
            CSSFontFamily newFamily = new CSSFontFamily();
            CSSFontSettings.Fonts.Add(newFamily.Name, newFamily);
            listViewFonts.Items.Add(newFamily.Name);
            RefreshData();
            EditFontFamily(newFamily.Name);
        }

        private List<ListViewItem> GetUsed(ListView.SelectedListViewItemCollection selected)
        {
            return selected.Cast<ListViewItem>().Where(item => CSSFontSettings.IsFontUsed(item.Text)).ToList();
        }

        private void UpdateFontsList()
        {
            listViewFonts.Items.Clear();
            foreach (var cssFontFamily in CSSFontSettings.Fonts.Keys)
            {
                listViewFonts.Items.Add(cssFontFamily);
            }
        }

        private void UpdateFontsButtons()
        {
            bool itemSelected = listViewFonts.SelectedItems.Count > 0;
            buttonEditFont.Enabled = itemSelected;
            buttonRemoveFont.Enabled = itemSelected;
        }

        private void EditFontFamily(string familyFontName)
        {
            var editForm = new EditFontFamilyForm(CSSFontSettings, familyFontName);
            editForm.ShowDialog();
            RefreshData();
        }

        private void ListViewFontsSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFontsButtons();
        }



    }
}
