using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using FB2EPubConverter.Interfaces;
using Fb2ePubGui.Properties;

namespace Fb2ePubGui
{
    class ProgressUpdater : IProgressUpdateInterface
    {
        private readonly FormGUI _mainForm;


        public ProgressUpdater(FormGUI mainForm)
        {
            _mainForm = mainForm;
        }


        public void ConvertStarted(int total)
        {
            _mainForm.SetStatusText(string.Format(Resources.Started_To_ConvertFiles,total));
            _mainForm.SetProgressStart(total);

        }

        public void ConvertFinished(int total)
        {
            _mainForm.SetStatusText(string.Format(Resources.Finished_To_Convert_Files, total));
            _mainForm.SetProgressFinished();
        }

        public void ProcessingStarted(string fileName)
        {
            _mainForm.SetStatusText(string.Format(Resources.Converting_File, fileName));
        }

        public void ProcessingSaving(string fileName)
        {
            _mainForm.SetStatusText(string.Format(Resources.Saving_File, fileName));
        }

        public void Processed(string fileName)
        {
            _mainForm.SetStatusText(string.Format(Resources.File_Processed, fileName));
            _mainForm.SetFileProcessed();
        }

        public void SkippedDueError(string fileName)
        {
            _mainForm.SetStatusText(string.Format(Resources.File_Skipped_toError, fileName));
            _mainForm.SetFileProcessed();
        }

    }
}
