using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;

namespace Xoriath.FileStimuli
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidFileStimuliPkgString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExistsAndFullyLoaded_string)]
    public sealed class FileStimuliPackage : Package
    {
        private DTE mDTE;
        private DocumentEvents mEvents;

        public FileStimuliPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        protected override void Initialize()
        {
            base.Initialize();
            mDTE = Package.GetGlobalService(typeof(DTE)) as DTE;

            if (mDTE != null)
                RegisterSaveHook();
        }

        private void RegisterSaveHook()
        {
            if (mDTE == null)
                return;

            mEvents = mDTE.Events.DocumentEvents;
            mEvents.DocumentSaved += DocumentEvents_DocumentSaved;
        }

        void DocumentEvents_DocumentSaved(Document Document)
        {
            if (Path.GetExtension(Document.Name) == ".stim")
            {
                CheckForCorrectEndOfFile(Document);
            }
        }

        private void CheckForCorrectEndOfFile(Document Document)
        {
            TextSelection selection = Document.Selection as TextSelection;
            if (selection == null)
                return;

            VirtualPoint originalPoint = selection.ActivePoint;

            selection.EndOfDocument(false);

            VirtualPoint bottom = selection.BottomPoint;
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
