using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

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

    internal static class StrimClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("stim.directive")]
        internal static ClassificationTypeDefinition StimDirectiveType { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("stim.delay")]
        internal static ClassificationTypeDefinition StimDelayType { get; set; }

    }
}
