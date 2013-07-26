using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fb2ePubGui
{
    public partial class LogWindow : Form
    {
        private readonly string _logFileName = string.Empty;
        public LogWindow(string fileName)
        {
            _logFileName = fileName;
            InitializeComponent();
        }

        private void LogWindow_Load(object sender, EventArgs e)
        {
            Text = string.Format("LogWindow: ({0})", _logFileName);
            try
            {
                using (Stream stream = File.Open(_logFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader fileStreamReader = new StreamReader(stream))
                    {
                        richTextBoxLogData.Text = fileStreamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.log.ErrorFormat("Error loading file {0}",_logFileName);
            }
        }
        
    }
}
