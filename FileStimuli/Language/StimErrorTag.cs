using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Xoriath.FileStimuli.Language
{
    [Export(typeof(EditorFormatDefinition))]
    [Name("stim.error")]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    internal sealed class StimErrorType : EditorFormatDefinition
    {
        public StimErrorType()
        {
            this.ForegroundColor = Colors.Red;
            this.BackgroundCustomizable = false;
            DisplayName = "Stimulus Error";
        }
    }

    static internal class StimErrorTypes
    {
        [Export(typeof(ErrorTypeDefinition))]
        [Name("stim.error")]
        internal static ErrorTypeDefinition StimErrorType { get; set; }    
    }

    internal class StimErrorTag : ErrorTag
    {
        public StimErrorTag(string tooltip) : base("stim.error", tooltip) { }
    }
}
