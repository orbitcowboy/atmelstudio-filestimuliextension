using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Xoriath.FileStimuli.Language
{
    class StimErrorTagger : ITagger<ErrorTag>
    {
        private readonly ITextBuffer mBuffer;

        public StimErrorTagger(ITextBuffer buffer)
        {
            mBuffer = buffer;
        }

        public IEnumerable<ITagSpan<ErrorTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var span in spans)
                yield return StimParser.ParseError(span);
        }

#pragma warning disable 0067
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore 0067
    }
}
