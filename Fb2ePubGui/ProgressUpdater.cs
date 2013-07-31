using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using FB2EPubConverter.Interfaces;

namespace Fb2ePubGui
{
    public enum MessageType
    {
        Unknown = 0,
        Started,
        Finished,
        FileProcessingStarted,
        FileSaving,
        FileProcessed,
        FileSkipped,
    }

    public class MessagePacket
    {
        public string FileName { get; set; }
        public  int? Total { get; set; }
        public MessageType Type { get; set; }
    }

    [Synchronization]
    class ProgressUpdater :  ContextBoundObject, IProgressUpdateInterface
    {
        private readonly FormGUI _mainForm;
        private bool _callsEnabled = true;


        public ProgressUpdater(FormGUI mainForm)
        {
            _mainForm = mainForm;
        }


        public void ConvertStarted(int total)
        {
            if (!_callsEnabled)
            {
                return;
            }
            _mainForm.SubmitMessage(new MessagePacket
            {
                Type =  MessageType.Started,
                Total = total
            
            });
        }

        public void ConvertFinished(int total)
        {
            if (!_callsEnabled)
            {
                return;
            }
            _mainForm.SubmitMessage(new MessagePacket
            {
                Type = MessageType.Finished, 
                Total = total                
            });
        }

        public void ProcessingStarted(string fileName)
        {
            if (!_callsEnabled)
            {
                return;
            }
            _mainForm.SubmitMessage(new MessagePacket
            {
                FileName = fileName,
                Type = MessageType.FileProcessingStarted,
            });
        }

        public void ProcessingSaving(string fileName)
        {
            if (!_callsEnabled)
            {
                return;
            }
            _mainForm.SubmitMessage(new MessagePacket
            {
                FileName = fileName,
                Type = MessageType.FileSaving,
            });
        }

        public void Processed(string fileName)
        {
            if (!_callsEnabled)
            {
                return;
            }
            _mainForm.SubmitMessage(new MessagePacket
            {
                FileName = fileName,
                Type = MessageType.FileProcessed,
            });
        }

        public void SkippedDueError(string fileName)
        {
            if (!_callsEnabled)
            {
                return;
            }
            _mainForm.SubmitMessage(new MessagePacket
            {
                FileName = fileName,
                Type = MessageType.FileSkipped,
            });
        }

        internal void EnableCalls(bool enabled)
        {
            _callsEnabled = enabled;
        }
    }
}
