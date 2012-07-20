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
    public partial class AddCSSForm : Form
    {
        public string ElementName { get; set; }


        public string ElementClass { get; set; }



        public AddCSSForm()
        {
            InitializeComponent();
            FillComboBoxces();
            UpdateButtons();
        }

        private void FillComboBoxces()
        {
            FillNamesBox();
            FillClassesBox();
        }

        private void FillClassesBox()
        {
            comboBoxName.BeginUpdate();
            comboBoxName.Items.Clear();

            comboBoxName.Items.Add("a");
            comboBoxName.Items.Add("abbr");
            comboBoxName.Items.Add("acronym");
            comboBoxName.Items.Add("address");
            comboBoxName.Items.Add("applet");
            comboBoxName.Items.Add("area");
            comboBoxName.Items.Add("b");
            comboBoxName.Items.Add("base");
            comboBoxName.Items.Add("basefont");
            comboBoxName.Items.Add("bdo");
            comboBoxName.Items.Add("big");
            comboBoxName.Items.Add("blockquote");
            comboBoxName.Items.Add("body");
            comboBoxName.Items.Add("br");
            comboBoxName.Items.Add("button");
            comboBoxName.Items.Add("caption");
            comboBoxName.Items.Add("center");
            comboBoxName.Items.Add("cite");
            comboBoxName.Items.Add("code");
            comboBoxName.Items.Add("col");
            comboBoxName.Items.Add("colgroup");
            comboBoxName.Items.Add("dd");
            comboBoxName.Items.Add("del");
            comboBoxName.Items.Add("dfn");
            comboBoxName.Items.Add("dir");
            comboBoxName.Items.Add("div");
            comboBoxName.Items.Add("dl");
            comboBoxName.Items.Add("dt");
            comboBoxName.Items.Add("em");
            comboBoxName.Items.Add("fieldset");
            comboBoxName.Items.Add("font");
            comboBoxName.Items.Add("form");
            comboBoxName.Items.Add("frame");
            comboBoxName.Items.Add("frameset");
            comboBoxName.Items.Add("head");
            comboBoxName.Items.Add("h1");
            comboBoxName.Items.Add("h2");
            comboBoxName.Items.Add("h3");
            comboBoxName.Items.Add("h4");
            comboBoxName.Items.Add("h5");
            comboBoxName.Items.Add("h6");
            comboBoxName.Items.Add("hr");
            comboBoxName.Items.Add("html");
            comboBoxName.Items.Add("i");
            comboBoxName.Items.Add("iframe");
            comboBoxName.Items.Add("img");
            comboBoxName.Items.Add("input");
            comboBoxName.Items.Add("ins");
            comboBoxName.Items.Add("kbd");
            comboBoxName.Items.Add("label");
            comboBoxName.Items.Add("legend");
            comboBoxName.Items.Add("li");
            comboBoxName.Items.Add("link");
            comboBoxName.Items.Add("map");
            comboBoxName.Items.Add("menu");
            comboBoxName.Items.Add("meta");
            comboBoxName.Items.Add("noframes");
            comboBoxName.Items.Add("noscript");
            comboBoxName.Items.Add("object");
            comboBoxName.Items.Add("ol");
            comboBoxName.Items.Add("optgroup");
            comboBoxName.Items.Add("option");
            comboBoxName.Items.Add("p");
            comboBoxName.Items.Add("param");
            comboBoxName.Items.Add("pre");
            comboBoxName.Items.Add("q");
            comboBoxName.Items.Add("s");
            comboBoxName.Items.Add("samp");
            comboBoxName.Items.Add("script");
            comboBoxName.Items.Add("select");
            comboBoxName.Items.Add("small");
            comboBoxName.Items.Add("span");
            comboBoxName.Items.Add("strike");
            comboBoxName.Items.Add("strong");
            comboBoxName.Items.Add("style");
            comboBoxName.Items.Add("sub");
            comboBoxName.Items.Add("sup");
            comboBoxName.Items.Add("table");
            comboBoxName.Items.Add("tbody");
            comboBoxName.Items.Add("td");
            comboBoxName.Items.Add("textarea");
            comboBoxName.Items.Add("tfoot");
            comboBoxName.Items.Add("th");
            comboBoxName.Items.Add("thead");
            comboBoxName.Items.Add("title");
            comboBoxName.Items.Add("tr");
            comboBoxName.Items.Add("tt");
            comboBoxName.Items.Add("u");
            comboBoxName.Items.Add("ul");
            comboBoxName.Items.Add("var");

            comboBoxName.EndUpdate();
        }

        private void FillNamesBox()
        {
            comboBoxClass.BeginUpdate();
            comboBoxClass.Items.Clear();

            comboBoxClass.Items.Add("annotation");
            comboBoxClass.Items.Add("body_image");
            comboBoxClass.Items.Add("citation");
            comboBoxClass.Items.Add("citation_author");
            comboBoxClass.Items.Add("drop");
            comboBoxClass.Items.Add("empty-line");
            comboBoxClass.Items.Add("epigraph");
            comboBoxClass.Items.Add("epigraph_main");
            comboBoxClass.Items.Add("epigraph_author");
            comboBoxClass.Items.Add("ex_bad_link");
            comboBoxClass.Items.Add("fb2_info");
            comboBoxClass.Items.Add("int_image");
            comboBoxClass.Items.Add("normal_image");
            comboBoxClass.Items.Add("note_anchor");
            comboBoxClass.Items.Add("note_section");
            comboBoxClass.Items.Add("poem");
            comboBoxClass.Items.Add("poemdate");
            comboBoxClass.Items.Add("poem_author");
            comboBoxClass.Items.Add("section");
            comboBoxClass.Items.Add("section_image");
            comboBoxClass.Items.Add("stanza");
            comboBoxClass.Items.Add("subtitle");
            comboBoxClass.Items.Add("title");
            comboBoxClass.Items.Add("v");
            comboBoxClass.Items.Add("coverpage");
            comboBoxClass.Items.Add("coverimage");
            comboBoxClass.Items.Add("titlepage");
            comboBoxClass.Items.Add("about");
            comboBoxClass.Items.Add("epub");


            comboBoxClass.EndUpdate();
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ElementName = comboBoxName.Text;
            ElementClass = comboBoxClass.Text;

        }

        private void comboBoxName_TextChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void comboBoxClass_TextChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            buttonSave.Enabled = (!string.IsNullOrEmpty(comboBoxName.Text) || !string.IsNullOrEmpty(comboBoxClass.Text));
        }
    }
}
