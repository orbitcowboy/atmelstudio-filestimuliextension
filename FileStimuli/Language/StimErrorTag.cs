using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Xoriath.FileStimuli.Language
{
    internal class StimErrorTag : ErrorTag
    {
        public StimErrorTag(string tooltip) : base("stim.error", tooltip) { }
    }
}
