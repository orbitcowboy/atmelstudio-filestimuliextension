using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

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
            {
                var error = StimParser.ParseError(span);
                yield return error;
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}
