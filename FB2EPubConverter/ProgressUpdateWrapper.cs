using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using ConverterContracts.ComInterfaces;

namespace FB2EPubConverter
{
    /// <summary>
    /// The purpose of this class is to resolve COM Interface marshaling issues when calling "third party" provided IProgressUpdateInterface
    /// interface object.
    /// The problem is that interface pointer can be used fine from one thread, but if called from different threads or (as in our case)
    /// different threads running on different CPUs - the COM pointer is invalid in all threads besides main, any access (call) to it
    /// results in "Interface not found" error, so we work around it by obtaining IUnknown to that interface and later
    /// when needed getting unique object for it, that now created in proper thread context
    /// Synchronization attribute property additionally provides needed level of synchronization, so we do not care if server implementing interface
    /// thread safe or not
    /// </summary>
    [Synchronization]
    class ProgressUpdateWrapper : ContextBoundObject, IProgressUpdateInterface, IDisposable
    {
        private IntPtr _wrappedInterfacePtr    =   IntPtr.Zero;
        private bool _disposed;

        public ProgressUpdateWrapper(IProgressUpdateInterface wrappedInterface)
        {
            if (wrappedInterface != null)
            {
                _wrappedInterfacePtr = Marshal.GetIUnknownForObjectInContext(wrappedInterface);
            }
        }

        #region IProgressUpdateInterface_implementation
        public void ConvertStarted(int total)
        {
            if (_wrappedInterfacePtr != IntPtr.Zero)
            {
                IProgressUpdateInterface wrappedInterface =
                    (IProgressUpdateInterface) Marshal.GetUniqueObjectForIUnknown(_wrappedInterfacePtr);
                wrappedInterface.ConvertStarted(total);
            }
        }

        public void ConvertFinished(int total)
        {
            if (_wrappedInterfacePtr != IntPtr.Zero)
            {
                IProgressUpdateInterface wrappedInterface =
                    (IProgressUpdateInterface)Marshal.GetUniqueObjectForIUnknown(_wrappedInterfacePtr);
                wrappedInterface.ConvertFinished(total);
            }
        }

        public void ProcessingStarted(string fileName)
        {
            if (_wrappedInterfacePtr != IntPtr.Zero)
            {
                IProgressUpdateInterface wrappedInterface =
                    (IProgressUpdateInterface)Marshal.GetUniqueObjectForIUnknown(_wrappedInterfacePtr);
                wrappedInterface.ProcessingStarted(fileName);
            }
        }

        public void ProcessingSaving(string fileName)
        {
            if (_wrappedInterfacePtr != IntPtr.Zero)
            {
                IProgressUpdateInterface wrappedInterface =
                    (IProgressUpdateInterface)Marshal.GetUniqueObjectForIUnknown(_wrappedInterfacePtr);
                wrappedInterface.ProcessingSaving(fileName);
            }
        }

        public void Processed(string fileName)
        {
            if (_wrappedInterfacePtr != IntPtr.Zero)
            {
                IProgressUpdateInterface wrappedInterface =
                    (IProgressUpdateInterface)Marshal.GetUniqueObjectForIUnknown(_wrappedInterfacePtr);
                wrappedInterface.Processed(fileName);
            }
        }

        public void SkippedDueError(string fileName)
        {
            if (_wrappedInterfacePtr != IntPtr.Zero)
            {
                IProgressUpdateInterface wrappedInterface =
                    (IProgressUpdateInterface)Marshal.GetUniqueObjectForIUnknown(_wrappedInterfacePtr);
                wrappedInterface.SkippedDueError(fileName);
            }
        }
#endregion


        #region IDisposable_pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_wrappedInterfacePtr != IntPtr.Zero && disposing)
                {
                    Marshal.Release(_wrappedInterfacePtr);
                }
                _wrappedInterfacePtr = IntPtr.Zero;
                _disposed = true;

            }
        }
        #endregion

    }
}
