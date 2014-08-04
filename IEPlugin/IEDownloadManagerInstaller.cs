using System.ComponentModel;
using System.Configuration.Install;
using System.Runtime.InteropServices;


namespace IEFb2ePubPlugin
{
    [RunInstaller(true)]
    public partial class IEFb2ePubPluginInstaller : Installer
    {
        public IEFb2ePubPluginInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is called when installer's custom action executes and
        /// registers the explorer plugin as COM server.
        /// </summary>
        /// <param name="stateSaver"></param>     
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            RegistrationServices regsrv = new RegistrationServices();
            if (!regsrv.RegisterAssembly(this.GetType().Assembly,
            AssemblyRegistrationFlags.SetCodeBase))
            {
                throw new InstallException("Failed To Register for COM");
            }
        }

        /// <summary>
        /// This is called when installer's custom action executes and
        /// unregisters the plugin.
        /// </summary>
        /// <param name="stateSaver"></param>     
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);

            RegistrationServices regsrv = new RegistrationServices();
            if (!regsrv.UnregisterAssembly(this.GetType().Assembly))
            {
                throw new InstallException("Failed To Unregister for COM");
            }
        }
    }
}
