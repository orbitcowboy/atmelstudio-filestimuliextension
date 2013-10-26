using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Xoriath.FileStimuli.Language
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "stim.directive")]
    [Name("stim.directive")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class StimDirectiveType : ClassificationFormatDefinition
    {
        public StimDirectiveType()
        {
            DisplayName = "Stimulus Directive";
            IsBold = false;
            ForegroundColor = Colors.Blue;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "stim.delay")]
    [Name("stim.delay")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class StimDelayType : ClassificationFormatDefinition
    {
        public StimDelayType()
        {
            DisplayName = "Stimulus Delay";
            IsBold = true;
            ForegroundColor = Colors.Purple;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "stim.comment")]
    [Name("stim.comment")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class StimCommentType : ClassificationFormatDefinition
    {
        public StimCommentType()
        {
            DisplayName = "Stimulus Comment";
            IsBold = false;
            ForegroundColor = Colors.Green;
        }
    }

    internal static class StrimClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("stim.directive")]
        internal static ClassificationTypeDefinition StimDirectiveType { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("stim.delay")]
        internal static ClassificationTypeDefinition StimDelayType { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("stim.comment")]
        internal static ClassificationTypeDefinition StimCommentType { get; set; }
    }
}
