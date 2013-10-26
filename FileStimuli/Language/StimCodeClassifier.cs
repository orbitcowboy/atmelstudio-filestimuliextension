using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;

namespace Xoriath.FileStimuli.Language
{
    internal sealed class StimCodeClassifier : IClassifier
    {
        private static readonly Dictionary<StimParser.StimLineTypes, string> mClassifierTypeNames = new Dictionary<StimParser.StimLineTypes, string>() {
            { StimParser.StimLineTypes.DIRECTIVE, "stim.directive" },
            { StimParser.StimLineTypes.DELAY, "stim.delay" },
            {StimParser.StimLineTypes.COMMENT, "stim.comment"},
        };

        private readonly ITextBuffer mTextBuffer;
        private readonly IClassificationTypeRegistryService mClassificationTypeRegistry;
        private readonly List<ClassificationSpan> classifications = new List<ClassificationSpan>();

        public StimCodeClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classifierTypeRegistry)
        {
            mTextBuffer = buffer;
            mClassificationTypeRegistry = classifierTypeRegistry;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            classifications.Clear();

            if (span.Length == 0)
                return classifications;

            ITextSnapshotLine line = span.Start.GetContainingLine();

            foreach (Tuple<StimParser.StimLineTypes, SnapshotSpan> segment in StimParser.Parse(line))
            {
                IClassificationType classificationType = mClassificationTypeRegistry.GetClassificationType(mClassifierTypeNames[segment.Item1]);
                classifications.Add(new ClassificationSpan(segment.Item2, classificationType));
            }

            return classifications;
        }

#pragma warning disable 0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 0067
    }
}
