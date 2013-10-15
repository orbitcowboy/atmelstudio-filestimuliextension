using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System.IO;

namespace Xoriath.FileStimuli
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidFileStimuliPkgString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExistsAndFullyLoaded_string)]
    public sealed class FileStimuliPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public FileStimuliPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }
        protected override void Initialize()
        {
            base.Initialize();
            mDTE = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;

            if (mDTE != null)
                RegisterSaveHook();

        }

        private EnvDTE.DTE mDTE;
        private EnvDTE.DocumentEvents mEvents;

        private void RegisterSaveHook()
        {
            if (mDTE == null)
                return;
            mEvents = mDTE.Events.DocumentEvents;
            mEvents.DocumentSaved += DocumentEvents_DocumentSaved;
        }

        void DocumentEvents_DocumentSaved(EnvDTE.Document Document)
        {
            if (Path.GetExtension(Document.Name) == ".stim")
            {
                CheckForCorrectEndOfFile(Document);
            }
        }

        private void CheckForCorrectEndOfFile(EnvDTE.Document Document)
        {
            var selection = Document.Selection as EnvDTE.TextSelection;
            if (selection == null)
                return;

            var originalPoint = selection.ActivePoint;

            selection.EndOfDocument(false);

            var bottom = selection.BottomPoint;
            selection.GotoLine(bottom.Line, true);
            if (selection.Text.Length != 0)
            {
                selection.Text += Environment.NewLine;
                Document.Save();
            }

            selection.MoveToPoint(originalPoint);

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
